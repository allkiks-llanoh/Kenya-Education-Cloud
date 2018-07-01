using KEC.ECommerce.Web.UI.Pagination;
using System.Collections.Generic;

namespace KEC.ECommerce.Web.UI.Models
{
    public class LicencePageViewModel
    {
        public IEnumerable<LicenceViewModel> Licences { get; set; }

        public Pager Pager { get; set; }
    }
}
