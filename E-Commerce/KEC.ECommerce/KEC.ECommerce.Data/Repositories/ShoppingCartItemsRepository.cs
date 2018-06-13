using KEC.ECommerce.Data.Database;
using KEC.ECommerce.Data.Models;
using KEC.ECommerce.Data.Repositories.Core;

namespace KEC.ECommerce.Data.Repositories
{
    public class ShoppingCartItemsRepository : Repository<ShoppingCartItem>
    {
        public ShoppingCartItemsRepository(ECommerceDataContext context) : base(context)
        {
        }
    }
}
