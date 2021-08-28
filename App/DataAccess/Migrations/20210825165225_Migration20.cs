using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class Migration20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MissionNum",
                table: "Missions");

            migrationBuilder.AddColumn<string>(
                name: "Continent1",
                table: "Missions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Continent2",
                table: "Missions",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Continent3",
                table: "Missions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MissionType",
                table: "Missions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumArmies",
                table: "Missions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TargetPlayerColor",
                table: "Missions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Continent1",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "Continent2",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "Continent3",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "MissionType",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "NumArmies",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "TargetPlayerColor",
                table: "Missions");

            migrationBuilder.AddColumn<int>(
                name: "MissionNum",
                table: "Missions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
