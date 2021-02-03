using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingLikeApp.Migrations
{
    public partial class CardsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApartmentCard_Apartments_ApartmentId",
                table: "ApartmentCard");

            migrationBuilder.DropForeignKey(
                name: "FK_ApartmentCard_Card_CardId",
                table: "ApartmentCard");

            migrationBuilder.DropForeignKey(
                name: "FK_NumberBed_Beds_BedId",
                table: "NumberBed");

            migrationBuilder.DropForeignKey(
                name: "FK_NumberBed_Numbers_NumberId",
                table: "NumberBed");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NumberBed",
                table: "NumberBed");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Card",
                table: "Card");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApartmentCard",
                table: "ApartmentCard");

            migrationBuilder.RenameTable(
                name: "NumberBed",
                newName: "NumberBeds");

            migrationBuilder.RenameTable(
                name: "Card",
                newName: "Cards");

            migrationBuilder.RenameTable(
                name: "ApartmentCard",
                newName: "ApartmentCards");

            migrationBuilder.RenameIndex(
                name: "IX_NumberBed_NumberId",
                table: "NumberBeds",
                newName: "IX_NumberBeds_NumberId");

            migrationBuilder.RenameIndex(
                name: "IX_NumberBed_BedId",
                table: "NumberBeds",
                newName: "IX_NumberBeds_BedId");

            migrationBuilder.RenameIndex(
                name: "IX_ApartmentCard_CardId",
                table: "ApartmentCards",
                newName: "IX_ApartmentCards_CardId");

            migrationBuilder.RenameIndex(
                name: "IX_ApartmentCard_ApartmentId",
                table: "ApartmentCards",
                newName: "IX_ApartmentCards_ApartmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NumberBeds",
                table: "NumberBeds",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cards",
                table: "Cards",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApartmentCards",
                table: "ApartmentCards",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "TestCard" });

            migrationBuilder.AddForeignKey(
                name: "FK_ApartmentCards_Apartments_ApartmentId",
                table: "ApartmentCards",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApartmentCards_Cards_CardId",
                table: "ApartmentCards",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NumberBeds_Beds_BedId",
                table: "NumberBeds",
                column: "BedId",
                principalTable: "Beds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NumberBeds_Numbers_NumberId",
                table: "NumberBeds",
                column: "NumberId",
                principalTable: "Numbers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApartmentCards_Apartments_ApartmentId",
                table: "ApartmentCards");

            migrationBuilder.DropForeignKey(
                name: "FK_ApartmentCards_Cards_CardId",
                table: "ApartmentCards");

            migrationBuilder.DropForeignKey(
                name: "FK_NumberBeds_Beds_BedId",
                table: "NumberBeds");

            migrationBuilder.DropForeignKey(
                name: "FK_NumberBeds_Numbers_NumberId",
                table: "NumberBeds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NumberBeds",
                table: "NumberBeds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cards",
                table: "Cards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApartmentCards",
                table: "ApartmentCards");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.RenameTable(
                name: "NumberBeds",
                newName: "NumberBed");

            migrationBuilder.RenameTable(
                name: "Cards",
                newName: "Card");

            migrationBuilder.RenameTable(
                name: "ApartmentCards",
                newName: "ApartmentCard");

            migrationBuilder.RenameIndex(
                name: "IX_NumberBeds_NumberId",
                table: "NumberBed",
                newName: "IX_NumberBed_NumberId");

            migrationBuilder.RenameIndex(
                name: "IX_NumberBeds_BedId",
                table: "NumberBed",
                newName: "IX_NumberBed_BedId");

            migrationBuilder.RenameIndex(
                name: "IX_ApartmentCards_CardId",
                table: "ApartmentCard",
                newName: "IX_ApartmentCard_CardId");

            migrationBuilder.RenameIndex(
                name: "IX_ApartmentCards_ApartmentId",
                table: "ApartmentCard",
                newName: "IX_ApartmentCard_ApartmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NumberBed",
                table: "NumberBed",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Card",
                table: "Card",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApartmentCard",
                table: "ApartmentCard",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApartmentCard_Apartments_ApartmentId",
                table: "ApartmentCard",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApartmentCard_Card_CardId",
                table: "ApartmentCard",
                column: "CardId",
                principalTable: "Card",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NumberBed_Beds_BedId",
                table: "NumberBed",
                column: "BedId",
                principalTable: "Beds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NumberBed_Numbers_NumberId",
                table: "NumberBed",
                column: "NumberId",
                principalTable: "Numbers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
