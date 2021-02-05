using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingLikeApp.Migrations
{
    public partial class NumberRoomUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberRoomId",
                table: "NumberRoomBeds",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_NumberRoomBeds_NumberRoomId",
                table: "NumberRoomBeds",
                column: "NumberRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_NumberRoomBeds_NumberRooms_NumberRoomId",
                table: "NumberRoomBeds",
                column: "NumberRoomId",
                principalTable: "NumberRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NumberRoomBeds_NumberRooms_NumberRoomId",
                table: "NumberRoomBeds");

            migrationBuilder.DropIndex(
                name: "IX_NumberRoomBeds_NumberRoomId",
                table: "NumberRoomBeds");

            migrationBuilder.DropColumn(
                name: "NumberRoomId",
                table: "NumberRoomBeds");
        }
    }
}
