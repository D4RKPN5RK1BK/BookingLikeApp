using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingLikeApp.Migrations
{
    public partial class ApartmentServiceFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApartmentServices_Services_ServiceId",
                table: "ApartmentServices");

            migrationBuilder.DropColumn(
                name: "AdditionId",
                table: "ApartmentServices");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceId",
                table: "ApartmentServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ApartmentServices_Services_ServiceId",
                table: "ApartmentServices",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApartmentServices_Services_ServiceId",
                table: "ApartmentServices");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceId",
                table: "ApartmentServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AdditionId",
                table: "ApartmentServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ApartmentServices_Services_ServiceId",
                table: "ApartmentServices",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
