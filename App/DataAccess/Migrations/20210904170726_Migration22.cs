using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class Migration22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "WonCard",
                table: "Players",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "GameID",
                table: "PlayerCards",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WonCard",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "GameID",
                table: "PlayerCards");
        }
    }
}
