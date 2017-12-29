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
        public int SchoolId { get; set; }
        public int StatusId { get; set; }
        public int WalletId { get; set; }
        public int BatchId { get; set; }
        public virtual ICollection<DbTransaction> Transactions { get; set; }
        public virtual ICollection<DbVoucherPin> VoucherPins { get; set; }
        public virtual DbWallet Wallet { get; set; }
        public virtual DbSchool School { get; set; }
        public virtual DbBatch Batch { get; set; }
        public virtual ICollection<DbStatus> VoucherStatuses { get; set; }


    }
}
