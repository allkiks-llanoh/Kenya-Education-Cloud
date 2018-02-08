using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KEC.Curation.Data.Migrations
{
    public partial class RenameGradeIdToLevelId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publications_Levels_LevelId",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "GradeId",
                table: "Publications");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Publications_Levels_LevelId",
                table: "Publications",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
