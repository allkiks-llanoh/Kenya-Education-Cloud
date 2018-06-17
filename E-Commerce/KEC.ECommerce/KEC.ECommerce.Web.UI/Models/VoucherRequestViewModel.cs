using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.ECommerce.Web.UI.Models
{
    public class VoucherRequestViewModel
    {
        public int OrderId { get; private set; }
        public string ErrorMessage { get; private set; }
        public VoucherRequestViewModel(int orderId,string errorMessage=null)
        {
            ErrorMessage = errorMessage;
            OrderId = orderId;
        }
    }
}
