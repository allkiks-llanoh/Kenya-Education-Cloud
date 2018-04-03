using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KEC.Curation.Data.Migrations
{
    public partial class removefromPublicationIDinCuratorAssignment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CuratorAssignments_Publications_PublicationId",
                table: "CuratorAssignments");

            migrationBuilder.AlterColumn<int>(
                name: "PublicationId",
                table: "CuratorAssignments",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CuratorAssignments_Publications_PublicationId",
                table: "CuratorAssignments",
                column: "PublicationId",
                principalTable: "Publications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CuratorAssignments_Publications_PublicationId",
                table: "CuratorAssignments");

            migrationBuilder.AlterColumn<int>(
                name: "PublicationId",
                table: "CuratorAssignments",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_CuratorAssignments_Publications_PublicationId",
                table: "CuratorAssignments",
                column: "PublicationId",
                principalTable: "Publications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
