using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EquipmentService.Migrations
{
    /// <inheritdoc />
    public partial class maintenance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Maintenance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    MaintenanceDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    retired = table.Column<bool>(type: "boolean", nullable: false),
                    MachineryId = table.Column<int>(type: "integer", nullable: true),
                    ToolId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maintenance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Maintenance_Machinery_MachineryId",
                        column: x => x.MachineryId,
                        principalTable: "Machinery",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Maintenance_Tool_ToolId",
                        column: x => x.ToolId,
                        principalTable: "Tool",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "MachineryServicing",
                keyColumn: "Id",
                keyValue: 1,
                column: "ServiceDate",
                value: new DateTime(2023, 5, 21, 19, 4, 2, 353, DateTimeKind.Utc).AddTicks(9476));

            migrationBuilder.UpdateData(
                table: "MachineryServicing",
                keyColumn: "Id",
                keyValue: 2,
                column: "ServiceDate",
                value: new DateTime(2023, 5, 21, 19, 4, 2, 353, DateTimeKind.Utc).AddTicks(9479));

            migrationBuilder.UpdateData(
                table: "ToolServicing",
                keyColumn: "Id",
                keyValue: 1,
                column: "ServiceDate",
                value: new DateTime(2023, 5, 21, 19, 4, 2, 353, DateTimeKind.Utc).AddTicks(9497));

            migrationBuilder.UpdateData(
                table: "ToolServicing",
                keyColumn: "Id",
                keyValue: 2,
                column: "ServiceDate",
                value: new DateTime(2023, 5, 21, 19, 4, 2, 353, DateTimeKind.Utc).AddTicks(9499));

            migrationBuilder.CreateIndex(
                name: "IX_Maintenance_MachineryId",
                table: "Maintenance",
                column: "MachineryId");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenance_ToolId",
                table: "Maintenance",
                column: "ToolId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Maintenance");

            migrationBuilder.UpdateData(
                table: "MachineryServicing",
                keyColumn: "Id",
                keyValue: 1,
                column: "ServiceDate",
                value: new DateTime(2023, 5, 21, 18, 35, 7, 495, DateTimeKind.Utc).AddTicks(2596));

            migrationBuilder.UpdateData(
                table: "MachineryServicing",
                keyColumn: "Id",
                keyValue: 2,
                column: "ServiceDate",
                value: new DateTime(2023, 5, 21, 18, 35, 7, 495, DateTimeKind.Utc).AddTicks(2599));

            migrationBuilder.UpdateData(
                table: "ToolServicing",
                keyColumn: "Id",
                keyValue: 1,
                column: "ServiceDate",
                value: new DateTime(2023, 5, 21, 18, 35, 7, 495, DateTimeKind.Utc).AddTicks(2618));

            migrationBuilder.UpdateData(
                table: "ToolServicing",
                keyColumn: "Id",
                keyValue: 2,
                column: "ServiceDate",
                value: new DateTime(2023, 5, 21, 18, 35, 7, 495, DateTimeKind.Utc).AddTicks(2621));
        }
    }
}
