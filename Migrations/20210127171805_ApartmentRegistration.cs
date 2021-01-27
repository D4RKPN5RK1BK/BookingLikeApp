using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingLikeApp.Migrations
{
    public partial class ApartmentRegistration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AllTimeRegistration",
                table: "Apartments",
                newName: "FullTimeRegistration");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullTimeRegistration",
                table: "Apartments",
                newName: "AllTimeRegistration");
        }
    }
}
