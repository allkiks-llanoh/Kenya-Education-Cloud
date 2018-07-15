using Microsoft.EntityFrameworkCore.Migrations;

namespace KEC.ECommerce.Data.Migrations
{
    public partial class AddContentUrlToPublication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentUrl",
                table: "Publications",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentUrl",
                table: "Publications");
        }
    }
}
