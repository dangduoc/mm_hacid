using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class addbannersetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SummaryEn",
                table: "AboutProject",
                unicode: false,
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldUnicode: false,
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Summary",
                table: "AboutProject",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.CreateTable(
                name: "BannerSetting",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HomeProject1 = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    HomeProject2 = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    HomeProject3 = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    HomeProject4 = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    About = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    Project = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    New = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    Contact = table.Column<string>(unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BannerSetting", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BannerSetting");

            migrationBuilder.AlterColumn<string>(
                name: "SummaryEn",
                table: "AboutProject",
                type: "varchar(500)",
                unicode: false,
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 2000);

            migrationBuilder.AlterColumn<string>(
                name: "Summary",
                table: "AboutProject",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 2000);
        }
    }
}
