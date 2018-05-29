using KEC.ECommerce.Data.Database;
using KEC.ECommerce.Data.Models;
using KEC.ECommerce.Data.Repositories.Core;

namespace KEC.ECommerce.Data.Repositories
{
    public class AuthorsRepository : Repository<Author>
    {
        public AuthorsRepository(ECommerceDataContext context) : base(context)
        {
        }
    }
}
