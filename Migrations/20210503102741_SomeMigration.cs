using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingLikeApp.Migrations
{
    public partial class SomeMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewScore_Reviews_ReviewId",
                table: "ReviewScore");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewScore_Scores_ScoreId",
                table: "ReviewScore");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReviewScore",
                table: "ReviewScore");

            migrationBuilder.RenameTable(
                name: "ReviewScore",
                newName: "ReviewScores");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewScore_ScoreId",
                table: "ReviewScores",
                newName: "IX_ReviewScores_ScoreId");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewScore_ReviewId",
                table: "ReviewScores",
                newName: "IX_ReviewScores_ReviewId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReviewScores",
                table: "ReviewScores",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewScores_Reviews_ReviewId",
                table: "ReviewScores",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "ReservationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewScores_Scores_ScoreId",
                table: "ReviewScores",
                column: "ScoreId",
                principalTable: "Scores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewScores_Reviews_ReviewId",
                table: "ReviewScores");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewScores_Scores_ScoreId",
                table: "ReviewScores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReviewScores",
                table: "ReviewScores");

            migrationBuilder.RenameTable(
                name: "ReviewScores",
                newName: "ReviewScore");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewScores_ScoreId",
                table: "ReviewScore",
                newName: "IX_ReviewScore_ScoreId");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewScores_ReviewId",
                table: "ReviewScore",
                newName: "IX_ReviewScore_ReviewId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReviewScore",
                table: "ReviewScore",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewScore_Reviews_ReviewId",
                table: "ReviewScore",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "ReservationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewScore_Scores_ScoreId",
                table: "ReviewScore",
                column: "ScoreId",
                principalTable: "Scores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
