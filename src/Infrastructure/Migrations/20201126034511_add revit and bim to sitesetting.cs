using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class addrevitandbimtositesetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Bin",
                table: "SiteSetting",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ForumLink",
                table: "SiteSetting",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Revit",
                table: "SiteSetting",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bin",
                table: "SiteSetting");

            migrationBuilder.DropColumn(
                name: "ForumLink",
                table: "SiteSetting");

            migrationBuilder.DropColumn(
                name: "Revit",
                table: "SiteSetting");
        }
    }
}
