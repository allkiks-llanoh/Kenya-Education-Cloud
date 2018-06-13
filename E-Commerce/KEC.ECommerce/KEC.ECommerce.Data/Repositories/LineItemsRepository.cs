using KEC.ECommerce.Data.Database;
using KEC.ECommerce.Data.Models;
using KEC.ECommerce.Data.Repositories.Core;

namespace KEC.ECommerce.Data.Repositories
{
    public class LineItemsRepository : Repository<LineItem>
    {
        private readonly ECommerceDataContext _eCommerceDataContext;

        public LineItemsRepository(ECommerceDataContext context) : base(context)
        {
            _eCommerceDataContext = context as ECommerceDataContext;
        }
    }
}
