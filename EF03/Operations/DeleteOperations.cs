using EF03.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF03.Operations
{
    public static class DeleteOperations
    {
        // 1. Unenroll Student
        public static void Unenroll(int studentId, int courseId)
        {
            using var context = new LearningPlatformDbContext();

            try
            {
                var enrollment = context.Enrollments.Find(
                    studentId, courseId);

                if (enrollment == null)
                {
                    Console.WriteLine("Enrollment not found.");
                    return;
                }

                context.Enrollments.Remove(enrollment);
                context.SaveChanges();

                Console.WriteLine("Student unenrolled successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Delete failed: " + ex.Message);
            }
        }

        // 2. Try to Delete Category with Sub-Categories
        public static void TryDeleteCategory(int categoryId)
        {
            using var context = new LearningPlatformDbContext();

            try
            {
                var category = context.Categories.Find(categoryId);

                if (category == null)
                {
                    Console.WriteLine("Category not found.");
                    return;
                }

                context.Categories.Remove(category);
                context.SaveChanges();

                Console.WriteLine("Category deleted.");
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(
                    "Cannot delete category because it has related records.");

                Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
            }
        }

        // 3. Safe Delete Category
        public static void SafeDeleteCategory(int categoryId)
        {
            using var context = new LearningPlatformDbContext();

            try
            {
                var category = context.Categories.Find(categoryId);

                if (category == null)
                {
                    Console.WriteLine("Category not found.");
                    return;
                }

                bool hasSubCategories = context.Categories
                    .Any(c => c.ParentCategoryId == categoryId);

                if (hasSubCategories)
                {
                    Console.WriteLine(
                        "Cannot delete category: It has sub-categories.");
                    return;
                }

                bool hasCourses = context.Courses
                    .Any(c => c.CategoryId == categoryId);

                if (hasCourses)
                {
                    Console.WriteLine(
                        "Cannot delete category: It has courses.");
                    return;
                }

                context.Categories.Remove(category);
                context.SaveChanges();

                Console.WriteLine("Category deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Delete failed: " + ex.Message);
            }
        }

        // 4. Delete Course with Enrollments
        public static void DeleteCourse(int courseId)
        {
            using var context = new LearningPlatformDbContext();

            try
            {
                var course = context.Courses.Find(courseId);

                if (course == null)
                {
                    Console.WriteLine("Course not found.");
                    return;
                }

                int beforeCount = context.Enrollments
                    .Count(e => e.CourseId == courseId);

                Console.WriteLine(
                    "Enrollments Before Delete: " + beforeCount);

                context.Courses.Remove(course);
                context.SaveChanges();

                // Enrollment records are removed automatically
                // because the required Course relationship uses Cascade Delete.

                int afterCount = context.Enrollments
                    .Count(e => e.CourseId == courseId);

                Console.WriteLine(
                    "Enrollments After Delete: " + afterCount);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Delete failed: " + ex.Message);
            }
        }
    }
}
