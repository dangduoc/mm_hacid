using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class projectcatemulti : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_ProjectCategory_CategoryId",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_ProjectField_ProjectFieldId",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_CategoryId",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_ProjectFieldId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "ProjectFieldId",
                table: "Project");

            migrationBuilder.CreateTable(
                name: "ProjectCategoryRelation",
                columns: table => new
                {
                    ProjectId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectCategoryRelation", x => new { x.CategoryId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_ProjectCategoryRelation_ProjectCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ProjectCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectCategoryRelation_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectFieldRelation",
                columns: table => new
                {
                    ProjectId = table.Column<int>(nullable: false),
                    ProjectFieldId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectFieldRelation", x => new { x.ProjectFieldId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_ProjectFieldRelation_ProjectField_ProjectFieldId",
                        column: x => x.ProjectFieldId,
                        principalTable: "ProjectField",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectFieldRelation_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCategoryRelation_ProjectId",
                table: "ProjectCategoryRelation",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFieldRelation_ProjectId",
                table: "ProjectFieldRelation",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectCategoryRelation");

            migrationBuilder.DropTable(
                name: "ProjectFieldRelation");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Project",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProjectFieldId",
                table: "Project",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Project_CategoryId",
                table: "Project",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_ProjectFieldId",
                table: "Project",
                column: "ProjectFieldId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_ProjectCategory_CategoryId",
                table: "Project",
                column: "CategoryId",
                principalTable: "ProjectCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_ProjectField_ProjectFieldId",
                table: "Project",
                column: "ProjectFieldId",
                principalTable: "ProjectField",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
