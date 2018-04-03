using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KEC.Curation.Data.Migrations
{
    public partial class addprincipalCuratorCommentRepository : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PrincipalCuratorComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Notes = table.Column<string>(nullable: true),
                    PrincipalCuratorGuid = table.Column<string>(nullable: true),
                    PublicationId = table.Column<int>(nullable: false),
                    Submitted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrincipalCuratorComments", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrincipalCuratorComments");
        }
    }
}
