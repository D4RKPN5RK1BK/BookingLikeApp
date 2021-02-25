using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingLikeApp.Migrations
{
    public partial class uf2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApartmentServices_Apartments_ApartmentId",
                table: "ApartmentServices");

            migrationBuilder.DropColumn(
                name: "AparmentId",
                table: "ApartmentServices");

            migrationBuilder.AlterColumn<int>(
                name: "ApartmentId",
                table: "ApartmentServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ApartmentServices_Apartments_ApartmentId",
                table: "ApartmentServices",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApartmentServices_Apartments_ApartmentId",
                table: "ApartmentServices");

            migrationBuilder.AlterColumn<int>(
                name: "ApartmentId",
                table: "ApartmentServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AparmentId",
                table: "ApartmentServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ApartmentServices_Apartments_ApartmentId",
                table: "ApartmentServices",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
