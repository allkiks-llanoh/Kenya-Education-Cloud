using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEC.Voucher.Data.Models
{
    class DbVouchers
    {
        public int Id { get; set; }
        public string VoucherCode { get; set; }
        public string VoucherSerial { get; set; }
        public int VoucherYear { get; set; }
        public DbSchools SchoolId { get; set; }
        public DbStatuses StatusId { get; set; }
        public DbWallets WalletId { get; set; }
        public ICollection<DbTransactions> Transactions { get; set; }

    }
}
