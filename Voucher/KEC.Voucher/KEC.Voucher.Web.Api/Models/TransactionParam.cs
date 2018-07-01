using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KEC.Voucher.Web.Api.Models
{
    public class TransactionParam
    {
        public string voucherPin { get; set; }
        public string VoucherCode { get; set; }
        public string Email { get; set; }
        public decimal Amount { get; set; }
        public string  Description { get; set; }
    }
}