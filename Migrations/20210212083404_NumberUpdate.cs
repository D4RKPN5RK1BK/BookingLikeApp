using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingLikeApp.Migrations
{
    public partial class NumberUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Shared",
                table: "Numbers");

            migrationBuilder.RenameColumn(
                name: "CustomName",
                table: "Numbers",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Numbers",
                newName: "CustomName");

            migrationBuilder.AddColumn<bool>(
                name: "Shared",
                table: "Numbers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
