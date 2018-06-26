using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KEC.ECommerce.Data.Migrations
{
    public partial class RemoveFirstNameLastNameFromPublisher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Publishers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Publishers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Publishers",
                nullable: true);
        }
    }
}
