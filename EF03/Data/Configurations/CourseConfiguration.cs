using EF03.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF03.Data.Configurations
{
    public class CourseConfiguration
            : IEntityTypeConfiguration<Course>
    {
        public void Configure(
            EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.CourseId);

            builder.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.Description)
                .HasMaxLength(1000);

            builder.Property(c => c.Price)
                .HasPrecision(10, 2);

            builder.Property(c => c.Level)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(c => c.IsPublished)
                .IsRequired();

            builder.Property(c => c.PublishedAt)
                .IsRequired(false);

            builder.HasOne(c => c.Instructor)
                .WithMany(i => i.Courses)
                .HasForeignKey(c => c.InstructorId)
                .IsRequired();

            builder.HasOne(c => c.Category)
                .WithMany(cat => cat.Courses)
                .HasForeignKey(c => c.CategoryId)
                .IsRequired();

            // Seed Data
            builder.HasData(
                new Course
                {
                    CourseId = 1,
                    Title = "C# Fundamentals",
                    Description = "Learn C# basics",
                    Price = 500,
                    Level = CourseLevel.Beginner,
                    IsPublished = true,
                    PublishedAt = new DateTime(2025, 4, 1),
                    InstructorId = 1,
                    CategoryId = 1
                },
                new Course
                {
                    CourseId = 2,
                    Title = "ASP.NET Core",
                    Description = "Learn ASP.NET Core",
                    Price = 800,
                    Level = CourseLevel.Intermediate,
                    IsPublished = true,
                    PublishedAt = new DateTime(2025, 4, 5),
                    InstructorId = 1,
                    CategoryId = 3
                },
                new Course
                {
                    CourseId = 3,
                    Title = "Advanced Web API",
                    Description = "Build Web APIs",
                    Price = 1000,
                    Level = CourseLevel.Advanced,
                    IsPublished = true,
                    PublishedAt = new DateTime(2025, 4, 10),
                    InstructorId = 2,
                    CategoryId = 2
                },
                new Course
                {
                    CourseId = 4,
                    Title = "HTML and CSS",
                    Description = "Frontend Basics",
                    Price = 400,
                    Level = CourseLevel.Beginner,
                    IsPublished = true,
                    PublishedAt = new DateTime(2025, 4, 15),
                    InstructorId = 2,
                    CategoryId = 4
                },
                new Course
                {
                    CourseId = 5,
                    Title = "SQL Server",
                    Description = "Database Fundamentals",
                    Price = 600,
                    Level = CourseLevel.Intermediate,
                    IsPublished = false,
                    PublishedAt = null,
                    InstructorId = 3,
                    CategoryId = 5
                },
                new Course
                {
                    CourseId = 6,
                    Title = "Advanced C#",
                    Description = "Advanced C# Topics",
                    Price = 900,
                    Level = CourseLevel.Advanced,
                    IsPublished = false,
                    PublishedAt = null,
                    InstructorId = 3,
                    CategoryId = 1
                }
            );
        }
    }
}
