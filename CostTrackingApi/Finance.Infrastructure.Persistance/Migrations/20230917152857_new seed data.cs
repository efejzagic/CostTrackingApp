using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Finance.Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class newseeddata : Migration
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
                    ReferenceId = table.Column<int>(type: "integer", nullable: false),
                    ConstructionSiteId = table.Column<int>(type: "integer", nullable: true),
                    MachineryId = table.Column<int>(type: "integer", nullable: true),
                    ToolId = table.Column<int>(type: "integer", nullable: true),
                    MaintenanceRecordId = table.Column<int>(type: "integer", nullable: true),
                    ArticleId = table.Column<int>(type: "integer", nullable: true),
                    OrderId = table.Column<int>(type: "integer", nullable: true)
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
                    ArticleId = table.Column<int>(type: "integer", nullable: false),
                    OrderId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    ExpenseId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseItems_Expense_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "Expense",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    InvoiceId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Expense",
                columns: new[] { "Id", "Amount", "ArticleId", "ConstructionSiteId", "Date", "Description", "MachineryId", "MaintenanceRecordId", "OrderId", "ReferenceId", "ToolId" },
                values: new object[,]
                {
                    { 1, 1059.12m, null, 1, new DateTime(2023, 9, 17, 15, 28, 57, 39, DateTimeKind.Utc).AddTicks(3710), "Mostar Olymic pool expenses", null, null, null, 1112, null },
                    { 2, 87.8m, null, null, new DateTime(2023, 9, 17, 15, 28, 57, 39, DateTimeKind.Utc).AddTicks(3716), "Tool purchase", null, null, null, 132, 2 },
                    { 3, 188.07m, null, null, new DateTime(2023, 9, 17, 15, 28, 57, 39, DateTimeKind.Utc).AddTicks(3719), "Circular saw service", null, 4, null, 132, null }
                });

            migrationBuilder.InsertData(
                table: "Invoice",
                columns: new[] { "Id", "Amount", "ArticleId", "ConstructionSiteId", "Date", "DueDate", "MachineryId", "MaintenanceRecordId", "OrderId", "ToolId" },
                values: new object[] { 1, 1298.92m, 0, 0, new DateTime(2023, 9, 17, 15, 28, 57, 39, DateTimeKind.Utc).AddTicks(3564), new DateTime(2023, 9, 17, 15, 28, 57, 39, DateTimeKind.Utc).AddTicks(3568), 0, 0, 1, 0 });

            migrationBuilder.InsertData(
                table: "ExpenseItems",
                columns: new[] { "Id", "Amount", "Description", "ExpenseId" },
                values: new object[,]
                {
                    { 1, 112m, "Fuel for Komatsu PC200 excavator", 1 },
                    { 2, 212.8m, "Fuel for Caterpillar 320d excavator", 1 },
                    { 3, 723.2m, "Food", 1 },
                    { 4, 10.5m, "Hydraulic fluid for Komatsu PC200 excavator", 1 },
                    { 5, 11.12m, "Renting dump truck", 1 },
                    { 6, 87.8m, "Mallet purchase", 2 },
                    { 7, 87.15m, "Circular Saw", 3 },
                    { 8, 100.92m, "Oil", 3 }
                });

            migrationBuilder.InsertData(
                table: "InvoiceItems",
                columns: new[] { "Id", "Amount", "Description", "InvoiceId" },
                values: new object[,]
                {
                    { 1, 500m, "Cement", 1 },
                    { 2, 350m, "Parquet floor", 1 },
                    { 3, 448.92m, "Bricks", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseItems_ExpenseId",
                table: "ExpenseItems",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_InvoiceId",
                table: "InvoiceItems",
                column: "InvoiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpenseItems");

            migrationBuilder.DropTable(
                name: "InvoiceItems");

            migrationBuilder.DropTable(
                name: "Expense");

            migrationBuilder.DropTable(
                name: "Invoice");
        }
    }
}
