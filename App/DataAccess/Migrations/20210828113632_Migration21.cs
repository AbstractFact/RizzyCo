using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class Migration21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumArmies",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "NumTerritories",
                table: "Continents");

            migrationBuilder.AddColumn<int>(
                name: "NumTerritories",
                table: "Missions",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumTerritories",
                table: "Missions");

            migrationBuilder.AddColumn<int>(
                name: "NumArmies",
                table: "Missions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumTerritories",
                table: "Continents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
