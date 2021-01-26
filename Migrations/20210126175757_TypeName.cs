using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingLikeApp.Migrations
{
    public partial class TypeName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NormalizedName",
                table: "ApartmentTypes",
                newName: "PhotoUrl");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ApartmentTypes",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ApartmentTypes",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ApartmentTypes");

            migrationBuilder.RenameColumn(
                name: "PhotoUrl",
                table: "ApartmentTypes",
                newName: "NormalizedName");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ApartmentTypes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);
        }
    }
}
