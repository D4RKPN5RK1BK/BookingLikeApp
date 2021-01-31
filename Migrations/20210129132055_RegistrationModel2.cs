using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingLikeApp.Migrations
{
    public partial class RegistrationModel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Registrations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApartmentId = table.Column<int>(type: "int", nullable: false),
                    BasicInfo = table.Column<bool>(type: "bit", nullable: false),
                    Numbers = table.Column<bool>(type: "bit", nullable: false),
                    Rules = table.Column<bool>(type: "bit", nullable: false),
                    FacilitesRequired = table.Column<bool>(type: "bit", nullable: false),
                    Facilites = table.Column<bool>(type: "bit", nullable: false),
                    Photos = table.Column<bool>(type: "bit", nullable: false),
                    Payment = table.Column<bool>(type: "bit", nullable: false),
                    Services = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Registrations_Apartments_ApartmentId",
                        column: x => x.ApartmentId,
                        principalTable: "Apartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_ApartmentId",
                table: "Registrations",
                column: "ApartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Registrations");
        }
    }
}
