using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class alterhomeslide1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "HomeSlide");

            migrationBuilder.DropColumn(
                name: "DescriptionEn",
                table: "HomeSlide");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "HomeSlide",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocationEn",
                table: "HomeSlide",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectTitle",
                table: "HomeSlide",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectTitleEn",
                table: "HomeSlide",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "HomeSlide");

            migrationBuilder.DropColumn(
                name: "LocationEn",
                table: "HomeSlide");

            migrationBuilder.DropColumn(
                name: "ProjectTitle",
                table: "HomeSlide");

            migrationBuilder.DropColumn(
                name: "ProjectTitleEn",
                table: "HomeSlide");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "HomeSlide",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEn",
                table: "HomeSlide",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
