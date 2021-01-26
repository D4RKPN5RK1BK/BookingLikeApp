using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingLikeApp.Migrations
{
    public partial class Beds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Badrooms",
                table: "Numbers");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Numbers",
                newName: "BedQuantity");

            migrationBuilder.RenameColumn(
                name: "NotUsed",
                table: "Numbers",
                newName: "BedId");

            migrationBuilder.RenameColumn(
                name: "Kitchens",
                table: "Numbers",
                newName: "Beds");

            migrationBuilder.CreateTable(
                name: "Beds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Capacity = table.Column<decimal>(type: "decimal(1,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beds", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Numbers_Beds_BedId",
                table: "Numbers");

            migrationBuilder.DropTable(
                name: "Beds");

            migrationBuilder.DropIndex(
                name: "IX_Numbers_BedId",
                table: "Numbers");

            migrationBuilder.RenameColumn(
                name: "Beds",
                table: "Numbers",
                newName: "Kitchens");

            migrationBuilder.RenameColumn(
                name: "BedQuantity",
                table: "Numbers",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "BedId",
                table: "Numbers",
                newName: "NotUsed");

            migrationBuilder.AddColumn<decimal>(
                name: "Badrooms",
                table: "Numbers",
                type: "decimal(3,0)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
