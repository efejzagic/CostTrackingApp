using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Maintenance.Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class newseeddata : Migration
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
                    { 1, "Service record for Komatsu excavator KPC200", 1, "Komatsu PC200 maintenance", 340.0, "Completed", "Emil Fejzagić", new DateTime(2023, 9, 17, 15, 29, 45, 133, DateTimeKind.Utc).AddTicks(8509), null },
                    { 2, "Service record for BOMAG roller compactor BAC200", 8, "BOMAG roller compactor", 120.40000000000001, "Pending", "Emil Fejzagić", new DateTime(2023, 9, 17, 15, 29, 45, 133, DateTimeKind.Utc).AddTicks(8515), null },
                    { 3, "Service record for Caterpillar AP SN: CAP300", 6, "Caterpillar AP maintenance", 280.89999999999998, "Completed", "Emil Fejzagić", new DateTime(2023, 9, 17, 15, 29, 45, 133, DateTimeKind.Utc).AddTicks(8517), null },
                    { 4, "Service record for circular saw", null, "Circular Saw", 87.150000000000006, "Pending", "Mirza Zukanović", new DateTime(2023, 9, 17, 15, 29, 45, 133, DateTimeKind.Utc).AddTicks(8519), 2 },
                    { 5, "Service record for cordless Dril", null, "Cordless Dril", 12.35, "Completed", "Mirza Zukanović", new DateTime(2023, 9, 17, 15, 29, 45, 133, DateTimeKind.Utc).AddTicks(8521), 6 }
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
