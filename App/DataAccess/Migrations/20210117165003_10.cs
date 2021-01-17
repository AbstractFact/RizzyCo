using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class _10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerColors_Games_GameID",
                table: "PlayerColors");

            migrationBuilder.DropIndex(
                name: "IX_PlayerColors_GameID",
                table: "PlayerColors");

            migrationBuilder.DropColumn(
                name: "GameID",
                table: "PlayerColors");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GameID",
                table: "PlayerColors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerColors_GameID",
                table: "PlayerColors",
                column: "GameID");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerColors_Games_GameID",
                table: "PlayerColors",
                column: "GameID",
                principalTable: "Games",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
