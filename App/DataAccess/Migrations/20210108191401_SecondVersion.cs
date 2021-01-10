using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class SecondVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Territories_Territories_TerritoryID",
                table: "Territories");

            migrationBuilder.DropIndex(
                name: "IX_Territories_TerritoryID",
                table: "Territories");

            migrationBuilder.DropColumn(
                name: "TerritoryID",
                table: "Territories");

            migrationBuilder.AddColumn<bool>(
                name: "Available",
                table: "PlayerColors",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Available",
                table: "PlayerColors");

            migrationBuilder.AddColumn<int>(
                name: "TerritoryID",
                table: "Territories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Territories_TerritoryID",
                table: "Territories",
                column: "TerritoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Territories_Territories_TerritoryID",
                table: "Territories",
                column: "TerritoryID",
                principalTable: "Territories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
