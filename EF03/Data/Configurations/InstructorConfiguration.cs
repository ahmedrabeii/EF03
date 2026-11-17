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
    public class InstructorConfiguration
          : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(
            EntityTypeBuilder<Instructor> builder)
        {
            builder.HasKey(i => i.InstructorId);

            builder.Property(i => i.FullName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(i => i.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(i => i.Bio)
                .HasMaxLength(500);

            builder.Property(i => i.JoinedAt)
                .IsRequired();

            builder.HasMany(i => i.Courses)
                .WithOne(c => c.Instructor)
                .HasForeignKey(c => c.InstructorId)
                .IsRequired();

            // Seed Data
            builder.HasData(
                new Instructor
                {
                    InstructorId = 1,
                    FullName = "Ahmed Ali",
                    Email = "ahmed@example.com",
                    Bio = "Backend Developer",
                    JoinedAt = new DateTime(2025, 1, 1)
                },
                new Instructor
                {
                    InstructorId = 2,
                    FullName = "Sara Mohamed",
                    Email = "sara@example.com",
                    Bio = "Software Engineer",
                    JoinedAt = new DateTime(2025, 2, 1)
                },
                new Instructor
                {
                    InstructorId = 3,
                    FullName = "Omar Hassan",
                    Email = "omar@example.com",
                    Bio = null,
                    JoinedAt = new DateTime(2025, 3, 1)
                }
            );
        }
    }
}
