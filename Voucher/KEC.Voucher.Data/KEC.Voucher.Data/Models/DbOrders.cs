using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEC.Voucher.Data.Models
{
    class DbOrders
    {
        public int Id { get; set; }
        public string OrderGuid { get; set; }
        public string StorId { get; set; }
        public string CustomerId { get; set; }
        public string OrderNumber { get; set; }
        public decimal OrderTotal { get; set; }


    }
}
