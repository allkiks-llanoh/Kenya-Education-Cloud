using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KEC.Curation.Data.Migrations
{
    public partial class ChangedPublicationStageToEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PublicationStageLogs_PublicationStages_PublicationStageId",
                table: "PublicationStageLogs");

            migrationBuilder.DropTable(
                name: "PublicationStages");

            migrationBuilder.DropIndex(
                name: "IX_PublicationStageLogs_PublicationStageId",
                table: "PublicationStageLogs");

            migrationBuilder.DropColumn(
                name: "PublicationStageId",
                table: "PublicationStageLogs");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "IX_PublicationStageLogs_PublicationStageId",
                table: "PublicationStageLogs",
                column: "PublicationStageId");

            migrationBuilder.AddForeignKey(
                name: "FK_PublicationStageLogs_PublicationStages_PublicationStageId",
                table: "PublicationStageLogs",
                column: "PublicationStageId",
                principalTable: "PublicationStages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
