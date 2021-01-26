using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingLikeApp.Migrations
{
    public partial class BedsExc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Beds",
                table: "Numbers");

            migrationBuilder.AlterColumn<decimal>(
                name: "BedQuantity",
                table: "Numbers",
                type: "decimal(3,0)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BedQuantity",
                table: "Numbers",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,0)");

            migrationBuilder.AddColumn<decimal>(
                name: "Beds",
                table: "Numbers",
                type: "decimal(3,0)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
