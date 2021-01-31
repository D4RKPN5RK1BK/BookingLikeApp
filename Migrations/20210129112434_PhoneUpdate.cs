using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingLikeApp.Migrations
{
    public partial class PhoneUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContactNumber",
                table: "Apartments",
                newName: "ContactPhone");

            migrationBuilder.RenameColumn(
                name: "AdditionalNumber",
                table: "Apartments",
                newName: "AdditionalPhone");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContactPhone",
                table: "Apartments",
                newName: "ContactNumber");

            migrationBuilder.RenameColumn(
                name: "AdditionalPhone",
                table: "Apartments",
                newName: "AdditionalNumber");
        }
    }
}
