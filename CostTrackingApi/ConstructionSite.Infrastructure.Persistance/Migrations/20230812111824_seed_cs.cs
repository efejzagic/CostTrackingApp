using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConstructionSite.Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class seed_cs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ConstructionSite",
                columns: new[] { "Id", "Address", "City", "Country", "Description", "Title" },
                values: new object[,]
                {
                    { 1, "Address 1", "City 1", "Country 1", "Description 1", "Construction Site 1" },
                    { 2, "Address 2", "City 2", "Country 2", "Description 2", "Construction Site 2" }
                });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "Id", "Address", "City", "ConstructionSiteId", "Country", "HourlyRate", "HoursOfWork", "Name", "Salary", "Surname" },
                values: new object[,]
                {
                    { 1, "Address 1", "City 1", 1, "Country 1", 15.5, 8, "User", 2700.0, "One" },
                    { 2, "Address 2", "City 2", 2, "Country 2", 12.5, 7, "Test", 1920.5, "Two" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ConstructionSite",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ConstructionSite",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
