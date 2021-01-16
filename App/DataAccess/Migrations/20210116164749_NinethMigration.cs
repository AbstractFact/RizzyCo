using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class NinethMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Territories_Maps_MapID",
                table: "Territories");

            migrationBuilder.DropIndex(
                name: "IX_Territories_MapID",
                table: "Territories");

            migrationBuilder.DropColumn(
                name: "MapID",
                table: "Territories");

            migrationBuilder.AddColumn<int>(
                name: "ContinentID",
                table: "Territories",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MissionNum",
                table: "Missions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Taken",
                table: "Cards",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Continents",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<int>(nullable: false),
                    NumTerritories = table.Column<int>(nullable: false),
                    MapID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Continents", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Continents_Maps_MapID",
                        column: x => x.MapID,
                        principalTable: "Maps",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Territories_ContinentID",
                table: "Territories",
                column: "ContinentID");

            migrationBuilder.CreateIndex(
                name: "IX_Continents_MapID",
                table: "Continents",
                column: "MapID");

            migrationBuilder.AddForeignKey(
                name: "FK_Territories_Continents_ContinentID",
                table: "Territories",
                column: "ContinentID",
                principalTable: "Continents",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Territories_Continents_ContinentID",
                table: "Territories");

            migrationBuilder.DropTable(
                name: "Continents");

            migrationBuilder.DropIndex(
                name: "IX_Territories_ContinentID",
                table: "Territories");

            migrationBuilder.DropColumn(
                name: "ContinentID",
                table: "Territories");

            migrationBuilder.DropColumn(
                name: "MissionNum",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "Taken",
                table: "Cards");

            migrationBuilder.AddColumn<int>(
                name: "MapID",
                table: "Territories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Territories_MapID",
                table: "Territories",
                column: "MapID");

            migrationBuilder.AddForeignKey(
                name: "FK_Territories_Maps_MapID",
                table: "Territories",
                column: "MapID",
                principalTable: "Maps",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
