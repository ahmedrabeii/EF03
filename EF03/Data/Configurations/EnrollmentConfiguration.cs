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
    public class EnrollmentConfiguration
           : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(
            EntityTypeBuilder<Enrollment> builder)
        {
            // Composite Primary Key
            builder.HasKey(e => new
            {
                e.StudentId,
                e.CourseId
            });

            builder.Property(e => e.EnrollmentDate)
                .IsRequired();

            builder.Property(e => e.Progress)
                .HasPrecision(5, 2)
                .IsRequired();

            builder.Property(e => e.Status)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(e => e.FinalGrade)
                .HasPrecision(5, 2)
                .IsRequired(false);

            builder.HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId);

            builder.HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId);
        }
    }
}
