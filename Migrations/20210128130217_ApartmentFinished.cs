using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingLikeApp.Migrations
{
    public partial class ApartmentFinished : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Bedrooms",
                table: "Numbers",
                type: "decimal(3,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "LinvingRooms",
                table: "Numbers",
                type: "decimal(3,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "Finished",
                table: "Apartments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bedrooms",
                table: "Numbers");

            migrationBuilder.DropColumn(
                name: "LinvingRooms",
                table: "Numbers");

            migrationBuilder.DropColumn(
                name: "Finished",
                table: "Apartments");
        }
    }
}
