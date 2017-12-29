using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KEC.Voucher.Data.Models;

namespace KEC.Voucher.Data.Mappings
{
    internal class DbBatchesMap : EntityTypeConfiguration<DbBatch>
    {
        public DbBatchesMap()
        {
            // Batches
            this.ToTable("Batches")
                .HasKey(t => t.Id);
            // County
            this.HasRequired(t => t.County)
                .WithMany()
                .HasForeignKey(t => t.CountyId)
                .WillCascadeOnDelete(false);
            // Vouchers
            HasMany(p => p.Vouchers)
                .WithRequired(p => p.Batch)
                .WillCascadeOnDelete(false);
            

        }
    }
}
