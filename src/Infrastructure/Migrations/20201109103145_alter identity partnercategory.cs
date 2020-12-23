using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class alteridentitypartnercategory : Migration
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
                name: "PartnerCategoryId",
                table: "PartnerCategory");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PartnerCategory",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partner_PartnerCategory_CategoryId",
                table: "Partner");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PartnerCategory",
                table: "PartnerCategory");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PartnerCategory");

            migrationBuilder.AddColumn<int>(
                name: "PartnerCategoryId",
                table: "PartnerCategory",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
    }
}
