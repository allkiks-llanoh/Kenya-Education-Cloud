using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KEC.Curation.Data.Migrations
{
    public partial class updatedContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiefCuratorAssignments_PublicationSections_PublicationSectionId",
                table: "ChiefCuratorAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiefCuratorComments_ChiefCuratorAssignments_ChiefCuratorAssignmentId",
                table: "ChiefCuratorComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiefCuratorComments_Publications_PublicationId",
                table: "ChiefCuratorComments");

            migrationBuilder.DropForeignKey(
                name: "FK_CuratorAssignments_PublicationSections_PublicationSectionId",
                table: "CuratorAssignments");

            migrationBuilder.DropIndex(
                name: "IX_CuratorAssignments_PublicationSectionId",
                table: "CuratorAssignments");

            migrationBuilder.DropIndex(
                name: "IX_ChiefCuratorComments_ChiefCuratorAssignmentId",
                table: "ChiefCuratorComments");

            migrationBuilder.DropIndex(
                name: "IX_ChiefCuratorComments_PublicationId",
                table: "ChiefCuratorComments");

            migrationBuilder.DropIndex(
                name: "IX_ChiefCuratorAssignments_PublicationSectionId",
                table: "ChiefCuratorAssignments");

            migrationBuilder.DropColumn(
                name: "ChiefCuratorAssignmentId",
                table: "ChiefCuratorComments");

            migrationBuilder.DropColumn(
                name: "PublicationSectionId",
                table: "ChiefCuratorAssignments");

            migrationBuilder.AddColumn<int>(
                name: "ChiefCuratorAssignmentId",
                table: "PublicationSections",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChiefCuratorAssignmentId1",
                table: "PublicationSections",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CuratorAssignmentId",
                table: "PublicationSections",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PublicationId",
                table: "CuratorAssignments",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Submitted",
                table: "ChiefCuratorComments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Submitted",
                table: "ChiefCuratorAssignments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_PublicationSections_ChiefCuratorAssignmentId",
                table: "PublicationSections",
                column: "ChiefCuratorAssignmentId",
                unique: true,
                filter: "[ChiefCuratorAssignmentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationSections_ChiefCuratorAssignmentId1",
                table: "PublicationSections",
                column: "ChiefCuratorAssignmentId1",
                unique: true,
                filter: "[ChiefCuratorAssignmentId1] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationSections_CuratorAssignmentId",
                table: "PublicationSections",
                column: "CuratorAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CuratorAssignments_PublicationId",
                table: "CuratorAssignments",
                column: "PublicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CuratorAssignments_Publications_PublicationId",
                table: "CuratorAssignments",
                column: "PublicationId",
                principalTable: "Publications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PublicationSections_CuratorAssignments_ChiefCuratorAssignmentId",
                table: "PublicationSections",
                column: "ChiefCuratorAssignmentId",
                principalTable: "CuratorAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PublicationSections_ChiefCuratorAssignments_ChiefCuratorAssignmentId1",
                table: "PublicationSections",
                column: "ChiefCuratorAssignmentId1",
                principalTable: "ChiefCuratorAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PublicationSections_CuratorAssignments_CuratorAssignmentId",
                table: "PublicationSections",
                column: "CuratorAssignmentId",
                principalTable: "CuratorAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CuratorAssignments_Publications_PublicationId",
                table: "CuratorAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_PublicationSections_CuratorAssignments_ChiefCuratorAssignmentId",
                table: "PublicationSections");

            migrationBuilder.DropForeignKey(
                name: "FK_PublicationSections_ChiefCuratorAssignments_ChiefCuratorAssignmentId1",
                table: "PublicationSections");

            migrationBuilder.DropForeignKey(
                name: "FK_PublicationSections_CuratorAssignments_CuratorAssignmentId",
                table: "PublicationSections");

            migrationBuilder.DropIndex(
                name: "IX_PublicationSections_ChiefCuratorAssignmentId",
                table: "PublicationSections");

            migrationBuilder.DropIndex(
                name: "IX_PublicationSections_ChiefCuratorAssignmentId1",
                table: "PublicationSections");

            migrationBuilder.DropIndex(
                name: "IX_PublicationSections_CuratorAssignmentId",
                table: "PublicationSections");

            migrationBuilder.DropIndex(
                name: "IX_CuratorAssignments_PublicationId",
                table: "CuratorAssignments");

            migrationBuilder.DropColumn(
                name: "ChiefCuratorAssignmentId",
                table: "PublicationSections");

            migrationBuilder.DropColumn(
                name: "ChiefCuratorAssignmentId1",
                table: "PublicationSections");

            migrationBuilder.DropColumn(
                name: "CuratorAssignmentId",
                table: "PublicationSections");

            migrationBuilder.DropColumn(
                name: "PublicationId",
                table: "CuratorAssignments");

            migrationBuilder.DropColumn(
                name: "Submitted",
                table: "ChiefCuratorComments");

            migrationBuilder.DropColumn(
                name: "Submitted",
                table: "ChiefCuratorAssignments");

            migrationBuilder.AddColumn<int>(
                name: "ChiefCuratorAssignmentId",
                table: "ChiefCuratorComments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PublicationSectionId",
                table: "ChiefCuratorAssignments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CuratorAssignments_PublicationSectionId",
                table: "CuratorAssignments",
                column: "PublicationSectionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChiefCuratorComments_ChiefCuratorAssignmentId",
                table: "ChiefCuratorComments",
                column: "ChiefCuratorAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiefCuratorComments_PublicationId",
                table: "ChiefCuratorComments",
                column: "PublicationId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ChiefCuratorComments_ChiefCuratorAssignments_ChiefCuratorAssignmentId",
                table: "ChiefCuratorComments",
                column: "ChiefCuratorAssignmentId",
                principalTable: "ChiefCuratorAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiefCuratorComments_Publications_PublicationId",
                table: "ChiefCuratorComments",
                column: "PublicationId",
                principalTable: "Publications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CuratorAssignments_PublicationSections_PublicationSectionId",
                table: "CuratorAssignments",
                column: "PublicationSectionId",
                principalTable: "PublicationSections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
