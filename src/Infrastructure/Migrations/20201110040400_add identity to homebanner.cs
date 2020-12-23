using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class addidentitytohomebanner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_HomeBanners",
                table: "HomeBanners");

            migrationBuilder.DropColumn(
                name: "HomeId",
                table: "HomeBanners");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "HomeBanners",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HomeBanners",
                table: "HomeBanners",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_HomeBanners",
                table: "HomeBanners");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "HomeBanners");

            migrationBuilder.AddColumn<int>(
                name: "HomeId",
                table: "HomeBanners",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HomeBanners",
                table: "HomeBanners",
                column: "HomeId");
        }
    }
}
