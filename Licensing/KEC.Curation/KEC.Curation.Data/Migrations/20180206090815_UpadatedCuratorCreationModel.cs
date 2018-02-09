using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KEC.Curation.Data.Migrations
{
    public partial class UpadatedCuratorCreationModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "CuratorCreations");

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "CuratorCreations",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "CuratorCreations");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "CuratorCreations",
                nullable: true);
        }
    }
}
