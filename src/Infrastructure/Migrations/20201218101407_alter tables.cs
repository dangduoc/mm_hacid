using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class altertables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HomeBanners");

            migrationBuilder.DropColumn(
                name: "Banner",
                table: "Project");

            migrationBuilder.AddColumn<string>(
                name: "CompanyImage",
                table: "SiteSetting",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "SiteSetting",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyNameEn",
                table: "SiteSetting",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CopyRight",
                table: "SiteSetting",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Theme",
                table: "Project",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "Partner",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "AboutProject",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "HomeSlide",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 500, nullable: false),
                    TitleEn = table.Column<string>(maxLength: 500, nullable: true),
                    Index = table.Column<int>(nullable: false),
                    Link = table.Column<string>(nullable: true),
                    Theme = table.Column<int>(nullable: false),
                    Image = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeSlide", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HomeSlide");

            migrationBuilder.DropColumn(
                name: "CompanyImage",
                table: "SiteSetting");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "SiteSetting");

            migrationBuilder.DropColumn(
                name: "CompanyNameEn",
                table: "SiteSetting");

            migrationBuilder.DropColumn(
                name: "CopyRight",
                table: "SiteSetting");

            migrationBuilder.DropColumn(
                name: "Theme",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "Partner");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "AboutProject");

            migrationBuilder.AddColumn<string>(
                name: "Banner",
                table: "Project",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "HomeBanners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Index = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    Theme = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeBanners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeBanners_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HomeBanners_ProjectId",
                table: "HomeBanners",
                column: "ProjectId",
                unique: true);
        }
    }
}
