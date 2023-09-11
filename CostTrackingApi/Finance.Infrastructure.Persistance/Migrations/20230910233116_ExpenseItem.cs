using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Finance.Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ExpenseItem : Migration
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
                    ArticleId = table.Column<int>(type: "integer", nullable: true)
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
                columns: new[] { "Id", "Amount", "ArticleId", "ConstructionSiteId", "Date", "Description", "MachineryId", "MaintenanceRecordId", "ReferenceId", "ToolId" },
                values: new object[,]
                {
                    { 1, 1298.92m, null, 1, new DateTime(2023, 9, 10, 23, 31, 16, 608, DateTimeKind.Utc).AddTicks(5460), "Description 1", null, null, 1112, null },
                    { 2, 498.92m, null, null, new DateTime(2023, 9, 10, 23, 31, 16, 608, DateTimeKind.Utc).AddTicks(5471), "Description 2", null, null, 132, 2 }
                });

            migrationBuilder.InsertData(
                table: "Invoice",
                columns: new[] { "Id", "Amount", "ArticleId", "ConstructionSiteId", "Date", "DueDate", "MachineryId", "MaintenanceRecordId", "ToolId" },
                values: new object[,]
                {
                    { 1, 1298.92m, 1, 0, new DateTime(2023, 9, 10, 23, 31, 16, 608, DateTimeKind.Utc).AddTicks(5038), new DateTime(2023, 9, 10, 23, 31, 16, 608, DateTimeKind.Utc).AddTicks(5043), 0, 0, 0 },
                    { 2, 498.92m, 0, 0, new DateTime(2023, 9, 10, 23, 31, 16, 608, DateTimeKind.Utc).AddTicks(5051), new DateTime(2023, 9, 10, 23, 31, 16, 608, DateTimeKind.Utc).AddTicks(5052), 0, 12, 0 }
                });

            migrationBuilder.InsertData(
                table: "ExpenseItems",
                columns: new[] { "Id", "Amount", "Description", "ExpenseId" },
                values: new object[,]
                {
                    { 1, 112m, "Expense item 1", 1 },
                    { 2, 212.8m, "Expense item 2", 1 },
                    { 3, 723.2m, "Expense item 3", 1 },
                    { 4, 10.5m, "Expense item 4", 1 },
                    { 5, 11.12m, "Expense item 5", 1 },
                    { 6, 87.8m, "Expense item 6", 2 },
                    { 7, 24.3m, "Expense item 7", 2 }
                });

            migrationBuilder.InsertData(
                table: "InvoiceItems",
                columns: new[] { "Id", "Amount", "Description", "InvoiceId" },
                values: new object[,]
                {
                    { 1, 500m, "Item 1", 1 },
                    { 2, 350m, "Item 2", 1 },
                    { 3, 448.92m, "Item 3", 1 },
                    { 4, 200m, "Item 2.1", 2 },
                    { 5, 298.92m, "Item 2.2", 2 }
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
