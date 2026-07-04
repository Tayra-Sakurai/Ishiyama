using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kara.Migrations
{
    /// <inheritdoc />
    public partial class DeletedIsUsed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsUsed",
                table: "Logs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                table: "Logs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
