using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingLikeApp.Migrations
{
    public partial class DbFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProceedDateTime",
                table: "EntityReservations",
                newName: "TimeStamp");

            migrationBuilder.AddColumn<int>(
                name: "Adults",
                table: "EntityReservations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Childrens",
                table: "EntityReservations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Packs_NumberId",
                table: "Packs",
                column: "NumberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Packs_Numbers_NumberId",
                table: "Packs",
                column: "NumberId",
                principalTable: "Numbers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packs_Numbers_NumberId",
                table: "Packs");

            migrationBuilder.DropIndex(
                name: "IX_Packs_NumberId",
                table: "Packs");

            migrationBuilder.DropColumn(
                name: "Adults",
                table: "EntityReservations");

            migrationBuilder.DropColumn(
                name: "Childrens",
                table: "EntityReservations");

            migrationBuilder.RenameColumn(
                name: "TimeStamp",
                table: "EntityReservations",
                newName: "ProceedDateTime");
        }
    }
}
