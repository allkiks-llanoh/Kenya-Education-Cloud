using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KEC.Curation.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Levels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Levels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PublicationStages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Level = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicationStages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubjectTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    SubjectTypeId = table.Column<int>(nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subjects_SubjectTypes_SubjectTypeId",
                        column: x => x.SubjectTypeId,
                        principalTable: "SubjectTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Publications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuthorName = table.Column<string>(nullable: true),
                    CertificateNumber = table.Column<string>(nullable: true),
                    CertificateUrl = table.Column<string>(nullable: true),
                    CompletionDate = table.Column<DateTime>(nullable: false),
                    CreatedTimeUtc = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    GradeId = table.Column<int>(nullable: false),
                    ISBNNumber = table.Column<string>(nullable: true),
                    KICDNumber = table.Column<string>(nullable: true),
                    LevelId = table.Column<int>(nullable: true),
                    MimeType = table.Column<string>(nullable: true),
                    ModifiedTimeUtc = table.Column<DateTime>(nullable: false),
                    Owner = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    PublisherName = table.Column<string>(nullable: true),
                    SubjectId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Publications_Levels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Levels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Publications_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PublicationSections",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    Owner = table.Column<string>(nullable: true),
                    PublicationId = table.Column<int>(nullable: false),
                    SectionDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicationSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PublicationSections_Publications_PublicationId",
                        column: x => x.PublicationId,
                        principalTable: "Publications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PublicationStageLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActionTaken = table.Column<string>(nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    Owner = table.Column<string>(nullable: true),
                    PublicationId = table.Column<int>(nullable: false),
                    PublicationStageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicationStageLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PublicationStageLogs_Publications_PublicationId",
                        column: x => x.PublicationId,
                        principalTable: "Publications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PublicationStageLogs_PublicationStages_PublicationStageId",
                        column: x => x.PublicationStageId,
                        principalTable: "PublicationStages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CuratorAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AssignedBy = table.Column<string>(nullable: true),
                    Assignee = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    PublicationSectionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuratorAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CuratorAssignments_PublicationSections_PublicationSectionId",
                        column: x => x.PublicationSectionId,
                        principalTable: "PublicationSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CuratorAssignments_PublicationSectionId",
                table: "CuratorAssignments",
                column: "PublicationSectionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Publications_LevelId",
                table: "Publications",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Publications_SubjectId",
                table: "Publications",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationSections_PublicationId",
                table: "PublicationSections",
                column: "PublicationId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationStageLogs_PublicationId",
                table: "PublicationStageLogs",
                column: "PublicationId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationStageLogs_PublicationStageId",
                table: "PublicationStageLogs",
                column: "PublicationStageId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_SubjectTypeId",
                table: "Subjects",
                column: "SubjectTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CuratorAssignments");

            migrationBuilder.DropTable(
                name: "PublicationStageLogs");

            migrationBuilder.DropTable(
                name: "PublicationSections");

            migrationBuilder.DropTable(
                name: "PublicationStages");

            migrationBuilder.DropTable(
                name: "Publications");

            migrationBuilder.DropTable(
                name: "Levels");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "SubjectTypes");
        }
    }
}
