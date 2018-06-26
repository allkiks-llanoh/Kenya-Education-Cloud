using KEC.ECommerce.Data.Database;
using KEC.ECommerce.Data.Helpers;
using KEC.ECommerce.Data.Models;
using KEC.ECommerce.Data.Repositories.Core;
using System.Linq;

namespace KEC.ECommerce.Data.Repositories
{
    public class LicencesRepository : Repository<Licence>
    {
        private readonly ECommerceDataContext _ecommerceContext;

        public LicencesRepository(ECommerceDataContext context) : base(context)
        {
            _ecommerceContext = context as ECommerceDataContext;
        }
        public string GetNextLicence(string prefix = "KECLC#")
        {
            var code = string.Empty;
            do
            {
                code = RandomCodeGenerator.GetLicenceNumber(prefix);
            } while ((Find(p => p.Code.Equals(code)).FirstOrDefault() != null));
            return code;
        }
    }
}
