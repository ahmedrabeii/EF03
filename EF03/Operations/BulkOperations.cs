using EF03.Data;
using EF03.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF03.Operations
{
    public static class BulkOperations
    {
        // 1. Bulk Price Raise
        public static void BulkPriceRaise(CourseLevel level)
        {
            using var context = new LearningPlatformDbContext();

            try
            {
                int affectedRows = context.Courses
                    .Where(c => c.IsPublished && c.Level == level)
                    .ExecuteUpdate(setters =>
                        setters.SetProperty(
                            c => c.Price,
                            c => c.Price * 1.10m));

                Console.WriteLine(
                    "Courses Updated: " + affectedRows);
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "Bulk Update failed: " + ex.Message);
            }
        }

        // 2. Bulk Delete
        public static void BulkDelete()
        {
            using var context = new LearningPlatformDbContext();

            try
            {
                int affectedRows = context.Enrollments
                    .Where(e => e.Status == EnrollmentStatus.Dropped)
                    .ExecuteDelete();

                Console.WriteLine(
                    "Dropped Enrollments Deleted: " + affectedRows);
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "Bulk Delete failed: " + ex.Message);
            }
        }
    }
}
