using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KEC.Curation.Data.Migrations
{
    public partial class AddedPublicationandChiefCuratorAssignmentToChiefCuratorComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChiefCuratorAssignmentId",
                table: "ChiefCuratorComments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChiefCuratorComments_ChiefCuratorAssignmentId",
                table: "ChiefCuratorComments",
                column: "ChiefCuratorAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiefCuratorComments_PublicationId",
                table: "ChiefCuratorComments",
                column: "PublicationId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiefCuratorComments_ChiefCuratorAssignments_ChiefCuratorAssignmentId",
                table: "ChiefCuratorComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiefCuratorComments_Publications_PublicationId",
                table: "ChiefCuratorComments");

            migrationBuilder.DropIndex(
                name: "IX_ChiefCuratorComments_ChiefCuratorAssignmentId",
                table: "ChiefCuratorComments");

            migrationBuilder.DropIndex(
                name: "IX_ChiefCuratorComments_PublicationId",
                table: "ChiefCuratorComments");

            migrationBuilder.DropColumn(
                name: "ChiefCuratorAssignmentId",
                table: "ChiefCuratorComments");
        }
    }
}
