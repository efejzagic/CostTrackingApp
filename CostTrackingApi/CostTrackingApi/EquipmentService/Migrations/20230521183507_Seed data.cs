using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EquipmentService.Migrations
{
    /// <inheritdoc />
    public partial class Seeddata : Migration
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

            migrationBuilder.CreateTable(
                name: "MachineryServicing",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    MachineryId = table.Column<int>(type: "integer", nullable: false),
                    ServiceDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    retired = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineryServicing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MachineryServicing_Machinery_MachineryId",
                        column: x => x.MachineryId,
                        principalTable: "Machinery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ToolServicing",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    ToolId = table.Column<int>(type: "integer", nullable: false),
                    ServiceDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    retired = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToolServicing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ToolServicing_Tool_ToolId",
                        column: x => x.ToolId,
                        principalTable: "Tool",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Machinery",
                columns: new[] { "Id", "Description", "Location", "MachineryStatus", "Name", "ProductionYear", "retired" },
                values: new object[,]
                {
                    { 1, "Description 1", "Loc 1", 3, "Machinery 1", new DateOnly(2023, 5, 21), false },
                    { 2, "Description 2", "Loc 2", 0, "Machinery 2", new DateOnly(2023, 5, 21), false }
                });

            migrationBuilder.InsertData(
                table: "Tool",
                columns: new[] { "Id", "Description", "Location", "Title", "ToolStatus", "retired" },
                values: new object[,]
                {
                    { 1, "Desc 1", "Loc 1", "Tool 1", 0, false },
                    { 2, "Desc 2", "Loc 2", "Tool 2", 3, false }
                });

            migrationBuilder.InsertData(
                table: "MachineryServicing",
                columns: new[] { "Id", "Description", "MachineryId", "Price", "ServiceDate", "Title", "retired" },
                values: new object[,]
                {
                    { 1, "Desc 1", 1, 10.0, new DateTime(2023, 5, 21, 18, 35, 7, 495, DateTimeKind.Utc).AddTicks(2596), "Machine Service 1", false },
                    { 2, "Desc 2", 2, 20.5, new DateTime(2023, 5, 21, 18, 35, 7, 495, DateTimeKind.Utc).AddTicks(2599), "Machine Serivce 2", false }
                });

            migrationBuilder.InsertData(
                table: "ToolServicing",
                columns: new[] { "Id", "Description", "Price", "ServiceDate", "Title", "ToolId", "retired" },
                values: new object[,]
                {
                    { 1, "Desc 1", 10.0, new DateTime(2023, 5, 21, 18, 35, 7, 495, DateTimeKind.Utc).AddTicks(2618), "Machine Service 1", 1, false },
                    { 2, "Desc 2", 20.5, new DateTime(2023, 5, 21, 18, 35, 7, 495, DateTimeKind.Utc).AddTicks(2621), "Machine Serivce 2", 2, false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MachineryServicing_MachineryId",
                table: "MachineryServicing",
                column: "MachineryId");

            migrationBuilder.CreateIndex(
                name: "IX_ToolServicing_ToolId",
                table: "ToolServicing",
                column: "ToolId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MachineryServicing");

            migrationBuilder.DropTable(
                name: "ToolServicing");

            migrationBuilder.DropTable(
                name: "Machinery");

            migrationBuilder.DropTable(
                name: "Tool");
        }
    }
}
