using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingLikeApp.Migrations
{
    public partial class DecimalToIntUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BreakfestIncluded",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "FreeParking",
                table: "Apartments");

            migrationBuilder.AlterColumn<int>(
                name: "LinvingRooms",
                table: "Numbers",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,0)");

            migrationBuilder.AlterColumn<int>(
                name: "Bedrooms",
                table: "Numbers",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,0)");

            migrationBuilder.AlterColumn<int>(
                name: "BedQuantity",
                table: "Numbers",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,0)");

            migrationBuilder.AlterColumn<int>(
                name: "Bathrooms",
                table: "Numbers",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,0)");

            migrationBuilder.AlterColumn<int>(
                name: "Area",
                table: "Numbers",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,0)");

            migrationBuilder.AlterColumn<int>(
                name: "Parking",
                table: "Apartments",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "Breakfest",
                table: "Apartments",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "LinvingRooms",
                table: "Numbers",
                type: "decimal(3,0)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Bedrooms",
                table: "Numbers",
                type: "decimal(3,0)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "BedQuantity",
                table: "Numbers",
                type: "decimal(3,0)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Bathrooms",
                table: "Numbers",
                type: "decimal(3,0)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Area",
                table: "Numbers",
                type: "decimal(4,0)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "Parking",
                table: "Apartments",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "Breakfest",
                table: "Apartments",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "BreakfestIncluded",
                table: "Apartments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "FreeParking",
                table: "Apartments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
