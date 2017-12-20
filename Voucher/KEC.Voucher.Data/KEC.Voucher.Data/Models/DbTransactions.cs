using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEC.Voucher.Data.Models
{
    class DbTransactions
    {
        public int Id { get; set; }
        public DbVouchers VoucherId { get; set; }
        public DbOrders OrderTotal { get; set; }
        public string TransactionDescription { get; set; }
        public int PinId { get; set; }
        public DbSchoolAdmin SchoolAdminId { get; set; }
        public ICollection<DbOrders> Order { get; set; }
        public DateTime CreatedOnUtc { get; set; }
    }
}
