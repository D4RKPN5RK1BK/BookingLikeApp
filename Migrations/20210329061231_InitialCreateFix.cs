using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingLikeApp.Migrations
{
    public partial class InitialCreateFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntityReservations_AspNetUsers_UserId",
                table: "EntityReservations");

            migrationBuilder.DropIndex(
                name: "IX_EntityReservations_UserId",
                table: "EntityReservations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "EntityReservations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "EntityReservations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EntityReservations_UserId",
                table: "EntityReservations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_EntityReservations_AspNetUsers_UserId",
                table: "EntityReservations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
