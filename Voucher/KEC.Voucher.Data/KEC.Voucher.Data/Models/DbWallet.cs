using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEC.Voucher.Data.Models
{
    public class DbWallet
    {
        public int Id { get; set; }
        public DbVoucher VoucherId { get; set; }
        public decimal WalletAmount { get; set; }
        public DbSchool SchoolID { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public decimal Balance { get; set; }
        public DateTime UpdatedOnUtc { get; set; }


    }
}
