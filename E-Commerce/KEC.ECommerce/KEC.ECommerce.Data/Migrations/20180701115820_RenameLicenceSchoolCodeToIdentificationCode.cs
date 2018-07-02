using Microsoft.EntityFrameworkCore.Migrations;

namespace KEC.ECommerce.Data.Migrations
{
    public partial class RenameLicenceSchoolCodeToIdentificationCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SchoolCode",
                table: "Licences",
                newName: "IdentificationCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdentificationCode",
                table: "Licences",
                newName: "SchoolCode");
        }
    }
}
