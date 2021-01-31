using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingLikeApp.Migrations
{
    public partial class OneToOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Registrations_ApartmentId",
                table: "Registrations");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_ApartmentId",
                table: "Registrations",
                column: "ApartmentId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Registrations_ApartmentId",
                table: "Registrations");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_ApartmentId",
                table: "Registrations",
                column: "ApartmentId");
        }
    }
}
