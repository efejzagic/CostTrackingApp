using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorageService.Migrations
{
    /// <inheritdoc />
    public partial class Articleretiredadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "retired",
                table: "Article",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "retired",
                table: "Article");
        }
    }
}
