using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingLikeApp.Migrations
{
    public partial class ApartmentUpdate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pools",
                table: "Apartments");

            migrationBuilder.RenameColumn(
                name: "ChildrenAllowed",
                table: "Apartments",
                newName: "SmokeFreeNumbers");

            migrationBuilder.AlterColumn<string>(
                name: "SecondAddressLine",
                table: "Apartments",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AllTimeRegistration",
                table: "Apartments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Breakfest",
                table: "Apartments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "BreakfestIncluded",
                table: "Apartments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ChildrensAllowed",
                table: "Apartments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "FamilyNumbers",
                table: "Apartments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Fitnes",
                table: "Apartments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Parking",
                table: "Apartments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Pool",
                table: "Apartments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllTimeRegistration",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "Breakfest",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "BreakfestIncluded",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "ChildrensAllowed",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "FamilyNumbers",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "Fitnes",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "Parking",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "Pool",
                table: "Apartments");

            migrationBuilder.RenameColumn(
                name: "SmokeFreeNumbers",
                table: "Apartments",
                newName: "ChildrenAllowed");

            migrationBuilder.AlterColumn<string>(
                name: "SecondAddressLine",
                table: "Apartments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Pools",
                table: "Apartments",
                type: "decimal(3,0)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
