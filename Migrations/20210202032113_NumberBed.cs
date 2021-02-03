using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingLikeApp.Migrations
{
    public partial class NumberBed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Numbers_Beds_BedId",
                table: "Numbers");

            migrationBuilder.DropIndex(
                name: "IX_Numbers_BedId",
                table: "Numbers");

            migrationBuilder.CreateTable(
                name: "NumberBed",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberId = table.Column<int>(type: "int", nullable: false),
                    BedId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumberBed", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NumberBed_Beds_BedId",
                        column: x => x.BedId,
                        principalTable: "Beds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NumberBed_Numbers_NumberId",
                        column: x => x.NumberId,
                        principalTable: "Numbers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NumberBed_BedId",
                table: "NumberBed",
                column: "BedId");

            migrationBuilder.CreateIndex(
                name: "IX_NumberBed_NumberId",
                table: "NumberBed",
                column: "NumberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NumberBed");

            migrationBuilder.CreateIndex(
                name: "IX_Numbers_BedId",
                table: "Numbers",
                column: "BedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Numbers_Beds_BedId",
                table: "Numbers",
                column: "BedId",
                principalTable: "Beds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
