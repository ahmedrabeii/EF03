using EF03.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF03.Operations
{
    public static class ReadOperations
    {
        // 1. Find Course by ID and Title
        public static void FindCourse()
        {
            using var context = new LearningPlatformDbContext();

            int courseId = 1;
            string courseTitle = "ASP.NET Core";

            var courseById = context.Courses.Find(courseId);

            if (courseById == null)
            {
                Console.WriteLine("Course by ID not found.");
            }
            else
            {
                Console.WriteLine(
                    "Found by ID: " + courseById.Title);
            }

            var courseByTitle = context.Courses
                .FirstOrDefault(c => c.Title == courseTitle);

            if (courseByTitle == null)
            {
                Console.WriteLine("Course by Title not found.");
            }
            else
            {
                Console.WriteLine(
                    "Found by Title: " + courseByTitle.Title);
            }
        }

        // 2. Filter Courses
        public static void FilterCourses()
        {
            using var context = new LearningPlatformDbContext();

            decimal minPrice = 400;
            decimal maxPrice = 900;

            var courses = context.Courses
                .Where(c => c.Price >= minPrice &&
                            c.Price <= maxPrice)
                .OrderByDescending(c => c.Price)
                .Select(c => new
                {
                    c.Title,
                    c.Level,
                    c.Price
                })
                .ToList();

            foreach (var course in courses)
            {
                Console.WriteLine(
                    $"{course.Title} | {course.Level} | {course.Price}");
            }
        }

        // 3. Sub-Categories
        public static void ShowSubCategories()
        {
            using var context = new LearningPlatformDbContext();

            int categoryId = 1;

            var subCategories = context.Categories
                .Where(c => c.ParentCategoryId == categoryId)
                .ToList();

            Console.WriteLine("Sub-Categories:");

            foreach (var category in subCategories)
            {
                Console.WriteLine(category.Name);
            }

            var topCategories = context.Categories
                .Where(c => c.ParentCategoryId == null)
                .ToList();

            Console.WriteLine("Top-Level Categories:");

            foreach (var category in topCategories)
            {
                Console.WriteLine(category.Name);
            }
        }

        // 4. Course Roster
        public static void ShowCourseRoster(int courseId)
        {
            using var context = new LearningPlatformDbContext();

            var course = context.Courses.Find(courseId);

            if (course == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            var studentIds = context.Enrollments
                .Where(e => e.CourseId == courseId)
                .Select(e => e.StudentId)
                .ToList();

            Console.WriteLine(
                "Number of enrolled students: " +
                studentIds.Count);

            var students = context.Students
                .Where(s => studentIds.Contains(s.StudentId))
                .ToList();

            foreach (var student in students)
            {
                Console.WriteLine(student.FullName);
            }
        }
    }
}
