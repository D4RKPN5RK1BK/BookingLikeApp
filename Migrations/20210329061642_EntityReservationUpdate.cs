using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingLikeApp.Migrations
{
    public partial class EntityReservationUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Packs_PackId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_PackTenants_PackTenantId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_PackId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_PackTenantId",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "Adults",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "Childrens",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "PackId",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "PackTenantId",
                table: "Reservation");

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

            migrationBuilder.AddColumn<int>(
                name: "PackId",
                table: "EntityReservations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PackTenantId",
                table: "EntityReservations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EntityReservations_PackId",
                table: "EntityReservations",
                column: "PackId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityReservations_PackTenantId",
                table: "EntityReservations",
                column: "PackTenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_EntityReservations_Packs_PackId",
                table: "EntityReservations",
                column: "PackId",
                principalTable: "Packs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EntityReservations_PackTenants_PackTenantId",
                table: "EntityReservations",
                column: "PackTenantId",
                principalTable: "PackTenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntityReservations_Packs_PackId",
                table: "EntityReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_EntityReservations_PackTenants_PackTenantId",
                table: "EntityReservations");

            migrationBuilder.DropIndex(
                name: "IX_EntityReservations_PackId",
                table: "EntityReservations");

            migrationBuilder.DropIndex(
                name: "IX_EntityReservations_PackTenantId",
                table: "EntityReservations");

            migrationBuilder.DropColumn(
                name: "Adults",
                table: "EntityReservations");

            migrationBuilder.DropColumn(
                name: "Childrens",
                table: "EntityReservations");

            migrationBuilder.DropColumn(
                name: "PackId",
                table: "EntityReservations");

            migrationBuilder.DropColumn(
                name: "PackTenantId",
                table: "EntityReservations");

            migrationBuilder.AddColumn<int>(
                name: "Adults",
                table: "Reservation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Childrens",
                table: "Reservation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PackId",
                table: "Reservation",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PackTenantId",
                table: "Reservation",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_PackId",
                table: "Reservation",
                column: "PackId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_PackTenantId",
                table: "Reservation",
                column: "PackTenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Packs_PackId",
                table: "Reservation",
                column: "PackId",
                principalTable: "Packs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_PackTenants_PackTenantId",
                table: "Reservation",
                column: "PackTenantId",
                principalTable: "PackTenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
