using KEC.Voucher.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEC.Voucher.Data.Mappings
{
    internal class DbWalletsMap : EntityTypeConfiguration<DbWallet>
    {
        public DbWalletsMap()
        {
            this.ToTable("Wallets")
                .HasKey(t => t.Id)
                .HasRequired(t => t.Voucher);
                
        }
    }
    
}
