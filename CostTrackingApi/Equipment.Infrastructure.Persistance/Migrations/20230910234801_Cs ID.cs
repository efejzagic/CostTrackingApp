using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Equipment.Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class CsID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Machinery",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    ConstructionSiteId = table.Column<int>(type: "integer", nullable: true),
                    retired = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machinery", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tool",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    ConstructionSiteId = table.Column<int>(type: "integer", nullable: true),
                    retired = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tool", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Machinery",
                columns: new[] { "Id", "ConstructionSiteId", "Description", "Location", "Name", "retired" },
                values: new object[,]
                {
                    { 1, 1, "Description 1", "Loc 1", "Machinery 1", false },
                    { 2, 1, "Description 2", "Loc 2", "Machinery 2", false }
                });

            migrationBuilder.InsertData(
                table: "Tool",
                columns: new[] { "Id", "ConstructionSiteId", "Description", "Location", "Title", "retired" },
                values: new object[,]
                {
                    { 1, 1, "Desc 1", "Loc 1", "Tool 1", false },
                    { 2, null, "Desc 2", "Loc 2", "Tool 2", false }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Machinery");

            migrationBuilder.DropTable(
                name: "Tool");
        }
    }
}
