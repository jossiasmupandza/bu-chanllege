using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddTypeColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Categories");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "InputTypes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "InputTypes");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
