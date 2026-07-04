using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kara.Migrations
{
    /// <inheritdoc />
    public partial class InsertedNewTable1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActionTypeId",
                table: "Logs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Actions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Logs_ActionTypeId",
                table: "Logs",
                column: "ActionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Actions_Name",
                table: "Actions",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Actions_ActionTypeId",
                table: "Logs",
                column: "ActionTypeId",
                principalTable: "Actions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Actions_ActionTypeId",
                table: "Logs");

            migrationBuilder.DropTable(
                name: "Actions");

            migrationBuilder.DropIndex(
                name: "IX_Logs_ActionTypeId",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "ActionTypeId",
                table: "Logs");
        }
    }
}
