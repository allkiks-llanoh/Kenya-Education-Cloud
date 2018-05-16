using KEC.ECommerce.Data.Database;
using KEC.ECommerce.Data.Models;
using KEC.ECommerce.Data.Repositories.Core;

namespace KEC.ECommerce.Data.Repositories
{
    public class LevelsRepository : Repository<Level>
    {
        public LevelsRepository(ECommerceDataContext context) : base(context)
        {
        }
    }
}
