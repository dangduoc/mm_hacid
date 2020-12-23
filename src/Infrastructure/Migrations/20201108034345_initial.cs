using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AboutArticle",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    HtmlContent = table.Column<string>(nullable: false),
                    HtmlContentEn = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutArticle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AboutProject",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 500, nullable: false),
                    Summary = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutProject", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyHistory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    NameEn = table.Column<string>(unicode: false, maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    NameEn = table.Column<string>(unicode: false, maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PartnerCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    NameEn = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartnerCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    NameEn = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    ProjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectField",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    NameEn = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    Summary = table.Column<string>(maxLength: 500, nullable: true),
                    SummaryEn = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    OrderIndex = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectField", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "New",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedByUserId = table.Column<string>(nullable: true),
                    CreatedOnDate = table.Column<DateTime>(nullable: false),
                    LastModifiedByUserId = table.Column<string>(nullable: true),
                    LastModifiedOnDate = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(maxLength: 250, nullable: false),
                    TitleEn = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    HtmlContent = table.Column<string>(nullable: false),
                    HtmlContentEn = table.Column<string>(unicode: false, nullable: true),
                    Banners = table.Column<string>(unicode: false, nullable: false),
                    Thumbnail = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    TotalViews = table.Column<int>(nullable: false, defaultValue: 0),
                    IsEnglishIncluded = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_New", x => x.Id);
                    table.ForeignKey(
                        name: "FK_New_NewCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "NewCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Partner",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    NameEn = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    Thumbnail = table.Column<int>(unicode: false, maxLength: 200, nullable: false),
                    Link = table.Column<string>(unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Partner_PartnerCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "PartnerCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedByUserId = table.Column<string>(nullable: true),
                    CreatedOnDate = table.Column<DateTime>(nullable: false),
                    LastModifiedByUserId = table.Column<string>(nullable: true),
                    LastModifiedOnDate = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(maxLength: 250, nullable: false),
                    TitleEn = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    Summary = table.Column<string>(maxLength: 1024, nullable: false),
                    SummaryEn = table.Column<string>(unicode: false, maxLength: 1024, nullable: true),
                    HtmlContent = table.Column<string>(nullable: false),
                    HtmlContentEn = table.Column<string>(unicode: false, nullable: true),
                    Banners = table.Column<string>(unicode: false, nullable: false),
                    Banner = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Thumbnail = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    LocationId = table.Column<int>(nullable: false),
                    ProjectFieldId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    Investor = table.Column<string>(maxLength: 200, nullable: false),
                    Year = table.Column<int>(nullable: false),
                    IsEnglishIncluded = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Project_ProjectCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ProjectCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Project_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Project_ProjectField_ProjectFieldId",
                        column: x => x.ProjectFieldId,
                        principalTable: "ProjectField",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HomeBanners",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    ProjectId = table.Column<string>(nullable: false),
                    Index = table.Column<int>(nullable: false),
                    ProjectId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeBanners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeBanners_Project_ProjectId1",
                        column: x => x.ProjectId1,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HomeBanners_ProjectId1",
                table: "HomeBanners",
                column: "ProjectId1");

            migrationBuilder.CreateIndex(
                name: "IX_New_CategoryId",
                table: "New",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Partner_CategoryId",
                table: "Partner",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_CategoryId",
                table: "Project",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_LocationId",
                table: "Project",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_ProjectFieldId",
                table: "Project",
                column: "ProjectFieldId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboutArticle");

            migrationBuilder.DropTable(
                name: "AboutProject");

            migrationBuilder.DropTable(
                name: "CompanyHistory");

            migrationBuilder.DropTable(
                name: "HomeBanners");

            migrationBuilder.DropTable(
                name: "New");

            migrationBuilder.DropTable(
                name: "Partner");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "NewCategory");

            migrationBuilder.DropTable(
                name: "PartnerCategory");

            migrationBuilder.DropTable(
                name: "ProjectCategory");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "ProjectField");
        }
    }
}
