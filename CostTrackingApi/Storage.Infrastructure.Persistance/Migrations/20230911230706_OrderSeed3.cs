using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Storage.Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class OrderSeed3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 9, 11, 23, 7, 5, 846, DateTimeKind.Utc).AddTicks(6435));

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2023, 9, 11, 23, 7, 5, 846, DateTimeKind.Utc).AddTicks(6444));

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2023, 9, 11, 23, 7, 5, 846, DateTimeKind.Utc).AddTicks(6447));

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2023, 9, 11, 23, 7, 5, 846, DateTimeKind.Utc).AddTicks(6450));

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2023, 9, 11, 23, 7, 5, 846, DateTimeKind.Utc).AddTicks(6452));

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2023, 9, 11, 23, 7, 5, 846, DateTimeKind.Utc).AddTicks(6457));

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "OrderDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 9, 11, 23, 7, 5, 846, DateTimeKind.Utc).AddTicks(6499) },
                    { 2, new DateTime(2023, 9, 11, 23, 7, 5, 846, DateTimeKind.Utc).AddTicks(6503) }
                });

            migrationBuilder.UpdateData(
                table: "Supplier",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 9, 11, 23, 7, 5, 846, DateTimeKind.Utc).AddTicks(6335));

            migrationBuilder.UpdateData(
                table: "Supplier",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2023, 9, 11, 23, 7, 5, 846, DateTimeKind.Utc).AddTicks(6345));

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "ArticleId", "OrderId", "PricePerItem", "Quantity" },
                values: new object[,]
                {
                    { 1, 5, 1, 20.5, 20 },
                    { 2, 6, 2, 17.399999999999999, 13 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 9, 11, 23, 3, 35, 117, DateTimeKind.Utc).AddTicks(5176));

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2023, 9, 11, 23, 3, 35, 117, DateTimeKind.Utc).AddTicks(5185));

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2023, 9, 11, 23, 3, 35, 117, DateTimeKind.Utc).AddTicks(5189));

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2023, 9, 11, 23, 3, 35, 117, DateTimeKind.Utc).AddTicks(5192));

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2023, 9, 11, 23, 3, 35, 117, DateTimeKind.Utc).AddTicks(5195));

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2023, 9, 11, 23, 3, 35, 117, DateTimeKind.Utc).AddTicks(5201));

            migrationBuilder.UpdateData(
                table: "Supplier",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 9, 11, 23, 3, 35, 117, DateTimeKind.Utc).AddTicks(5051));

            migrationBuilder.UpdateData(
                table: "Supplier",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2023, 9, 11, 23, 3, 35, 117, DateTimeKind.Utc).AddTicks(5067));
        }
    }
}
