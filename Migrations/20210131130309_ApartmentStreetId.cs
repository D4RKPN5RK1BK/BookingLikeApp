using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingLikeApp.Migrations
{
    public partial class ApartmentStreetId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_Streets_StreetId",
                table: "Apartments");

            migrationBuilder.AlterColumn<int>(
                name: "StreetId",
                table: "Apartments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ApartmentStreetId",
                table: "Apartments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PolisherStreetId",
                table: "Apartments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "UseCards",
                table: "Apartments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_Streets_StreetId",
                table: "Apartments",
                column: "StreetId",
                principalTable: "Streets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_Streets_StreetId",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "ApartmentStreetId",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "PolisherStreetId",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "UseCards",
                table: "Apartments");

            migrationBuilder.AlterColumn<int>(
                name: "StreetId",
                table: "Apartments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_Streets_StreetId",
                table: "Apartments",
                column: "StreetId",
                principalTable: "Streets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
