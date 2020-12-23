using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class alteraboutprojectandpartner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partner_PartnerCategory_CategoryId",
                table: "Partner");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PartnerCategory",
                table: "PartnerCategory");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "ProjectCategory");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PartnerCategory");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "AboutProject");

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                table: "PartnerCategory",
                unicode: false,
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PartnerCategory",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PartnerCategoryId",
                table: "PartnerCategory",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Thumbnail",
                table: "Partner",
                unicode: false,
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldUnicode: false,
                oldMaxLength: 200);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "AboutProject",
                unicode: false,
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SummaryEn",
                table: "AboutProject",
                unicode: false,
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TabTitle",
                table: "AboutProject",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TabTitleEn",
                table: "AboutProject",
                unicode: false,
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TitleEn",
                table: "AboutProject",
                unicode: false,
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PartnerCategory",
                table: "PartnerCategory",
                column: "PartnerCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Partner_PartnerCategory_CategoryId",
                table: "Partner",
                column: "CategoryId",
                principalTable: "PartnerCategory",
                principalColumn: "PartnerCategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partner_PartnerCategory_CategoryId",
                table: "Partner");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PartnerCategory",
                table: "PartnerCategory");

            migrationBuilder.DropColumn(
                name: "PartnerCategoryId",
                table: "PartnerCategory");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "AboutProject");

            migrationBuilder.DropColumn(
                name: "SummaryEn",
                table: "AboutProject");

            migrationBuilder.DropColumn(
                name: "TabTitle",
                table: "AboutProject");

            migrationBuilder.DropColumn(
                name: "TabTitleEn",
                table: "AboutProject");

            migrationBuilder.DropColumn(
                name: "TitleEn",
                table: "AboutProject");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "ProjectCategory",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                table: "PartnerCategory",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PartnerCategory",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PartnerCategory",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Thumbnail",
                table: "Partner",
                type: "int",
                unicode: false,
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 200);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "AboutProject",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PartnerCategory",
                table: "PartnerCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Partner_PartnerCategory_CategoryId",
                table: "Partner",
                column: "CategoryId",
                principalTable: "PartnerCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
