using KEC.Voucher.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEC.Voucher.Data.Mappings
{
    internal class DbVouchersMap : EntityTypeConfiguration<DbVoucher>
    {
        public DbVouchersMap()
        {
            this.ToTable("Vouchers");
            // Transactions
            HasMany(t => t.Transactions)
             .WithRequired(t => t.Voucher)
             .WillCascadeOnDelete(false);
            // Voucher Pins
            HasMany(t => t.VoucherPins)
                .WithRequired(t => t.Voucher)
                .WillCascadeOnDelete(false);
            // Voucher
            HasOptional(x => x.Status)
            .WithOptionalDependent(l => l.Voucher);
            //Wallet
            HasOptional(x => x.Wallet)
            .WithOptionalDependent(l => l.Voucher);
        }
    }

}
