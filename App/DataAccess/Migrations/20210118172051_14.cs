using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class _14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Users_CreatorID",
                table: "Games");

            migrationBuilder.DropTable(
                name: "GameCards");

            migrationBuilder.DropTable(
                name: "GameMissions");

            migrationBuilder.DropTable(
                name: "GamePlayerColors");

            migrationBuilder.DropTable(
                name: "GamePlayerTerritories");

            migrationBuilder.DropTable(
                name: "GamesUser");

            migrationBuilder.DropIndex(
                name: "IX_Games_CreatorID",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "CreatorID",
                table: "Games");

            migrationBuilder.AddColumn<bool>(
                name: "Creator",
                table: "Players",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OnTurn",
                table: "Players",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "PlayerCards",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerID = table.Column<int>(nullable: true),
                    CardID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerCards", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PlayerCards_Cards_CardID",
                        column: x => x.CardID,
                        principalTable: "Cards",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayerCards_Players_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Players",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlayerTerritories",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerID = table.Column<int>(nullable: true),
                    TerritoryID = table.Column<int>(nullable: true),
                    Armies = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerTerritories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PlayerTerritories_Players_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Players",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayerTerritories_Territories_TerritoryID",
                        column: x => x.TerritoryID,
                        principalTable: "Territories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCards_CardID",
                table: "PlayerCards",
                column: "CardID");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCards_PlayerID",
                table: "PlayerCards",
                column: "PlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerTerritories_PlayerID",
                table: "PlayerTerritories",
                column: "PlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerTerritories_TerritoryID",
                table: "PlayerTerritories",
                column: "TerritoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerCards");

            migrationBuilder.DropTable(
                name: "PlayerTerritories");

            migrationBuilder.DropColumn(
                name: "Creator",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "OnTurn",
                table: "Players");

            migrationBuilder.AddColumn<int>(
                name: "CreatorID",
                table: "Games",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GameCards",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardID = table.Column<int>(type: "int", nullable: true),
                    GameID = table.Column<int>(type: "int", nullable: true),
                    PlayerID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameCards", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GameCards_Cards_CardID",
                        column: x => x.CardID,
                        principalTable: "Cards",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GameCards_Games_GameID",
                        column: x => x.GameID,
                        principalTable: "Games",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GameCards_Players_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Players",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GameMissions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameID = table.Column<int>(type: "int", nullable: true),
                    MissionID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameMissions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GameMissions_Games_GameID",
                        column: x => x.GameID,
                        principalTable: "Games",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GameMissions_Missions_MissionID",
                        column: x => x.MissionID,
                        principalTable: "Missions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GamePlayerColors",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Available = table.Column<bool>(type: "bit", nullable: false),
                    GameID = table.Column<int>(type: "int", nullable: true),
                    PlayerColorID = table.Column<int>(type: "int", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "GamePlayerTerritories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Armies = table.Column<int>(type: "int", nullable: false),
                    GameID = table.Column<int>(type: "int", nullable: true),
                    PlayerID = table.Column<int>(type: "int", nullable: true),
                    TerritoryID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlayerTerritories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GamePlayerTerritories_Games_GameID",
                        column: x => x.GameID,
                        principalTable: "Games",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GamePlayerTerritories_Players_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Players",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GamePlayerTerritories_Territories_TerritoryID",
                        column: x => x.TerritoryID,
                        principalTable: "Territories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GamesUser",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameID = table.Column<int>(type: "int", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamesUser", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GamesUser_Games_GameID",
                        column: x => x.GameID,
                        principalTable: "Games",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GamesUser_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_CreatorID",
                table: "Games",
                column: "CreatorID");

            migrationBuilder.CreateIndex(
                name: "IX_GameCards_CardID",
                table: "GameCards",
                column: "CardID");

            migrationBuilder.CreateIndex(
                name: "IX_GameCards_GameID",
                table: "GameCards",
                column: "GameID");

            migrationBuilder.CreateIndex(
                name: "IX_GameCards_PlayerID",
                table: "GameCards",
                column: "PlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_GameMissions_GameID",
                table: "GameMissions",
                column: "GameID");

            migrationBuilder.CreateIndex(
                name: "IX_GameMissions_MissionID",
                table: "GameMissions",
                column: "MissionID");

            migrationBuilder.CreateIndex(
                name: "IX_GamePlayerColors_GameID",
                table: "GamePlayerColors",
                column: "GameID");

            migrationBuilder.CreateIndex(
                name: "IX_GamePlayerColors_PlayerColorID",
                table: "GamePlayerColors",
                column: "PlayerColorID");

            migrationBuilder.CreateIndex(
                name: "IX_GamePlayerTerritories_GameID",
                table: "GamePlayerTerritories",
                column: "GameID");

            migrationBuilder.CreateIndex(
                name: "IX_GamePlayerTerritories_PlayerID",
                table: "GamePlayerTerritories",
                column: "PlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_GamePlayerTerritories_TerritoryID",
                table: "GamePlayerTerritories",
                column: "TerritoryID");

            migrationBuilder.CreateIndex(
                name: "IX_GamesUser_GameID",
                table: "GamesUser",
                column: "GameID");

            migrationBuilder.CreateIndex(
                name: "IX_GamesUser_UserID",
                table: "GamesUser",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Users_CreatorID",
                table: "Games",
                column: "CreatorID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
