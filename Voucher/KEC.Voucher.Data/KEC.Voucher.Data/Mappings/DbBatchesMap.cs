using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KEC.Voucher.Data.Models;

namespace KEC.Voucher.Data.Mappings
{
    internal class DbBatchesMap : EntityTypeConfiguration<DbBatches>
    {
        public DbBatchesMap()
        {
            this.ToTable("Batches")
                .HasKey(t => t.Id);

            this.HasRequired(t => t.CountyId)
                .WithMany()
                .HasForeignKey(t => t.CountyId)
                .WillCascadeOnDelete(false);

        }
    }
}
