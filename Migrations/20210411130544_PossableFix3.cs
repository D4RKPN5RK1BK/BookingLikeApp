using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingLikeApp.Migrations
{
    public partial class PossableFix3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntityReservations_Packs_PackId",
                table: "EntityReservations");

            migrationBuilder.DropIndex(
                name: "IX_EntityReservations_PackId",
                table: "EntityReservations");

            migrationBuilder.DropColumn(
                name: "PackId",
                table: "EntityReservations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PackId",
                table: "EntityReservations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EntityReservations_PackId",
                table: "EntityReservations",
                column: "PackId");

            migrationBuilder.AddForeignKey(
                name: "FK_EntityReservations_Packs_PackId",
                table: "EntityReservations",
                column: "PackId",
                principalTable: "Packs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
