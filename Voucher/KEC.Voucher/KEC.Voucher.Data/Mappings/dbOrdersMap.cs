using KEC.Voucher.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEC.Voucher.Data.Mappings
{
    internal class DbOrdersMap : EntityTypeConfiguration<DbOrder>
    {
        public DbOrdersMap()
        {
            this.ToTable("Orders")
                .HasKey(t => t.Id);
        }
    }
}
