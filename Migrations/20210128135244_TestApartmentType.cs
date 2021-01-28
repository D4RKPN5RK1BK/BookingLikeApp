using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingLikeApp.Migrations
{
    public partial class TestApartmentType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ApartmentTypes",
                columns: new[] { "Id", "Description", "Name", "PhotoUrl" },
                values: new object[] { 1, "TestType", "TestType", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApartmentTypes",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
