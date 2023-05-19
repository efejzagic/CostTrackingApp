using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorageService.Migrations
{
    /// <inheritdoc />
    public partial class Foreignkeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 5, 19, 7, 51, 7, 866, DateTimeKind.Utc).AddTicks(939));

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2023, 5, 19, 7, 51, 7, 866, DateTimeKind.Utc).AddTicks(942));

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2023, 5, 19, 7, 51, 7, 866, DateTimeKind.Utc).AddTicks(943));

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2023, 5, 19, 7, 51, 7, 866, DateTimeKind.Utc).AddTicks(945));

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2023, 5, 19, 7, 51, 7, 866, DateTimeKind.Utc).AddTicks(946));

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2023, 5, 19, 7, 51, 7, 866, DateTimeKind.Utc).AddTicks(949));

            migrationBuilder.UpdateData(
                table: "Supplier",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 5, 19, 7, 51, 7, 866, DateTimeKind.Utc).AddTicks(885));

            migrationBuilder.UpdateData(
                table: "Supplier",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2023, 5, 19, 7, 51, 7, 866, DateTimeKind.Utc).AddTicks(894));

            migrationBuilder.CreateIndex(
                name: "IX_Article_SupplierId",
                table: "Article",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Article_Supplier_SupplierId",
                table: "Article",
                column: "SupplierId",
                principalTable: "Supplier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_Supplier_SupplierId",
                table: "Article");

            migrationBuilder.DropIndex(
                name: "IX_Article_SupplierId",
                table: "Article");

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 5, 19, 7, 40, 51, 44, DateTimeKind.Utc).AddTicks(6680));

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2023, 5, 19, 7, 40, 51, 44, DateTimeKind.Utc).AddTicks(6690));

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2023, 5, 19, 7, 40, 51, 44, DateTimeKind.Utc).AddTicks(6694));

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2023, 5, 19, 7, 40, 51, 44, DateTimeKind.Utc).AddTicks(6697));

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2023, 5, 19, 7, 40, 51, 44, DateTimeKind.Utc).AddTicks(6700));

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2023, 5, 19, 7, 40, 51, 44, DateTimeKind.Utc).AddTicks(6707));

            migrationBuilder.UpdateData(
                table: "Supplier",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 5, 19, 7, 40, 51, 44, DateTimeKind.Utc).AddTicks(6502));

            migrationBuilder.UpdateData(
                table: "Supplier",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2023, 5, 19, 7, 40, 51, 44, DateTimeKind.Utc).AddTicks(6515));
        }
    }
}
