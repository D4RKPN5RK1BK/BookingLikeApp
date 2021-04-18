using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingLikeApp.Migrations
{
    public partial class EntityReserationUpdate04112021_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adults",
                table: "EntityReservations");

            migrationBuilder.DropColumn(
                name: "Childrens",
                table: "EntityReservations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Adults",
                table: "EntityReservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Childrens",
                table: "EntityReservations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
