using Microsoft.EntityFrameworkCore.Migrations;

namespace RizzyCoBE.Migrations
{
    public partial class ThirdVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Namse",
                table: "Missions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Namse",
                table: "Missions",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
