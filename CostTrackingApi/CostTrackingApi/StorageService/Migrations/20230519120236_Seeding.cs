using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StorageService.Migrations
{
    /// <inheritdoc />
    public partial class Seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    { 1, "Address 1", "City 1", "Country 1", new DateTime(2023, 5, 19, 12, 2, 36, 270, DateTimeKind.Utc).AddTicks(4095), null, "email1@example.com", "Supplier 1", "Phone 1", false },
                    { 2, "Address 2", "City 2", "Country 2", new DateTime(2023, 5, 19, 12, 2, 36, 270, DateTimeKind.Utc).AddTicks(4151), null, "email2@example.com", "Supplier 2", "Phone 2", false }
                });

            migrationBuilder.InsertData(
                table: "Article",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "Name", "Price", "Quantity", "SupplierId", "retired" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 5, 19, 12, 2, 36, 270, DateTimeKind.Utc).AddTicks(4241), null, "Desc 1", "Article 1", 10.0, 1, 1, false },
                    { 2, new DateTime(2023, 5, 19, 12, 2, 36, 270, DateTimeKind.Utc).AddTicks(4249), null, "Desc 2", "Article 2", 20.0, 2, 1, false },
                    { 3, new DateTime(2023, 5, 19, 12, 2, 36, 270, DateTimeKind.Utc).AddTicks(4341), null, "Desc 3", "Article 3", 30.0, 3, 1, false },
                    { 4, new DateTime(2023, 5, 19, 12, 2, 36, 270, DateTimeKind.Utc).AddTicks(4346), null, "Desc 4", "Article 4", 40.0, 4, 1, false },
                    { 5, new DateTime(2023, 5, 19, 12, 2, 36, 270, DateTimeKind.Utc).AddTicks(4349), null, "Desc 5", "Article 5", 50.0, 5, 2, false },
                    { 6, new DateTime(2023, 5, 19, 12, 2, 36, 270, DateTimeKind.Utc).AddTicks(4356), null, "Desc 6", "Article 6", 60.0, 6, 2, false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Article_SupplierId",
                table: "Article",
                column: "SupplierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Article");

            migrationBuilder.DropTable(
                name: "Supplier");
        }
    }
}
