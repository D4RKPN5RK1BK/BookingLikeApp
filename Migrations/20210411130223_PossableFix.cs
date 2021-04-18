using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingLikeApp.Migrations
{
    public partial class PossableFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntityReservations_NumberEntities_NumberEntityId",
                table: "EntityReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_EntityReservations_PackTenants_PackTenantId",
                table: "EntityReservations");

            migrationBuilder.AlterColumn<int>(
                name: "PackTenantId",
                table: "EntityReservations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NumberEntityId",
                table: "EntityReservations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_EntityReservations_NumberEntities_NumberEntityId",
                table: "EntityReservations",
                column: "NumberEntityId",
                principalTable: "NumberEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_EntityReservations_NumberEntities_NumberEntityId",
                table: "EntityReservations");

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

            migrationBuilder.AlterColumn<int>(
                name: "NumberEntityId",
                table: "EntityReservations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EntityReservations_NumberEntities_NumberEntityId",
                table: "EntityReservations",
                column: "NumberEntityId",
                principalTable: "NumberEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
