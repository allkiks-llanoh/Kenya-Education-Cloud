using KEC.Voucher.Data.Models;
using System.Data.Entity.ModelConfiguration;
namespace KEC.Voucher.Data.Mappings
{
    internal class DbSchoolAdminMap : EntityTypeConfiguration<DbSchoolAdmin>
    {
        public DbSchoolAdminMap()
        {
            ToTable("ShoolAdmins");
        }
    }
}
