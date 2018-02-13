﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KEC.Voucher.Web.Api.Models
{
    public class TransactionParam
    {
        public string Pin { get; set; }
        public string VoucherCode { get; set; }
        public string AdminGuid { get; set; }
        public decimal Amount { get; set; }
        public string  Description { get; set; }
    }
}