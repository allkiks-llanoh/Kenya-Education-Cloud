using KEC.Voucher.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEC.Voucher.Data.Mappings
{
    internal class DbCountiesMap : EntityTypeConfiguration<DbCounty>
    {
        public DbCountiesMap()
        {
            ToTable("Counties")
                .HasKey(t => t.Id);
            // Batches
            HasMany(t => t.Batches)
                .WithRequired(t => t.County)
                .WillCascadeOnDelete(false);
            // Schools
            HasMany(t => t.Schools)
                .WithRequired(t => t.County)
                .WillCascadeOnDelete(false);
                
        }
    }
}
