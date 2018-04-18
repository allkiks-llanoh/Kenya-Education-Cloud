using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KEC.Curation.Data.Migrations
{
    public partial class AddedPublicationSectionToChiefCuratorComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PublicationSectionId",
                table: "ChiefCuratorAssignments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChiefCuratorAssignments_PublicationSectionId",
                table: "ChiefCuratorAssignments",
                column: "PublicationSectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiefCuratorAssignments_PublicationSections_PublicationSectionId",
                table: "ChiefCuratorAssignments",
                column: "PublicationSectionId",
                principalTable: "PublicationSections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiefCuratorAssignments_PublicationSections_PublicationSectionId",
                table: "ChiefCuratorAssignments");

            migrationBuilder.DropIndex(
                name: "IX_ChiefCuratorAssignments_PublicationSectionId",
                table: "ChiefCuratorAssignments");

            migrationBuilder.DropColumn(
                name: "PublicationSectionId",
                table: "ChiefCuratorAssignments");
        }
    }
}
