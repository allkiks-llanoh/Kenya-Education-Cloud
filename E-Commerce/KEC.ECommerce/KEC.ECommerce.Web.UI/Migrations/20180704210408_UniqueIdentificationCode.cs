using Microsoft.EntityFrameworkCore.Migrations;

namespace KEC.ECommerce.Web.UI.Migrations
{
    public partial class UniqueIdentificationCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IdentificationCode",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_AspNetUsers_IdentificationCode",
                table: "AspNetUsers",
                column: "IdentificationCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_AspNetUsers_IdentificationCode",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "IdentificationCode",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
