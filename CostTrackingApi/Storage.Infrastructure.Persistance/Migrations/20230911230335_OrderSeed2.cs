using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Storage.Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class OrderSeed2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 9, 11, 22, 43, 8, 876, DateTimeKind.Utc).AddTicks(1326));

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2023, 9, 11, 22, 43, 8, 876, DateTimeKind.Utc).AddTicks(1333));

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2023, 9, 11, 22, 43, 8, 876, DateTimeKind.Utc).AddTicks(1336));

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2023, 9, 11, 22, 43, 8, 876, DateTimeKind.Utc).AddTicks(1338));

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2023, 9, 11, 22, 43, 8, 876, DateTimeKind.Utc).AddTicks(1341));

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2023, 9, 11, 22, 43, 8, 876, DateTimeKind.Utc).AddTicks(1346));

            migrationBuilder.UpdateData(
                table: "Supplier",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 9, 11, 22, 43, 8, 876, DateTimeKind.Utc).AddTicks(1242));

            migrationBuilder.UpdateData(
                table: "Supplier",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2023, 9, 11, 22, 43, 8, 876, DateTimeKind.Utc).AddTicks(1255));
        }
    }
}
