using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KEC.Curation.Data.Migrations
{
    public partial class AddChiefCuratorAssignmentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChiefCuratorAssignmentId",
                table: "Publications",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ChiefCuratorAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AssignmetDateUtc = table.Column<DateTime>(nullable: false),
                    ChiefCuratorGuid = table.Column<string>(nullable: true),
                    PrincipalCuratorGuid = table.Column<string>(nullable: true),
                    PublicationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiefCuratorAssignments", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Publications_ChiefCuratorAssignmentId",
                table: "Publications",
                column: "ChiefCuratorAssignmentId",
                unique: true,
                filter: "[ChiefCuratorAssignmentId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Publications_ChiefCuratorAssignments_ChiefCuratorAssignmentId",
                table: "Publications",
                column: "ChiefCuratorAssignmentId",
                principalTable: "ChiefCuratorAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publications_ChiefCuratorAssignments_ChiefCuratorAssignmentId",
                table: "Publications");

            migrationBuilder.DropTable(
                name: "ChiefCuratorAssignments");

            migrationBuilder.DropIndex(
                name: "IX_Publications_ChiefCuratorAssignmentId",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "ChiefCuratorAssignmentId",
                table: "Publications");
        }
    }
}
