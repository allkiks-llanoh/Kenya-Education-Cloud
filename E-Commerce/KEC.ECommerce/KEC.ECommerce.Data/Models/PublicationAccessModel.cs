using System.Collections;
using System.Collections.Generic;
namespace KEC.ECommerce.Data.Models
{
    public class PublicationAccessModel
    {
        public string LicenceKey { get; set; }
        public IEnumerable<string> IdentificationCodes { get; set; }
    }
}
