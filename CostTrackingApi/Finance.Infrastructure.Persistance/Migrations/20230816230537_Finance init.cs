using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Finance.Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Financeinit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Expense",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    ReferenceId = table.Column<int>(type: "integer", nullable: false),
                    ConstructionSiteId = table.Column<int>(type: "integer", nullable: false),
                    MachineryId = table.Column<int>(type: "integer", nullable: false),
                    ToolId = table.Column<int>(type: "integer", nullable: false),
                    MaintenanceRecordId = table.Column<int>(type: "integer", nullable: false),
                    ArticleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expense", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    ConstructionSiteId = table.Column<int>(type: "integer", nullable: false),
                    MachineryId = table.Column<int>(type: "integer", nullable: false),
                    ToolId = table.Column<int>(type: "integer", nullable: false),
                    MaintenanceRecordId = table.Column<int>(type: "integer", nullable: false),
                    ArticleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    InvoiceId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceItem_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Expense",
                columns: new[] { "Id", "Amount", "ArticleId", "ConstructionSiteId", "Date", "Description", "MachineryId", "MaintenanceRecordId", "ReferenceId", "ToolId", "Type" },
                values: new object[,]
                {
                    { 1, 325.33m, 0, 0, new DateTime(2023, 8, 16, 23, 5, 37, 47, DateTimeKind.Utc).AddTicks(7811), " Expense type 1", 0, 11, 188, 0, 1 },
                    { 2, 325.33m, 0, 7, new DateTime(2023, 8, 16, 23, 5, 37, 47, DateTimeKind.Utc).AddTicks(7823), " Expense type 2", 0, 0, 126, 0, 0 }
                });

            migrationBuilder.InsertData(
                table: "Invoice",
                columns: new[] { "Id", "Amount", "ArticleId", "ConstructionSiteId", "Date", "DueDate", "MachineryId", "MaintenanceRecordId", "ToolId" },
                values: new object[,]
                {
                    { 1, 1298.92m, 1, 0, new DateTime(2023, 8, 16, 23, 5, 37, 47, DateTimeKind.Utc).AddTicks(7600), new DateTime(2023, 8, 16, 23, 5, 37, 47, DateTimeKind.Utc).AddTicks(7604), 0, 0, 0 },
                    { 2, 498.92m, 0, 0, new DateTime(2023, 8, 16, 23, 5, 37, 47, DateTimeKind.Utc).AddTicks(7624), new DateTime(2023, 8, 16, 23, 5, 37, 47, DateTimeKind.Utc).AddTicks(7625), 0, 12, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItem_InvoiceId",
                table: "InvoiceItem",
                column: "InvoiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expense");

            migrationBuilder.DropTable(
                name: "InvoiceItem");

            migrationBuilder.DropTable(
                name: "Invoice");
        }
    }
}
