using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingLikeApp.Migrations
{
    public partial class NumberEntityEnable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "NumberTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.AddColumn<bool>(
                name: "Enable",
                table: "NumberEntities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "ApartmentTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Отель со множеством комнат и номеров", "Отель" });

            migrationBuilder.InsertData(
                table: "ApartmentTypes",
                columns: new[] { "Id", "Description", "FacilitesRequired", "Name", "PhotoUrl" },
                values: new object[] { 2, "Частный дом обчно сожержит несколько квартир", false, "Дом", null });

            migrationBuilder.InsertData(
                table: "Beds",
                columns: new[] { "Id", "Capacity", "Code", "Name", "RoomId" },
                values: new object[,]
                {
                    { 1, 0m, "SNG", "Односпальная", null },
                    { 2, 0m, "DBL", "Двуспальная", null }
                });

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "MasterCard");

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "ВТБ" });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Москва");

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Россия");

            migrationBuilder.UpdateData(
                table: "NumberTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Одноместный номер");

            migrationBuilder.UpdateData(
                table: "NumberTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Двуместный номер");

            migrationBuilder.UpdateData(
                table: "NumberTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Общая комната");

            migrationBuilder.UpdateData(
                table: "NumberTypes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "HasRooms", "Name" },
                values: new object[] { true, "Апартаменты" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Спальная");

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Гостинная");

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Ванная");

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Wifi");

            migrationBuilder.UpdateData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Ленинградская");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApartmentTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Beds",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Beds",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "Enable",
                table: "NumberEntities");

            migrationBuilder.UpdateData(
                table: "ApartmentTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "TestType", "TestType" });

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "TestCard");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "TestCity");

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "TestCountry");

            migrationBuilder.UpdateData(
                table: "NumberTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Number");

            migrationBuilder.UpdateData(
                table: "NumberTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Apartment");

            migrationBuilder.UpdateData(
                table: "NumberTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "SharedRoom");

            migrationBuilder.UpdateData(
                table: "NumberTypes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "HasRooms", "Name" },
                values: new object[] { false, "SharedBed" });

            migrationBuilder.InsertData(
                table: "NumberTypes",
                columns: new[] { "Id", "Code", "Description", "HasRooms", "Name", "Share" },
                values: new object[] { 5, null, null, true, "SharedBedWithRooms", true });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Bedroom");

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Livingroom");

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Bathroom");

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "TestService");

            migrationBuilder.UpdateData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "TestCountry");
        }
    }
}
