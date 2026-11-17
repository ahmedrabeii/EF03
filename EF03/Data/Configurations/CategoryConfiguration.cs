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
    public class CategoryConfiguration
            : IEntityTypeConfiguration<Category>
    {
        public void Configure(
            EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.CategoryId);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Description)
                .HasMaxLength(500);

            // Self-Referencing Relationship
            builder.HasOne(c => c.ParentCategory)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed Data - 3 Levels
            builder.HasData(
                new Category
                {
                    CategoryId = 1,
                    Name = "Programming",
                    Description = "Programming Courses",
                    ParentCategoryId = null
                },
                new Category
                {
                    CategoryId = 2,
                    Name = "Backend",
                    Description = "Backend Development",
                    ParentCategoryId = 1
                },
                new Category
                {
                    CategoryId = 3,
                    Name = "ASP.NET",
                    Description = "ASP.NET Courses",
                    ParentCategoryId = 2
                },
                new Category
                {
                    CategoryId = 4,
                    Name = "Frontend",
                    Description = "Frontend Development",
                    ParentCategoryId = 1
                },
                new Category
                {
                    CategoryId = 5,
                    Name = "Database",
                    Description = "Database Courses",
                    ParentCategoryId = null
                }
            );
        }
    }
}
