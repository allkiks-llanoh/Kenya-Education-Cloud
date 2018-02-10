using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KEC.Curation.Data.Migrations
{
    public partial class UpdatedCuratorCreationModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "CuratorCreations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SirName",
                table: "CuratorCreations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "CuratorCreations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CuratorCreations_SubjectId",
                table: "CuratorCreations",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_CuratorCreations_Subjects_SubjectId",
                table: "CuratorCreations",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CuratorCreations_Subjects_SubjectId",
                table: "CuratorCreations");

            migrationBuilder.DropIndex(
                name: "IX_CuratorCreations_SubjectId",
                table: "CuratorCreations");

            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "CuratorCreations");

            migrationBuilder.DropColumn(
                name: "SirName",
                table: "CuratorCreations");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "CuratorCreations");
        }
    }
}
