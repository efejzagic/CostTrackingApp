using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Equipment.Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class newseeddata : Migration
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
                    { 1, 1, "SN: KPC200", "Mostar Construction Site", "Komatsu PC200 excavator", false },
                    { 2, 2, "SN: C320d", "Sarajevo", "Caterpillar 320d excavator", false },
                    { 3, 2, "SN: VEC210D", "Sarajevo", "Volvo EC210D excavator", false },
                    { 4, 2, "SN: L132hc", "Sarajevo", "Liebherr 132 hc tower crane", false },
                    { 5, 1, "SN: SSP1800", "Mostar", "Schwing concrete pump", false },
                    { 6, 3, "SN: CAP300", "Konjic", "Caterpillar asphalt paver", false },
                    { 7, 3, "SN: C797", "Konjic", "Caterpillar dump truck", false },
                    { 8, 3, "SN: BAC200", "Konjic", "BOMAG roller compactor", false },
                    { 9, 3, "SN: MTGS", "Konjic", "MAN concrete roller truck", false },
                    { 10, 3, "SN: Kd65", "Konjic", "Komatsu bulldozer", false }
                });

            migrationBuilder.InsertData(
                table: "Tool",
                columns: new[] { "Id", "ConstructionSiteId", "Description", "Location", "Title", "retired" },
                values: new object[,]
                {
                    { 1, 1, "", "Mostar", "Hammer", false },
                    { 2, 1, "", "Mostar", "Level", false },
                    { 3, 1, "", "Mostar", "Chisel", false },
                    { 4, 1, "", "Mostar", "Mallet", false },
                    { 5, 1, "Phillips", "Mostar", "Screwdriver Set", false },
                    { 6, 1, "", "Mostar", "Cordless Dril", false },
                    { 7, 2, "", "Sarajevo", "Jigsaw", false },
                    { 8, 2, "", "Sarajevo", "Heat Gun", false },
                    { 9, 2, "", "Sarajevo", "Table Saw", false },
                    { 10, 2, "", "Sarajevo", "Belt sander", false },
                    { 11, 2, "", "Sarajevo", "Router", false },
                    { 12, 2, "", "Sarajevo", "Hammer", false },
                    { 13, 3, "", "Konjic", "Utility Bar", false },
                    { 14, 3, "Flat", "Konjic", "Screwdriver set", false },
                    { 15, 3, "", "Konjic", "Circular Saw", false },
                    { 16, 3, "", "Konjic", "Nail Gun", false },
                    { 17, 3, "", "Konjic", "Chalk Line", false }
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
