using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class _11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GamePlayerColors",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameID = table.Column<int>(nullable: true),
                    PlayerColorID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlayerColors", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GamePlayerColors_Games_GameID",
                        column: x => x.GameID,
                        principalTable: "Games",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GamePlayerColors_PlayerColors_PlayerColorID",
                        column: x => x.PlayerColorID,
                        principalTable: "PlayerColors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GamePlayerColors_GameID",
                table: "GamePlayerColors",
                column: "GameID");

            migrationBuilder.CreateIndex(
                name: "IX_GamePlayerColors_PlayerColorID",
                table: "GamePlayerColors",
                column: "PlayerColorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GamePlayerColors");
        }
    }
}
