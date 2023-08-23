using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Finance.Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceItemsinit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItem_Invoice_InvoiceId",
                table: "InvoiceItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceItem",
                table: "InvoiceItem");

            migrationBuilder.RenameTable(
                name: "InvoiceItem",
                newName: "InvoiceItems");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceItem_InvoiceId",
                table: "InvoiceItems",
                newName: "IX_InvoiceItems_InvoiceId");

            migrationBuilder.AlterColumn<int>(
                name: "InvoiceId",
                table: "InvoiceItems",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceItems",
                table: "InvoiceItems",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2023, 8, 23, 8, 42, 35, 883, DateTimeKind.Utc).AddTicks(5291));

            migrationBuilder.UpdateData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2023, 8, 23, 8, 42, 35, 883, DateTimeKind.Utc).AddTicks(5300));

            migrationBuilder.UpdateData(
                table: "Invoice",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "DueDate" },
                values: new object[] { new DateTime(2023, 8, 23, 8, 42, 35, 883, DateTimeKind.Utc).AddTicks(5062), new DateTime(2023, 8, 23, 8, 42, 35, 883, DateTimeKind.Utc).AddTicks(5067) });

            migrationBuilder.UpdateData(
                table: "Invoice",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Date", "DueDate" },
                values: new object[] { new DateTime(2023, 8, 23, 8, 42, 35, 883, DateTimeKind.Utc).AddTicks(5080), new DateTime(2023, 8, 23, 8, 42, 35, 883, DateTimeKind.Utc).AddTicks(5082) });

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

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItems_Invoice_InvoiceId",
                table: "InvoiceItems",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItems_Invoice_InvoiceId",
                table: "InvoiceItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceItems",
                table: "InvoiceItems");

            migrationBuilder.DeleteData(
                table: "InvoiceItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "InvoiceItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "InvoiceItems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "InvoiceItems",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "InvoiceItems",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.RenameTable(
                name: "InvoiceItems",
                newName: "InvoiceItem");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceItems_InvoiceId",
                table: "InvoiceItem",
                newName: "IX_InvoiceItem_InvoiceId");

            migrationBuilder.AlterColumn<int>(
                name: "InvoiceId",
                table: "InvoiceItem",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceItem",
                table: "InvoiceItem",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2023, 8, 16, 23, 5, 37, 47, DateTimeKind.Utc).AddTicks(7811));

            migrationBuilder.UpdateData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2023, 8, 16, 23, 5, 37, 47, DateTimeKind.Utc).AddTicks(7823));

            migrationBuilder.UpdateData(
                table: "Invoice",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "DueDate" },
                values: new object[] { new DateTime(2023, 8, 16, 23, 5, 37, 47, DateTimeKind.Utc).AddTicks(7600), new DateTime(2023, 8, 16, 23, 5, 37, 47, DateTimeKind.Utc).AddTicks(7604) });

            migrationBuilder.UpdateData(
                table: "Invoice",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Date", "DueDate" },
                values: new object[] { new DateTime(2023, 8, 16, 23, 5, 37, 47, DateTimeKind.Utc).AddTicks(7624), new DateTime(2023, 8, 16, 23, 5, 37, 47, DateTimeKind.Utc).AddTicks(7625) });

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItem_Invoice_InvoiceId",
                table: "InvoiceItem",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "Id");
        }
    }
}
