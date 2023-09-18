using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Storage.Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class NewStorageSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ShippingDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OrderComplete = table.Column<bool>(type: "boolean", nullable: false),
                    TotalAmount = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    retired = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ArticleId = table.Column<int>(type: "integer", nullable: false),
                    ArticleName = table.Column<string>(type: "text", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    PricePerItem = table.Column<double>(type: "double precision", nullable: false),
                    OrderId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    SupplierId = table.Column<int>(type: "integer", nullable: false),
                    OrderRequired = table.Column<bool>(type: "boolean", nullable: false),
                    InStock = table.Column<bool>(type: "boolean", nullable: false),
                    retired = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Article_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "OrderComplete", "OrderDate", "ShippingDate", "TotalAmount" },
                values: new object[,]
                {
                    { 1, false, new DateTime(2023, 9, 17, 14, 35, 24, 206, DateTimeKind.Utc).AddTicks(7056), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.0 },
                    { 2, false, new DateTime(2023, 9, 17, 14, 35, 24, 206, DateTimeKind.Utc).AddTicks(7060), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.0 }
                });

            migrationBuilder.InsertData(
                table: "Supplier",
                columns: new[] { "Id", "Address", "City", "Country", "DateCreated", "DateModified", "Email", "Name", "Phone", "retired" },
                values: new object[,]
                {
                    { 1, "Address", "Konjic", "Bosnia and Herzegovina", new DateTime(2023, 9, 17, 14, 35, 24, 206, DateTimeKind.Utc).AddTicks(6817), null, "bmaterial@example.com", "Building Material Supplier", "+387891010", false },
                    { 2, "Address 2", "Sarajevo", "Bosnia and Herzegovina", new DateTime(2023, 9, 17, 14, 35, 24, 206, DateTimeKind.Utc).AddTicks(6827), null, "wood@example.com", "Wood Supplier", "+387891011", false },
                    { 3, "Address", "Mostar", "Bosnia and Herzegovina", new DateTime(2023, 9, 17, 14, 35, 24, 206, DateTimeKind.Utc).AddTicks(6830), null, "insulation@example.com", "Insulation Supplier", "+387891012", false },
                    { 4, "Address", "Bihać", "Bosnia and Herzegovina", new DateTime(2023, 9, 17, 14, 35, 24, 206, DateTimeKind.Utc).AddTicks(6833), null, "rmaterial@example.com", "Roof Material Supplier", "+387891013", false },
                    { 5, "Address", "Split", "Croatia", new DateTime(2023, 9, 17, 14, 35, 24, 206, DateTimeKind.Utc).AddTicks(6835), null, "gmaterial@example.com", "Glass Material Supplier", "+385891014", false },
                    { 6, "Address", "Novi Sad", "Serbia", new DateTime(2023, 9, 17, 14, 35, 24, 206, DateTimeKind.Utc).AddTicks(6841), null, "ematerial@example.com", "Electrical Material Supplier", "+381891014", false }
                });

            migrationBuilder.InsertData(
                table: "Article",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "InStock", "Name", "OrderRequired", "Price", "Quantity", "SupplierId", "retired" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 9, 17, 14, 35, 24, 206, DateTimeKind.Utc).AddTicks(6915), null, "Cement article", true, "Cement", false, 5.0, 20, 1, false },
                    { 2, new DateTime(2023, 9, 17, 14, 35, 24, 206, DateTimeKind.Utc).AddTicks(6924), null, "Walnut flooring", true, "Parquet floor", false, 32.5, 13, 2, false },
                    { 3, new DateTime(2023, 9, 17, 14, 35, 24, 206, DateTimeKind.Utc).AddTicks(6926), null, "Rods", true, "Iron Reinforcement", false, 30.0, 3, 1, false },
                    { 4, new DateTime(2023, 9, 17, 14, 35, 24, 206, DateTimeKind.Utc).AddTicks(6929), null, "Roof brick red", true, "Brick", false, 2.7999999999999998, 380, 4, false },
                    { 5, new DateTime(2023, 9, 17, 14, 35, 24, 206, DateTimeKind.Utc).AddTicks(6931), null, "Plexiglas", false, "Plexiglas", false, 74.900000000000006, 5, 5, false },
                    { 6, new DateTime(2023, 9, 17, 14, 35, 24, 206, DateTimeKind.Utc).AddTicks(6934), null, "Electric wires 2m", false, "Electric wires", false, 2.2999999999999998, 120, 6, false },
                    { 7, new DateTime(2023, 9, 17, 14, 35, 24, 206, DateTimeKind.Utc).AddTicks(6936), null, "Plasterboard 3x4m", false, "Plasterboard", false, 14.25, 30, 2, false },
                    { 8, new DateTime(2023, 9, 17, 14, 35, 24, 206, DateTimeKind.Utc).AddTicks(6938), null, "Screw 8mm", false, "Screw M8", false, 0.25, 1000, 1, false },
                    { 9, new DateTime(2023, 9, 17, 14, 35, 24, 206, DateTimeKind.Utc).AddTicks(6940), null, "Screw 10mm", false, "Screw M10", false, 0.34999999999999998, 570, 1, false },
                    { 10, new DateTime(2023, 9, 17, 14, 35, 24, 206, DateTimeKind.Utc).AddTicks(7010), null, "Floor insulation", false, "Floor insulation", false, 112.55, 12, 3, false },
                    { 11, new DateTime(2023, 9, 17, 14, 35, 24, 206, DateTimeKind.Utc).AddTicks(7013), null, "Fiber cement siding", false, "Fiber cement siding", false, 17.5, 22, 1, false }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "ArticleId", "ArticleName", "OrderId", "PricePerItem", "Quantity" },
                values: new object[,]
                {
                    { 1, 5, "Plexiglas", 1, 72.0, 20 },
                    { 2, 6, "Electric wires", 2, 2.0, 100 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Article_SupplierId",
                table: "Article",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Article");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Supplier");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
