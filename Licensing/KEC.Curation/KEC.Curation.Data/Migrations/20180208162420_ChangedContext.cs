using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KEC.Curation.Data.Migrations
{
    public partial class ChangedContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publications_Levels_LevelId",
                table: "Publications");

            migrationBuilder.DropForeignKey(
                name: "FK_PublicationStageLogs_PublicationStages_PublicationStageId1",
                table: "PublicationStageLogs");

            migrationBuilder.DropTable(
                name: "PublicationStages");

            migrationBuilder.DropIndex(
                name: "IX_PublicationStageLogs_PublicationStageId1",
                table: "PublicationStageLogs");

            migrationBuilder.DropColumn(
                name: "PublicationStageId",
                table: "PublicationStageLogs");

            migrationBuilder.DropColumn(
                name: "PublicationStageId1",
                table: "PublicationStageLogs");

            migrationBuilder.DropColumn(
                name: "GradeId",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "Stage",
                table: "Publications");

            migrationBuilder.AlterColumn<int>(
                name: "ActionTaken",
                table: "PublicationStageLogs",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Stage",
                table: "PublicationStageLogs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "LevelId",
                table: "Publications",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Publications_Levels_LevelId",
                table: "Publications",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publications_Levels_LevelId",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "Stage",
                table: "PublicationStageLogs");

            migrationBuilder.AlterColumn<string>(
                name: "ActionTaken",
                table: "PublicationStageLogs",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "PublicationStageId",
                table: "PublicationStageLogs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PublicationStageId1",
                table: "PublicationStageLogs",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LevelId",
                table: "Publications",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "GradeId",
                table: "Publications",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Stage",
                table: "Publications",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_PublicationStageLogs_PublicationStageId1",
                table: "PublicationStageLogs",
                column: "PublicationStageId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Publications_Levels_LevelId",
                table: "Publications",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PublicationStageLogs_PublicationStages_PublicationStageId1",
                table: "PublicationStageLogs",
                column: "PublicationStageId1",
                principalTable: "PublicationStages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
