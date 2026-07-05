using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kara.Migrations
{
    /// <inheritdoc />
    public partial class UodatedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_LargeCategoryId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Categories_LargeCategoryCategoryId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_LargeCategoryCategoryId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "LargeCategoryCategoryId",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "LargeCategoryId",
                table: "Categories",
                newName: "ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_LargeCategoryId",
                table: "Categories",
                newName: "IX_Categories_ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentId",
                table: "Categories",
                column: "ParentId",
                principalTable: "Categories",
                principalColumn: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentId",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "Categories",
                newName: "LargeCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_ParentId",
                table: "Categories",
                newName: "IX_Categories_LargeCategoryId");

            migrationBuilder.AddColumn<int>(
                name: "LargeCategoryCategoryId",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_LargeCategoryCategoryId",
                table: "Items",
                column: "LargeCategoryCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_LargeCategoryId",
                table: "Categories",
                column: "LargeCategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Categories_LargeCategoryCategoryId",
                table: "Items",
                column: "LargeCategoryCategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId");
        }
    }
}
