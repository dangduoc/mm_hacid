using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class addaboutporjectfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderIndex",
                table: "ProjectField");

            migrationBuilder.DropColumn(
                name: "Summary",
                table: "ProjectField");

            migrationBuilder.DropColumn(
                name: "SummaryEn",
                table: "ProjectField");

            migrationBuilder.CreateTable(
                name: "AboutProjectField",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    NameEn = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    Summary = table.Column<string>(maxLength: 500, nullable: false),
                    SummaryEn = table.Column<string>(unicode: false, maxLength: 500, nullable: false),
                    Banner = table.Column<string>(nullable: true),
                    OrderIndex = table.Column<int>(nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutProjectField", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboutProjectField");

            migrationBuilder.AddColumn<int>(
                name: "OrderIndex",
                table: "ProjectField",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "ProjectField",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SummaryEn",
                table: "ProjectField",
                type: "varchar(500)",
                unicode: false,
                maxLength: 500,
                nullable: true);
        }
    }
}
