using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingLikeApp.Migrations
{
    public partial class RuUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FoundationDate",
                table: "Apartments");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Numbers",
                newName: "CustomName");

            migrationBuilder.AddColumn<bool>(
                name: "CancelPrice",
                table: "Apartments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "DaysUntilCancelEnds",
                table: "Apartments",
                type: "decimal(2,0)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CancelPrice",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "DaysUntilCancelEnds",
                table: "Apartments");

            migrationBuilder.RenameColumn(
                name: "CustomName",
                table: "Numbers",
                newName: "Name");

            migrationBuilder.AddColumn<DateTime>(
                name: "FoundationDate",
                table: "Apartments",
                type: "datetime2",
                nullable: true);
        }
    }
}
