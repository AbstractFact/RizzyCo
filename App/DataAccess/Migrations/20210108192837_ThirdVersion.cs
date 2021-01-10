using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class ThirdVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Neighbours",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SrcID = table.Column<int>(nullable: true),
                    DstID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Neighbours", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Neighbours_Territories_DstID",
                        column: x => x.DstID,
                        principalTable: "Territories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Neighbours_Territories_SrcID",
                        column: x => x.SrcID,
                        principalTable: "Territories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Neighbours_DstID",
                table: "Neighbours",
                column: "DstID");

            migrationBuilder.CreateIndex(
                name: "IX_Neighbours_SrcID",
                table: "Neighbours",
                column: "SrcID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Neighbours");
        }
    }
}
