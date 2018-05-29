using KEC.ECommerce.Data.Database;
using KEC.ECommerce.Data.Models;
using KEC.ECommerce.Data.Repositories.Core;

namespace KEC.ECommerce.Data.Repositories
{
    public class PublishersRepository : Repository<Publisher>
    {
        public PublishersRepository(ECommerceDataContext context) : base(context)
        {
        }
    }
}
