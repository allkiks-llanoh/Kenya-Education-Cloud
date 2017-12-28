using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEC.Voucher.Data.Models
{
    public class DbTransaction
    {
        public int Id { get; set; }
        public int VoucherId { get; set; }
        public string TransactionDescription { get; set; }
        public int PinId { get; set; }
        public int SchoolAdminId { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public virtual DbSchoolAdmin SchoolAdmin { get; set; }
        public virtual DbVoucher Voucher { get; set; }
    }
}
