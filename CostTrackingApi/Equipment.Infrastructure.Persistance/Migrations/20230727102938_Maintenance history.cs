using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Equipment.Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Maintenancehistory : Migration
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
                name: "MachineryService",
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
                name: "Maintenance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    MachineryId = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    MaintenanceDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    retired = table.Column<bool>(type: "boolean", nullable: false),
                    ToolId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maintenance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Maintenance_Machinery_MachineryId",
                        column: x => x.MachineryId,
                        principalTable: "Machinery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Maintenance_Tool_ToolId",
                        column: x => x.ToolId,
                        principalTable: "Tool",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ToolService",
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
                columns: new[] { "Id", "Description", "Location", "MachineryStatus", "Name", "retired" },
                values: new object[,]
                {
                    { 1, "Description 1", "Loc 1", 3, "Machinery 1", false },
                    { 2, "Description 2", "Loc 2", 0, "Machinery 2", false }
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
                table: "MachineryService",
                columns: new[] { "Id", "Description", "MachineryId", "Price", "ServiceDate", "Title", "retired" },
                values: new object[,]
                {
                    { 1, "Desc 1", 1, 10.0, new DateTime(2023, 7, 27, 10, 29, 38, 392, DateTimeKind.Utc).AddTicks(1342), "Machine Service 1", false },
                    { 2, "Desc 2", 2, 20.5, new DateTime(2023, 7, 27, 10, 29, 38, 392, DateTimeKind.Utc).AddTicks(1344), "Machine Serivce 2", false }
                });

            migrationBuilder.InsertData(
                table: "ToolService",
                columns: new[] { "Id", "Description", "Price", "ServiceDate", "Title", "ToolId", "retired" },
                values: new object[,]
                {
                    { 1, "Desc 1", 10.0, new DateTime(2023, 7, 27, 10, 29, 38, 392, DateTimeKind.Utc).AddTicks(1362), "Machine Service 1", 1, false },
                    { 2, "Desc 2", 20.5, new DateTime(2023, 7, 27, 10, 29, 38, 392, DateTimeKind.Utc).AddTicks(1365), "Machine Serivce 2", 2, false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MachineryServicing_MachineryId",
                table: "MachineryService",
                column: "MachineryId");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenance_MachineryId",
                table: "Maintenance",
                column: "MachineryId");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenance_ToolId",
                table: "Maintenance",
                column: "ToolId");

            migrationBuilder.CreateIndex(
                name: "IX_ToolServicing_ToolId",
                table: "ToolService",
                column: "ToolId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MachineryService");

            migrationBuilder.DropTable(
                name: "Maintenance");

            migrationBuilder.DropTable(
                name: "ToolService");

            migrationBuilder.DropTable(
                name: "Machinery");

            migrationBuilder.DropTable(
                name: "Tool");
        }
    }
}
