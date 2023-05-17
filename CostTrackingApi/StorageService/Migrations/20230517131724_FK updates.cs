using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorageService.Migrations
{
    /// <inheritdoc />
    public partial class FKupdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_Supplier_SupplierId",
                table: "Article");

            migrationBuilder.DropIndex(
                name: "IX_Article_SupplierId",
                table: "Article");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
