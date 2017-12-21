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
        public DbVoucher VoucherId { get; set; }
        public DbOrder OrderTotal { get; set; }
        public string TransactionDescription { get; set; }
        public int PinId { get; set; }
        public DbSchoolAdmin SchoolAdminId { get; set; }
        public ICollection<DbOrder> Order { get; set; }
        public DateTime CreatedOnUtc { get; set; }
    }
}
