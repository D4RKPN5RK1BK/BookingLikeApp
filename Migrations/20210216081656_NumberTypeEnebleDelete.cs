using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingLikeApp.Migrations
{
    public partial class NumberTypeEnebleDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Enable",
                table: "NumberTypes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Enable",
                table: "NumberTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "NumberTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Enable",
                value: true);

            migrationBuilder.UpdateData(
                table: "NumberTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Enable",
                value: true);

            migrationBuilder.UpdateData(
                table: "NumberTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Enable",
                value: true);

            migrationBuilder.UpdateData(
                table: "NumberTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "Enable",
                value: true);

            migrationBuilder.UpdateData(
                table: "NumberTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "Enable",
                value: true);
        }
    }
}
