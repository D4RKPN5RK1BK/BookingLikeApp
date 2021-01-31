using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingLikeApp.Migrations
{
    public partial class ApartmentTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ArrivalTimeEnds",
                table: "Apartments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ArrivalTimeStarts",
                table: "Apartments",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<DateTime>(
                name: "DepartureTimeEnds",
                table: "Apartments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DepartureTimeStarts",
                table: "Apartments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArrivalTimeEnds",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "ArrivalTimeStarts",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "DepartureTimeEnds",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "DepartureTimeStarts",
                table: "Apartments");
        }
    }
}
