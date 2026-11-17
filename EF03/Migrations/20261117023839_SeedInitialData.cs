using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EF03.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Description", "Name", "ParentCategoryId" },
                values: new object[,]
                {
                    { 1, "Programming Courses", "Programming", null },
                    { 5, "Database Courses", "Database", null }
                });

            migrationBuilder.InsertData(
                table: "Instructors",
                columns: new[] { "InstructorId", "Bio", "Email", "FullName", "JoinedAt" },
                values: new object[,]
                {
                    { 1, "Backend Developer", "ahmed@example.com", "Ahmed Ali", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Software Engineer", "sara@example.com", "Sara Mohamed", new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, null, "omar@example.com", "Omar Hassan", new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Description", "Name", "ParentCategoryId" },
                values: new object[,]
                {
                    { 2, "Backend Development", "Backend", 1 },
                    { 4, "Frontend Development", "Frontend", 1 }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "CategoryId", "Description", "InstructorId", "IsPublished", "Level", "Price", "PublishedAt", "Title" },
                values: new object[,]
                {
                    { 1, 1, "Learn C# basics", 1, true, "Beginner", 500m, new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "C# Fundamentals" },
                    { 5, 5, "Database Fundamentals", 3, false, "Intermediate", 600m, null, "SQL Server" },
                    { 6, 1, "Advanced C# Topics", 3, false, "Advanced", 900m, null, "Advanced C#" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Description", "Name", "ParentCategoryId" },
                values: new object[] { 3, "ASP.NET Courses", "ASP.NET", 2 });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "CategoryId", "Description", "InstructorId", "IsPublished", "Level", "Price", "PublishedAt", "Title" },
                values: new object[,]
                {
                    { 3, 2, "Build Web APIs", 2, true, "Advanced", 1000m, new DateTime(2025, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Advanced Web API" },
                    { 4, 4, "Frontend Basics", 2, true, "Beginner", 400m, new DateTime(2025, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "HTML and CSS" },
                    { 2, 3, "Learn ASP.NET Core", 1, true, "Intermediate", 800m, new DateTime(2025, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "ASP.NET Core" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "InstructorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "InstructorId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "InstructorId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1);
        }
    }
}
