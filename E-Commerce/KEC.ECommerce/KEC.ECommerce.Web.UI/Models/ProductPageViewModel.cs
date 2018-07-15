using KEC.ECommerce.Web.UI.Pagination;
using System.Collections.Generic;

namespace KEC.ECommerce.Web.UI.Models
{
    public class ProductPageViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; set; }

        public Pager Pager { get; set; }
    }
}
