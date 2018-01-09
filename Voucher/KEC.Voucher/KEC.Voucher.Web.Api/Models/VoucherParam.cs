using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KEC.Voucher.Web.Api.Models
{
    public class VoucherParam
    {
        public VoucherParam()
        {
            SchoolCodes = new List<string>();
        }
        public int BatchId { get; set; }
        public List<string> SchoolCodes { get; set; }
    }
}