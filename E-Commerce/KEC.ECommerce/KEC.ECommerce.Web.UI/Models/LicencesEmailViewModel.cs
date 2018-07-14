using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.ECommerce.Web.UI.Models
{
    public class LicencesEmailViewModel
    {
        public string CustomerName { get; set; }
        public string OrderNumber { get; set; }
        public string GenDate { get; set; }
        public string CustomerEmail { get; set; }
        public string StoreEmail { get; set; }
        public IEnumerable<LicenceViewModel> Licences { get; set; }
    }
}
