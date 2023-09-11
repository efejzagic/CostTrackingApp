using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Storage.Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class OrderSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                columns: new[] { "DateCreated", "InStock" },
                values: new object[] { new DateTime(2023, 9, 11, 22, 43, 8, 876, DateTimeKind.Utc).AddTicks(1341), false });

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "InStock" },
                values: new object[] { new DateTime(2023, 9, 11, 22, 43, 8, 876, DateTimeKind.Utc).AddTicks(1346), false });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 9, 11, 22, 32, 37, 178, DateTimeKind.Utc).AddTicks(6756));

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2023, 9, 11, 22, 32, 37, 178, DateTimeKind.Utc).AddTicks(6764));

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2023, 9, 11, 22, 32, 37, 178, DateTimeKind.Utc).AddTicks(6768));

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2023, 9, 11, 22, 32, 37, 178, DateTimeKind.Utc).AddTicks(6771));

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "InStock" },
                values: new object[] { new DateTime(2023, 9, 11, 22, 32, 37, 178, DateTimeKind.Utc).AddTicks(6774), true });

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "InStock" },
                values: new object[] { new DateTime(2023, 9, 11, 22, 32, 37, 178, DateTimeKind.Utc).AddTicks(6780), true });

            migrationBuilder.UpdateData(
                table: "Supplier",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 9, 11, 22, 32, 37, 178, DateTimeKind.Utc).AddTicks(6661));

            migrationBuilder.UpdateData(
                table: "Supplier",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2023, 9, 11, 22, 32, 37, 178, DateTimeKind.Utc).AddTicks(6672));
        }
    }
}
