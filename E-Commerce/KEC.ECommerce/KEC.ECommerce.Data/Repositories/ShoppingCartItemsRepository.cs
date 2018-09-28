using KEC.ECommerce.Data.Database;
using KEC.ECommerce.Data.Models;
using KEC.ECommerce.Data.Repositories.Core;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.ECommerce.Data.Repositories
{
    public class ShoppingCartItemsRepository : Repository<ShoppingCartItem>
    {
        private ECommerceDataContext _ecommerceContext;
        public ShoppingCartItemsRepository(ECommerceDataContext context) : base(context)
        {

            _ecommerceContext = context as ECommerceDataContext;
        }
        public async Task<IEnumerable<ShoppingCartItem>> BestSellerPublicationsAsyc(int count = 6)
        {
            var publications = await (_context as ECommerceDataContext).ShoppingCartItems

                                            .OrderByDescending(p => p.PublicationId)
                                            .Take(count).ToListAsync();
            return publications;
        }
    }
}
