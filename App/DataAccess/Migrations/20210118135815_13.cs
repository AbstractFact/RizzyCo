using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class _13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Players_PlayerID",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Territories_Players_PlayerID",
                table: "Territories");

            migrationBuilder.DropIndex(
                name: "IX_Territories_PlayerID",
                table: "Territories");

            migrationBuilder.DropIndex(
                name: "IX_Cards_PlayerID",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "PlayerID",
                table: "Territories");

            migrationBuilder.DropColumn(
                name: "PlayerID",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "Taken",
                table: "Cards");

            migrationBuilder.CreateTable(
                name: "GameCards",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameID = table.Column<int>(nullable: true),
                    CardID = table.Column<int>(nullable: true),
                    PlayerID = table.Column<int>(nullable: true)
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
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameID = table.Column<int>(nullable: true),
                    MissionID = table.Column<int>(nullable: true)
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
                name: "GamePlayerTerritories",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameID = table.Column<int>(nullable: true),
                    PlayerID = table.Column<int>(nullable: true),
                    TerritoryID = table.Column<int>(nullable: true),
                    Armies = table.Column<int>(nullable: false)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameCards");

            migrationBuilder.DropTable(
                name: "GameMissions");

            migrationBuilder.DropTable(
                name: "GamePlayerTerritories");

            migrationBuilder.AddColumn<int>(
                name: "PlayerID",
                table: "Territories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlayerID",
                table: "Cards",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Taken",
                table: "Cards",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Territories_PlayerID",
                table: "Territories",
                column: "PlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_PlayerID",
                table: "Cards",
                column: "PlayerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Players_PlayerID",
                table: "Cards",
                column: "PlayerID",
                principalTable: "Players",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Territories_Players_PlayerID",
                table: "Territories",
                column: "PlayerID",
                principalTable: "Players",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
