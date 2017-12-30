using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEC.Voucher.Data.Models
{
    public class DbVoucherPin
    {
        public int Id { get; set; }
        public int VoucherId{ get; set; }
        public string Status { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public virtual DbVoucher Voucher { get; set; }

    }
}
