using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KEC.ECommerce.Data.Migrations
{
    public partial class PurchasedBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PurchasedBookId",
                table: "Publications",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PurchasedBooks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PublicationId = table.Column<int>(nullable: false),
                    IdentificationCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchasedBooks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Publications_PurchasedBookId",
                table: "Publications",
                column: "PurchasedBookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Publications_PurchasedBooks_PurchasedBookId",
                table: "Publications",
                column: "PurchasedBookId",
                principalTable: "PurchasedBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publications_PurchasedBooks_PurchasedBookId",
                table: "Publications");

            migrationBuilder.DropTable(
                name: "PurchasedBooks");

            migrationBuilder.DropIndex(
                name: "IX_Publications_PurchasedBookId",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "PurchasedBookId",
                table: "Publications");
        }
    }
}
