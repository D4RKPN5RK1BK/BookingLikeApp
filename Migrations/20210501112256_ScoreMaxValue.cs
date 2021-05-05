using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingLikeApp.Migrations
{
    public partial class ScoreMaxValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Score",
                table: "ReviewScore",
                newName: "Value");

            migrationBuilder.AddColumn<int>(
                name: "ScoreId",
                table: "ReviewScore",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Score",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Score", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReviewScore_ScoreId",
                table: "ReviewScore",
                column: "ScoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewScore_Score_ScoreId",
                table: "ReviewScore",
                column: "ScoreId",
                principalTable: "Score",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewScore_Score_ScoreId",
                table: "ReviewScore");

            migrationBuilder.DropTable(
                name: "Score");

            migrationBuilder.DropIndex(
                name: "IX_ReviewScore_ScoreId",
                table: "ReviewScore");

            migrationBuilder.DropColumn(
                name: "ScoreId",
                table: "ReviewScore");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "ReviewScore",
                newName: "Score");
        }
    }
}
