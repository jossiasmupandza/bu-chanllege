using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddPublicFlags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Public",
                table: "Quizzes");

            migrationBuilder.AddColumn<string>(
                name: "EditToken",
                table: "Quizzes",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PublicAnswer",
                table: "Quizzes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PublicQuiz",
                table: "Quizzes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EditToken",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "PublicAnswer",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "PublicQuiz",
                table: "Quizzes");

            migrationBuilder.AddColumn<bool>(
                name: "Public",
                table: "Quizzes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
