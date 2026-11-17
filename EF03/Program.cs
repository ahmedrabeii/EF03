using EF03.Data;
using EF03.Models;
using EF03.Operations;

namespace EF03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var context = new LearningPlatformDbContext())
            {
                // Seed Students
                if (!context.Students.Any())
                {
                    var students = new List<Student>
        {
            new Student
            {
                FullName = "Ali Hassan",
                Email = "ali@gmail.com",
                DateOfBirth = new DateTime(2003, 5, 10)
            },
            new Student
            {
                FullName = "Mona Ahmed",
                Email = "mona@gmail.com",
                DateOfBirth = new DateTime(2002, 8, 15)
            },
            new Student
            {
                FullName = "Omar Mohamed",
                Email = "omar@gmail.com",
                DateOfBirth = new DateTime(2004, 2, 20)
            },
            new Student
            {
                FullName = "Sara Ali",
                Email = "sara@gmail.com",
                DateOfBirth = new DateTime(2003, 11, 5)
            },
            new Student
            {
                FullName = "Youssef Khaled",
                Email = "youssef@gmail.com",
                DateOfBirth = new DateTime(2001, 7, 12)
            }
        };

                    context.Students.AddRange(students);
                    context.SaveChanges();

                    Console.WriteLine("Students Seeded Successfully");
                }

                // Seed Enrollments
                if (!context.Enrollments.Any())
                {
                    var students = context.Students
                        .OrderBy(s => s.StudentId)
                        .Take(5)
                        .ToList();

                    var validCourses = context.Courses
                        .Where(c => c.IsPublished)
                        .OrderBy(c => c.CourseId)
                        .Take(4)
                        .ToList();

                    if (students.Count < 5 || validCourses.Count < 4)
                    {
                        Console.WriteLine(
                            "Cannot seed enrollments: Students or published courses are missing.");
                    }
                    else
                    {
                        var enrollments = new List<Enrollment>
            {
                new Enrollment
                {
                    StudentId = students[0].StudentId,
                    CourseId = validCourses[0].CourseId,
                    EnrollmentDate = DateTime.Now,
                    Progress = 45,
                    Status = EnrollmentStatus.Active
                },
                new Enrollment
                {
                    StudentId = students[1].StudentId,
                    CourseId = validCourses[0].CourseId,
                    EnrollmentDate = DateTime.Now,
                    Progress = 100,
                    Status = EnrollmentStatus.Completed,
                    FinalGrade = 95
                },
                new Enrollment
                {
                    StudentId = students[2].StudentId,
                    CourseId = validCourses[1].CourseId,
                    EnrollmentDate = DateTime.Now,
                    Progress = 70,
                    Status = EnrollmentStatus.Active
                },
                new Enrollment
                {
                    StudentId = students[3].StudentId,
                    CourseId = validCourses[1].CourseId,
                    EnrollmentDate = DateTime.Now,
                    Progress = 25,
                    Status = EnrollmentStatus.Dropped
                },
                new Enrollment
                {
                    StudentId = students[4].StudentId,
                    CourseId = validCourses[2].CourseId,
                    EnrollmentDate = DateTime.Now,
                    Progress = 100,
                    Status = EnrollmentStatus.Completed,
                    FinalGrade = 90
                },
                new Enrollment
                {
                    StudentId = students[0].StudentId,
                    CourseId = validCourses[1].CourseId,
                    EnrollmentDate = DateTime.Now,
                    Progress = 100,
                    Status = EnrollmentStatus.Completed,
                    FinalGrade = 88
                },
                new Enrollment
                {
                    StudentId = students[1].StudentId,
                    CourseId = validCourses[2].CourseId,
                    EnrollmentDate = DateTime.Now,
                    Progress = 10,
                    Status = EnrollmentStatus.Dropped
                },
                new Enrollment
                {
                    StudentId = students[2].StudentId,
                    CourseId = validCourses[3].CourseId,
                    EnrollmentDate = DateTime.Now,
                    Progress = 60,
                    Status = EnrollmentStatus.Active
                }
            };

                        context.Enrollments.AddRange(enrollments);
                        context.SaveChanges();

                        Console.WriteLine("Enrollments Seeded Successfully");
                    }
                }
            }


            Console.WriteLine("\n===== CREATE OPERATIONS =====");

            CreateOperations.AddInstructor();

            CreateOperations.AddCourse();

            CreateOperations.AddSeveralStudents();

            CreateOperations.EnrollStudent(6, 1);


            Console.WriteLine("\n===== READ OPERATIONS =====");

            ReadOperations.FindCourse();

            ReadOperations.FilterCourses();

            ReadOperations.ShowSubCategories();

            ReadOperations.ShowCourseRoster(1);


            Console.WriteLine("\n===== UPDATE OPERATIONS =====");

            UpdateOperations.ChangePrice(1, 550);

            UpdateOperations.UpdateProgress(1, 1, 100);

            UpdateOperations.PublishCourse(5);


            Console.WriteLine("\n===== DELETE OPERATIONS =====");

            DeleteOperations.Unenroll(1, 2);

            DeleteOperations.TryDeleteCategory(1);

            DeleteOperations.SafeDeleteCategory(1);

            DeleteOperations.DeleteCourse(1);


            Console.WriteLine("\n===== BULK OPERATIONS =====");

            BulkOperations.BulkPriceRaise(CourseLevel.Beginner);

            BulkOperations.BulkDelete();


            Console.WriteLine("\nAll Operations Finished!");

            Console.ReadKey();

        }
    }
}
