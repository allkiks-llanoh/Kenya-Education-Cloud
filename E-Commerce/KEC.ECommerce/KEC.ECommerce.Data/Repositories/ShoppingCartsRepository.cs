using KEC.ECommerce.Data.Database;
using KEC.ECommerce.Data.Models;
using KEC.ECommerce.Data.Repositories.Core;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.ECommerce.Data.Repositories
{
    public class ShoppingCartsRepository : Repository<ShoppingCart>
    {
        private readonly ECommerceDataContext _ecommercerContext;
        public ShoppingCartsRepository(ECommerceDataContext context) : base(context)
        {
            _ecommercerContext = context as ECommerceDataContext;
        }
        public List<ShoppingCartItem> GetShoppingCartItems(int cartId)
        {

            var cartItems = _ecommercerContext.ShoppingCartItems.Where(p => p.CartId.Equals(cartId))
                                    .Include(p => p.Publication).ToList();
            return cartItems;
        }
        public decimal GetShoppingTotalCost(int cartId)
        {

            var totalCost = _ecommercerContext.ShoppingCartItems
                                              .Where(p => p.CartId.Equals(cartId))
                                              .Sum(p => p.Quantity * p.UnitPrice);
                                    
            return totalCost;
        }
        public long GetShoppingCartItemsCount(int cartId)
        {

            var count = _ecommercerContext.ShoppingCartItems
                                                .Where(p => p.CartId.Equals(cartId))
                                                .Sum(p => p.Quantity);
            return count;
        }
    }
}
