using EF03.Data;
using EF03.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF03.Operations
{
    public static class CreateOperations
    {
        // 1. Add Instructor
        public static void AddInstructor()
        {
            using var context = new LearningPlatformDbContext();

            try
            {
                var instructor = new Instructor
                {
                    FullName = "Mahmoud Ibrahim",
                    Email = "mahmoud@example.com",
                    Bio = "C# Developer",
                    JoinedAt = DateTime.Now
                };

                context.Instructors.Add(instructor);
                context.SaveChanges();

                Console.WriteLine(
                    "Instructor Added. ID: " + instructor.InstructorId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "Could not add instructor: " + ex.Message);
            }
        }

        // 2. Add Course
        public static void AddCourse()
        {
            using var context = new LearningPlatformDbContext();

            try
            {
                int instructorId = 1;
                int categoryId = 3;

                bool instructorExists = context.Instructors
                    .Any(i => i.InstructorId == instructorId);

                bool categoryExists = context.Categories
                    .Any(c => c.CategoryId == categoryId);

                if (!instructorExists || !categoryExists)
                {
                    Console.WriteLine(
                        "Instructor or Category not found.");
                    return;
                }

                var course = new Course
                {
                    Title = "Entity Framework Core",
                    Description = "Learn EF Core",
                    Price = 750,
                    Level = CourseLevel.Intermediate,
                    IsPublished = false,
                    PublishedAt = null,
                    InstructorId = instructorId,
                    CategoryId = categoryId
                };

                context.Courses.Add(course);
                context.SaveChanges();

                Console.WriteLine(
                    "Course Added. ID: " + course.CourseId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "Could not add course: " + ex.Message);
            }
        }

        // 3. Add Several Students
        public static void AddSeveralStudents()
        {
            using var context = new LearningPlatformDbContext();

            try
            {
                var students = new List<Student>
                {
                    new Student
                    {
                        FullName = "Khaled Ahmed",
                        Email = "khaled@example.com",
                        DateOfBirth = new DateTime(2002, 3, 10)
                    },
                    new Student
                    {
                        FullName = "Nour Ali",
                        Email = "nour@example.com",
                        DateOfBirth = new DateTime(2003, 6, 15)
                    },
                    new Student
                    {
                        FullName = "Mariam Hassan",
                        Email = "mariam@example.com",
                        DateOfBirth = new DateTime(2001, 9, 20)
                    }
                };

                context.Students.AddRange(students);
                context.SaveChanges();

                foreach (var student in students)
                {
                    Console.WriteLine(
                        student.FullName +
                        " - ID: " + student.StudentId);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "Could not add students: " + ex.Message);
            }
        }

        // 4. Enroll Student
        public static void EnrollStudent(
            int studentId, int courseId)
        {
            using var context = new LearningPlatformDbContext();

            try
            {
                var student = context.Students.Find(studentId);

                if (student == null)
                {
                    Console.WriteLine("Student not found.");
                    return;
                }

                var course = context.Courses.Find(courseId);

                if (course == null)
                {
                    Console.WriteLine("Course not found.");
                    return;
                }

                if (!course.IsPublished)
                {
                    Console.WriteLine(
                        "Enrollment refused: Course is not published.");
                    return;
                }

                bool alreadyEnrolled = context.Enrollments.Any(e =>
                    e.StudentId == studentId &&
                    e.CourseId == courseId);

                if (alreadyEnrolled)
                {
                    Console.WriteLine(
                        "Student is already enrolled in this course.");
                    return;
                }

                var enrollment = new Enrollment
                {
                    StudentId = studentId,
                    CourseId = courseId,
                    EnrollmentDate = DateTime.Now,
                    Progress = 0,
                    Status = EnrollmentStatus.Active,
                    FinalGrade = null
                };

                context.Enrollments.Add(enrollment);
                context.SaveChanges();

                Console.WriteLine(
                    "Student enrolled successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "Enrollment failed: " + ex.Message);
            }
        }
    }
}
