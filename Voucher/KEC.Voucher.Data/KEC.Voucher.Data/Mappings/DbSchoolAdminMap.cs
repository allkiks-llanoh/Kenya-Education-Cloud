using KEC.Voucher.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEC.Voucher.Data.Mappings
{
    internal class DbSchoolAdminMap : EntityTypeConfiguration<DbSchoolAdmin>
    {
        public DbSchoolAdminMap()
        {
            this.ToTable("SchoolAdmin")
                .HasKey(t => t.Id);
        }
    }
}
