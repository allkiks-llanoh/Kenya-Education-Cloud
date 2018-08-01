using Microsoft.EntityFrameworkCore.Migrations;

namespace KEC.ECommerce.Data.Migrations
{
    public partial class AddvoucherCodeToPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VoucherNumber",
                table: "Payments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VoucherNumber",
                table: "Payments");
        }
    }
}
