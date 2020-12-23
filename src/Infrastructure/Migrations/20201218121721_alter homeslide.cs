using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class alterhomeslide : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "HomeSlide",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEn",
                table: "HomeSlide",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "HomeSlide",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "HomeSlide");

            migrationBuilder.DropColumn(
                name: "DescriptionEn",
                table: "HomeSlide");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "HomeSlide");
        }
    }
}
