using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEC.Voucher.Data.Models
{
    public class DbVoucher
    {
        public int Id { get; set; }
        public string VoucherCode { get; set; }
        public string VoucherSerial { get; set; }
        public int VoucherYear { get; set; }
        public DbSchool SchoolId { get; set; }
        public DbStatus StatusId { get; set; }
        public DbWallet WalletId { get; set; }
        public ICollection<DbTransaction> Transactions { get; set; }

    }
}
