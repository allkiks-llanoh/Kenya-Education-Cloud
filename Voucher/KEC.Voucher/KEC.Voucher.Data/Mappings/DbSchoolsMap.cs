using KEC.Voucher.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEC.Voucher.Data.Mappings
{
    internal class DbSchoolsMap : EntityTypeConfiguration<DbSchool>
    {
        public DbSchoolsMap()
        {
            // Schools
            ToTable("Schools");
            HasMany(t => t.Vouchers)
                .WithRequired(t => t.School);
            // Fund Allocations
            HasMany(t => t.FundAllocations)
                .WithRequired(t => t.School)
                .WillCascadeOnDelete(false);
        }
    }

}
