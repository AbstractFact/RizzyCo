using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class _12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Available",
                table: "PlayerColors");

            migrationBuilder.AddColumn<bool>(
                name: "Available",
                table: "GamePlayerColors",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Available",
                table: "GamePlayerColors");

            migrationBuilder.AddColumn<bool>(
                name: "Available",
                table: "PlayerColors",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
