using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class UpdateColunsAnswerOptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnswerId",
                table: "Options",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Options_AnswerId",
                table: "Options",
                column: "AnswerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Answers_AnswerId",
                table: "Options",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_Answers_AnswerId",
                table: "Options");

            migrationBuilder.DropIndex(
                name: "IX_Options_AnswerId",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "AnswerId",
                table: "Options");
        }
    }
}
