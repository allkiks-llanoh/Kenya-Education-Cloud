using KEC.ECommerce.Data.Models;

namespace KEC.ECommerce.Web.UI.Helpers
{
    public class Customer
    {
        public string IdentificationCode { get; set; }
        public string Name { get; set; }
        public Order Order { get; set; }
       public PDFParams PDFParams { get; set; }
    }
}