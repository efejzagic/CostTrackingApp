using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EquipmentService.Migrations
{
    /// <inheritdoc />
    public partial class MachineryServicing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "Machinery",
                keyColumn: "Id",
                keyValue: 1,
                column: "ProductionYear",
                value: new DateOnly(2023, 5, 21));

            migrationBuilder.UpdateData(
                table: "Machinery",
                keyColumn: "Id",
                keyValue: 2,
                column: "ProductionYear",
                value: new DateOnly(2023, 5, 21));

            migrationBuilder.CreateIndex(
                name: "IX_MachineryServicing_MachineryId",
                table: "MachineryServicing",
                column: "MachineryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MachineryServicing");

            migrationBuilder.UpdateData(
                table: "Machinery",
                keyColumn: "Id",
                keyValue: 1,
                column: "ProductionYear",
                value: new DateOnly(2023, 5, 20));

            migrationBuilder.UpdateData(
                table: "Machinery",
                keyColumn: "Id",
                keyValue: 2,
                column: "ProductionYear",
                value: new DateOnly(2023, 5, 20));
        }
    }
}
