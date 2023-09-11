using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Storage.Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Order : Migration
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
                    OrderDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                table: "Supplier",
                columns: new[] { "Id", "Address", "City", "Country", "DateCreated", "DateModified", "Email", "Name", "Phone", "retired" },
                values: new object[,]
                {
                    { 1, "Address 1", "City 1", "Country 1", new DateTime(2023, 9, 11, 22, 32, 37, 178, DateTimeKind.Utc).AddTicks(6661), null, "email1@example.com", "Supplier 1", "Phone 1", false },
                    { 2, "Address 2", "City 2", "Country 2", new DateTime(2023, 9, 11, 22, 32, 37, 178, DateTimeKind.Utc).AddTicks(6672), null, "email2@example.com", "Supplier 2", "Phone 2", false }
                });

            migrationBuilder.InsertData(
                table: "Article",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "InStock", "Name", "OrderRequired", "Price", "Quantity", "SupplierId", "retired" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 9, 11, 22, 32, 37, 178, DateTimeKind.Utc).AddTicks(6756), null, "Desc 1", true, "Article 1", false, 10.0, 1, 1, false },
                    { 2, new DateTime(2023, 9, 11, 22, 32, 37, 178, DateTimeKind.Utc).AddTicks(6764), null, "Desc 2", true, "Article 2", false, 20.0, 2, 1, false },
                    { 3, new DateTime(2023, 9, 11, 22, 32, 37, 178, DateTimeKind.Utc).AddTicks(6768), null, "Desc 3", true, "Article 3", false, 30.0, 3, 1, false },
                    { 4, new DateTime(2023, 9, 11, 22, 32, 37, 178, DateTimeKind.Utc).AddTicks(6771), null, "Desc 4", true, "Article 4", false, 40.0, 4, 1, false },
                    { 5, new DateTime(2023, 9, 11, 22, 32, 37, 178, DateTimeKind.Utc).AddTicks(6774), null, "Desc 5", true, "Article 5", false, 50.0, 5, 2, false },
                    { 6, new DateTime(2023, 9, 11, 22, 32, 37, 178, DateTimeKind.Utc).AddTicks(6780), null, "Desc 6", true, "Article 6", false, 60.0, 6, 2, false }
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
