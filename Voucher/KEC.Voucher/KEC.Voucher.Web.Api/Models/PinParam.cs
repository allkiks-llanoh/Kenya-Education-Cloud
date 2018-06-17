using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KEC.Voucher.Web.Api.Models
{
    public class PinParam
    {
        public string VoucherCode { get; set; }
        public string Email { get; set; }
        public decimal Amount { get; set; }
    }
}