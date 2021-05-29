using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingLikeApp.Migrations
{
    public partial class Fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntityReservations_PackTenants_PackTenantId",
                table: "EntityReservations");

            migrationBuilder.DropColumn(
                name: "CancelPrice",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "DaysUntilCancelEnds",
                table: "Apartments");

            migrationBuilder.AlterColumn<int>(
                name: "PackTenantId",
                table: "EntityReservations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EntityReservations_PackTenants_PackTenantId",
                table: "EntityReservations",
                column: "PackTenantId",
                principalTable: "PackTenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntityReservations_PackTenants_PackTenantId",
                table: "EntityReservations");

            migrationBuilder.AlterColumn<int>(
                name: "PackTenantId",
                table: "EntityReservations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "CancelPrice",
                table: "Apartments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "DaysUntilCancelEnds",
                table: "Apartments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_EntityReservations_PackTenants_PackTenantId",
                table: "EntityReservations",
                column: "PackTenantId",
                principalTable: "PackTenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
