using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingLikeApp.Migrations
{
    public partial class ApartmetnInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Stars",
                table: "Apartments",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(1,0)");

            migrationBuilder.AlterColumn<int>(
                name: "DaysUntilCancelEnds",
                table: "Apartments",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(2,0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Stars",
                table: "Apartments",
                type: "decimal(1,0)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "DaysUntilCancelEnds",
                table: "Apartments",
                type: "decimal(2,0)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
