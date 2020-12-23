using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class addsitesetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SiteSetting",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Address = table.Column<string>(maxLength: 500, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 500, nullable: true),
                    Fax = table.Column<string>(maxLength: 500, nullable: true),
                    Email = table.Column<string>(maxLength: 500, nullable: true),
                    Facebook = table.Column<string>(maxLength: 500, nullable: true),
                    Linkedin = table.Column<string>(maxLength: 500, nullable: true),
                    Youtube = table.Column<string>(nullable: true),
                    Map = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteSetting", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SiteSetting");
        }
    }
}
