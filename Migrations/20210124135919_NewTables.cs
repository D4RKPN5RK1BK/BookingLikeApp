using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingLikeApp.Migrations
{
    public partial class NewTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Numbers_NumberType_NumberTypeId",
                table: "Numbers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NumberType",
                table: "NumberType");

            migrationBuilder.RenameTable(
                name: "NumberType",
                newName: "NumberTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NumberTypes",
                table: "NumberTypes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "NumberNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberTypeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumberNames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NumberNames_NumberTypes_NumberTypeId",
                        column: x => x.NumberTypeId,
                        principalTable: "NumberTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Streets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Streets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Streets_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NumberNames_NumberTypeId",
                table: "NumberNames",
                column: "NumberTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Streets_CityId",
                table: "Streets",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Numbers_NumberTypes_NumberTypeId",
                table: "Numbers",
                column: "NumberTypeId",
                principalTable: "NumberTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Numbers_NumberTypes_NumberTypeId",
                table: "Numbers");

            migrationBuilder.DropTable(
                name: "NumberNames");

            migrationBuilder.DropTable(
                name: "Streets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NumberTypes",
                table: "NumberTypes");

            migrationBuilder.RenameTable(
                name: "NumberTypes",
                newName: "NumberType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NumberType",
                table: "NumberType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Numbers_NumberType_NumberTypeId",
                table: "Numbers",
                column: "NumberTypeId",
                principalTable: "NumberType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
