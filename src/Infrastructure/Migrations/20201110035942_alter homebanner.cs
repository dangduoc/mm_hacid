using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class alterhomebanner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomeBanners_Project_ProjectId1",
                table: "HomeBanners");

            migrationBuilder.DropIndex(
                name: "IX_HomeBanners_ProjectId1",
                table: "HomeBanners");

            migrationBuilder.DropColumn(
                name: "ProjectId1",
                table: "HomeBanners");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "HomeBanners",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_HomeBanners_ProjectId",
                table: "HomeBanners",
                column: "ProjectId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HomeBanners_Project_ProjectId",
                table: "HomeBanners",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomeBanners_Project_ProjectId",
                table: "HomeBanners");

            migrationBuilder.DropIndex(
                name: "IX_HomeBanners_ProjectId",
                table: "HomeBanners");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "HomeBanners",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ProjectId1",
                table: "HomeBanners",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HomeBanners_ProjectId1",
                table: "HomeBanners",
                column: "ProjectId1");

            migrationBuilder.AddForeignKey(
                name: "FK_HomeBanners_Project_ProjectId1",
                table: "HomeBanners",
                column: "ProjectId1",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
