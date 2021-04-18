using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingLikeApp.Migrations
{
    public partial class ReserationUpdate04112021_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Numbers_NumberId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_NumberId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "NumberId",
                table: "Reservations");

            migrationBuilder.UpdateData(
                table: "NumberTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Двухместный номер");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberId",
                table: "Reservations",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "NumberTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Двуместный номер");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_NumberId",
                table: "Reservations",
                column: "NumberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Numbers_NumberId",
                table: "Reservations",
                column: "NumberId",
                principalTable: "Numbers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
