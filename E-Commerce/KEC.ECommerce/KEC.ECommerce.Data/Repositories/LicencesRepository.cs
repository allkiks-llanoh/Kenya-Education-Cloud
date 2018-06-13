using KEC.ECommerce.Data.Database;
using KEC.ECommerce.Data.Models;
using KEC.ECommerce.Data.Repositories.Core;

namespace KEC.ECommerce.Data.Repositories
{
    public class LicencesRepository : Repository<Licence>
    {
        private readonly ECommerceDataContext _ecommerceContext;

        public LicencesRepository(ECommerceDataContext context) : base(context)
        {
            _ecommerceContext = context as ECommerceDataContext;
        }
    }
}
