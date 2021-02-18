using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingLikeApp.Migrations
{
    public partial class NumberTypeCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BedOnly",
                table: "NumberTypes");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "NumberTypes",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "NumberTypes");

            migrationBuilder.AddColumn<bool>(
                name: "BedOnly",
                table: "NumberTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "NumberTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "BedOnly",
                value: true);

            migrationBuilder.UpdateData(
                table: "NumberTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "BedOnly",
                value: true);
        }
    }
}
