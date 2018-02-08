using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KEC.Voucher.Web.Api.Models
{
    public class PinParam
    {
        public string VoucherCode { get; set; }
        public string UserGuid { get; set; }
    }
}