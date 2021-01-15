using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class SeventhVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Users_UserID",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_UserID",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Creator",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Games");

            migrationBuilder.AddColumn<int>(
                name: "CreatorID",
                table: "Games",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_CreatorID",
                table: "Games",
                column: "CreatorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Users_CreatorID",
                table: "Games",
                column: "CreatorID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Users_CreatorID",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_CreatorID",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "CreatorID",
                table: "Games");

            migrationBuilder.AddColumn<int>(
                name: "Creator",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Games",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_UserID",
                table: "Games",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Users_UserID",
                table: "Games",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
