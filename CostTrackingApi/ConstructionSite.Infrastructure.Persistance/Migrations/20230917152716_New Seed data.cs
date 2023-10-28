using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConstructionSite.Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class NewSeeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConstructionSite",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstructionSite", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false),
                    ConstructionSiteId = table.Column<int>(type: "integer", nullable: false),
                    HourlyRate = table.Column<double>(type: "double precision", nullable: false),
                    HoursOfWork = table.Column<int>(type: "integer", nullable: false),
                    Salary = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_ConstructionSite_ConstructionSiteId",
                        column: x => x.ConstructionSiteId,
                        principalTable: "ConstructionSite",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ConstructionSite",
                columns: new[] { "Id", "Address", "City", "Country", "Description", "Title" },
                values: new object[,]
                {
                    { 1, "Južni logor", "Mostar", "Bosnia and Herzegovina", "", "Olympic swimming pool" },
                    { 2, "Socijalno", "Sarajevo", "Bosnia and Herzegovina", "", "Tram line" },
                    { 3, "Maršala Tita", "Konjic", "Bosnia and Herzegovina", "", "Road paving" }
                });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "Id", "Address", "City", "ConstructionSiteId", "Country", "HourlyRate", "HoursOfWork", "Name", "Salary", "Surname" },
                values: new object[,]
                {
                    { 1, "Address 1", "City 1", 1, "Country 1", 15.5, 8, "User", 2700.0, "One" },
                    { 2, "Address 2", "City 2", 2, "Country 2", 12.5, 7, "Test", 1920.5, "Two" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_ConstructionSiteId",
                table: "Employee",
                column: "ConstructionSiteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "ConstructionSite");
        }
    }
}
