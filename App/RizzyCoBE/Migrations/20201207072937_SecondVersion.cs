using Microsoft.EntityFrameworkCore.Migrations;

namespace RizzyCoBE.Migrations
{
    public partial class SecondVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerColor_Games_GameID",
                table: "PlayerColor");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_PlayerColor_PlayerColorID",
                table: "Players");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayerColor",
                table: "PlayerColor");

            migrationBuilder.RenameTable(
                name: "PlayerColor",
                newName: "PlayerColors");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerColor_GameID",
                table: "PlayerColors",
                newName: "IX_PlayerColors_GameID");

            migrationBuilder.AddColumn<string>(
                name: "Namse",
                table: "Missions",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayerColors",
                table: "PlayerColors",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerColors_Games_GameID",
                table: "PlayerColors",
                column: "GameID",
                principalTable: "Games",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_PlayerColors_PlayerColorID",
                table: "Players",
                column: "PlayerColorID",
                principalTable: "PlayerColors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerColors_Games_GameID",
                table: "PlayerColors");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_PlayerColors_PlayerColorID",
                table: "Players");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayerColors",
                table: "PlayerColors");

            migrationBuilder.DropColumn(
                name: "Namse",
                table: "Missions");

            migrationBuilder.RenameTable(
                name: "PlayerColors",
                newName: "PlayerColor");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerColors_GameID",
                table: "PlayerColor",
                newName: "IX_PlayerColor_GameID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayerColor",
                table: "PlayerColor",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerColor_Games_GameID",
                table: "PlayerColor",
                column: "GameID",
                principalTable: "Games",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_PlayerColor_PlayerColorID",
                table: "Players",
                column: "PlayerColorID",
                principalTable: "PlayerColor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
