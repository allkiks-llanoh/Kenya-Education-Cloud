using Microsoft.EntityFrameworkCore.Migrations;

namespace KEC.ECommerce.Data.Migrations
{
    public partial class PaymentStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderNumber",
                table: "PurchasedBooks",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PaymentStatus",
                table: "PurchasedBooks",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderNumber",
                table: "PurchasedBooks");

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "PurchasedBooks");
        }
    }
}
