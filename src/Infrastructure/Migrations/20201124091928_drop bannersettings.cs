using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class dropbannersettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BannerSetting");

            migrationBuilder.AddColumn<string>(
                name: "MapEmbed",
                table: "SiteSetting",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "Project",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "New",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MapEmbed",
                table: "SiteSetting");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "New");

            migrationBuilder.CreateTable(
                name: "BannerSetting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    About = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Contact = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    HomeProject1 = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    HomeProject2 = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    HomeProject3 = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    HomeProject4 = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    New = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Project = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BannerSetting", x => x.Id);
                });
        }
    }
}
