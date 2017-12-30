using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEC.Voucher.Data.Models
{
   public class DbStatus
    {
        public int Id { get; set; }
        public string StatusName { get; set; }
        public string StatusDescription { get; set; }
        public int VoucherId { get; set; }
        public DateTime TimeStamp { get; set; }
        public virtual DbVoucher Voucher { get; set; }
    }
}
