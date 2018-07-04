using KEC.ECommerce.Web.UI.Pagination;
using System.Collections.Generic;

namespace KEC.ECommerce.Web.UI.Models
{
    public class PurchasePageViewModel
    {
        public IEnumerable<OrderViewModel> Orders { get; set; }

        public Pager Pager { get; set; }
    }
}
