using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kara.Migrations
{
    /// <inheritdoc />
    public partial class AdditionCategorySteps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Categories_CategoryId",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Items",
                newName: "SmallCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Items_CategoryId",
                table: "Items",
                newName: "IX_Items_SmallCategoryId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Categories",
                newName: "CategoryId");

            migrationBuilder.AddColumn<int>(
                name: "LargeCategoryCategoryId",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Categories",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LargeCategoryId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_LargeCategoryCategoryId",
                table: "Items",
                column: "LargeCategoryCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_LargeCategoryId",
                table: "Categories",
                column: "LargeCategoryId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Categories_SmallCategoryId",
                table: "Items",
                column: "SmallCategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_LargeCategoryId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Categories_LargeCategoryCategoryId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Categories_SmallCategoryId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_LargeCategoryCategoryId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Categories_LargeCategoryId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "LargeCategoryCategoryId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "LargeCategoryId",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "SmallCategoryId",
                table: "Items",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Items_SmallCategoryId",
                table: "Items",
                newName: "IX_Items_CategoryId");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Categories",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Categories_CategoryId",
                table: "Items",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
