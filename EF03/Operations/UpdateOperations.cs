using EF03.Data;
using EF03.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF03.Operations
{
    public static class UpdateOperations
    {
        // 1. Change Course Price
        public static void ChangePrice(int courseId, decimal newPrice)
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

                if (newPrice < 0)
                {
                    Console.WriteLine("Invalid price.");
                    return;
                }

                course.Price = newPrice;

                context.SaveChanges();

                Console.WriteLine("Course price updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Update failed: " + ex.Message);
            }
        }

        // 2. Update Enrollment Progress
        public static void UpdateProgress(
            int studentId, int courseId, decimal newProgress)
        {
            using var context = new LearningPlatformDbContext();

            try
            {
                if (newProgress < 0 || newProgress > 100)
                {
                    Console.WriteLine(
                        "Progress must be between 0 and 100.");
                    return;
                }

                // Find using Composite Primary Key
                var enrollment = context.Enrollments.Find(
                    studentId, courseId);

                if (enrollment == null)
                {
                    Console.WriteLine("Enrollment not found.");
                    return;
                }

                enrollment.Progress = newProgress;

                if (newProgress == 100)
                {
                    enrollment.Status = EnrollmentStatus.Completed;
                }
                else
                {
                    enrollment.Status = EnrollmentStatus.Active;
                    enrollment.FinalGrade = null;
                }

                context.SaveChanges();

                Console.WriteLine("Progress updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Update failed: " + ex.Message);
            }
        }

        // 3. Publish Course
        public static void PublishCourse(int courseId)
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

                course.IsPublished = true;
                course.PublishedAt = DateTime.Now;

                context.SaveChanges();

                Console.WriteLine("Course published successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Publish failed: " + ex.Message);
            }
        }
    }
}
