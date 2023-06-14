using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FrancisPetShopMVC.Migrations
{
    /// <inheritdoc />
    public partial class CategoryItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Animals_CategoryId",
                table: "Animals",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Categories_CategoryId",
                table: "Animals",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Categories_CategoryId",
                table: "Animals");

            migrationBuilder.DropIndex(
                name: "IX_Animals_CategoryId",
                table: "Animals");
        }
    }
}
