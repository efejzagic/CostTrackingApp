using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EquipmentService.Migrations
{
    /// <inheritdoc />
    public partial class EquipmentOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Machinery",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ProductionYear = table.Column<DateOnly>(type: "date", nullable: false),
                    MachineryStatus = table.Column<int>(type: "integer", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    retired = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machinery", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tool",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ToolStatus = table.Column<int>(type: "integer", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    retired = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tool", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Machinery",
                columns: new[] { "Id", "Description", "Location", "MachineryStatus", "Name", "ProductionYear", "retired" },
                values: new object[,]
                {
                    { 1, "Description 1", "Loc 1", 3, "Machinery 1", new DateOnly(2023, 5, 20), false },
                    { 2, "Description 2", "Loc 2", 0, "Machinery 2", new DateOnly(2023, 5, 20), false }
                });

            migrationBuilder.InsertData(
                table: "Tool",
                columns: new[] { "Id", "Description", "Location", "Title", "ToolStatus", "retired" },
                values: new object[,]
                {
                    { 1, "Desc 1", "Loc 1", "Tool 1", 0, false },
                    { 2, "Desc 2", "Loc 2", "Tool 2", 3, false }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Machinery");

            migrationBuilder.DropTable(
                name: "Tool");
        }
    }
}
