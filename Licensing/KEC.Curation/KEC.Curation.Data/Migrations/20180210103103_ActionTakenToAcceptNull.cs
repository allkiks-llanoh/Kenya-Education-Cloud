using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KEC.Curation.Data.Migrations
{
    public partial class ActionTakenToAcceptNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ActionTaken",
                table: "PublicationStageLogs",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ActionTaken",
                table: "PublicationStageLogs",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
