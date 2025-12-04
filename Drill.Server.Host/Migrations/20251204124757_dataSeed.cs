using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Drill.Server.Host.Migrations
{
    /// <inheritdoc />
    public partial class dataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var now = DateTimeOffset.UtcNow;

            // Seed Users
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "CreatedAt" },
                values: new object[,]
                {
                    { 1, "john.doe@example.com", "John", "Doe", now },
                    { 2, "jane.smith@example.com", "Jane", "Smith", now },
                    { 3, "mike.johnson@example.com", "Mike", "Johnson", now }
                });

            // Seed Drills
            migrationBuilder.InsertData(
                table: "Drill",
                columns: new[] { "Id", "Title", "PricePerMinute", "CreatedAt" },
                values: new object[,]
                {
                    { 1, "Spiral Drill", 2.5f, now },
                    { 2, "Auger Drill", 3.0f, now },
                    { 3, "Core Drill", 4.5f, now },
                    { 4, "Paddle Drill", 5.0f, now },
                    { 5, "Percussion Drill", 2.0f, now }
                });

            migrationBuilder.Sql("ALTER SEQUENCE \"public\".\"User_Id_seq\" RESTART WITH 4;");
            migrationBuilder.Sql("ALTER SEQUENCE \"public\".\"Drill_Id_seq\" RESTART WITH 6;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove seed data for Drills
            migrationBuilder.DeleteData(
                table: "Drill",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4, 5 });

            // Remove seed data for Users
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3 });
        }
    }
}
