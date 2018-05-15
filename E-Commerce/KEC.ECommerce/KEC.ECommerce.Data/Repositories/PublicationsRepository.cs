using KEC.ECommerce.Data.Database;
using KEC.ECommerce.Data.Models;
using KEC.ECommerce.Data.Repositories.Core;

namespace KEC.ECommerce.Data.Repositories
{
    public class PublicationsRepository : Repository<Publication>
    {
        public PublicationsRepository(ECommerceDataContext context) : base(context)
        {
        }
    }
}
