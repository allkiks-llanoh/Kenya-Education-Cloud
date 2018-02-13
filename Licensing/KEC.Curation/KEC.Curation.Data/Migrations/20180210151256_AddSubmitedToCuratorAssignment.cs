using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KEC.Curation.Data.Migrations
{
    public partial class AddSubmitedToCuratorAssignment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "CuratorAssignments",
                newName: "CreatedUtc");

            migrationBuilder.AddColumn<bool>(
                name: "FullyAssigned",
                table: "Publications",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Submitted",
                table: "CuratorAssignments",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullyAssigned",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "Submitted",
                table: "CuratorAssignments");

            migrationBuilder.RenameColumn(
                name: "CreatedUtc",
                table: "CuratorAssignments",
                newName: "CreatedAt");
        }
    }
}
