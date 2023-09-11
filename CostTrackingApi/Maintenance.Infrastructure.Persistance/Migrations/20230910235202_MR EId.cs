using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Maintenance.Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class MREId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaintenanceRecord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    MachineryId = table.Column<int>(type: "integer", nullable: true),
                    ToolId = table.Column<int>(type: "integer", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    Technician = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceRecord", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "MaintenanceRecord",
                columns: new[] { "Id", "Description", "MachineryId", "Name", "Price", "Status", "Technician", "Timestamp", "ToolId" },
                values: new object[,]
                {
                    { 1, "Record for machine SN2023", 1, "MR 1", 120.40000000000001, "Completed", "User 1", new DateTime(2023, 9, 10, 23, 52, 2, 390, DateTimeKind.Utc).AddTicks(9078), null },
                    { 2, "Record for tool SN2021", null, "MR 2", 87.150000000000006, "Pending", "User 2", new DateTime(2023, 9, 10, 23, 52, 2, 390, DateTimeKind.Utc).AddTicks(9098), 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaintenanceRecord");
        }
    }
}
