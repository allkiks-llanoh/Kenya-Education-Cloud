using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.ECommerce.Web.UI.Models
{
    public class PinRequestViewModel
    {
        public int OrderId { get; private set; }
        public string VoucherCode { get; private set; }
        public string VoucherPin { get; set; }
        public PinRequestViewModel(int orderId,string voucherCode)
        {
            OrderId = orderId;
            VoucherCode = voucherCode;
        }
    }
}
