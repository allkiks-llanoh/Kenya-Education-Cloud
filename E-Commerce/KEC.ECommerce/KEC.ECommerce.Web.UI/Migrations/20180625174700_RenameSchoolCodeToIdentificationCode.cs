using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KEC.ECommerce.Web.UI.Migrations
{
    public partial class RenameSchoolCodeToIdentificationCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SchoolCode",
                table: "AspNetUsers",
                newName: "IdentificationCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdentificationCode",
                table: "AspNetUsers",
                newName: "SchoolCode");
        }
    }
}
