using KEC.ECommerce.Data.Models;
using KEC.ECommerce.Data.UnitOfWork.Core;
using System.Collections.Generic;
using System.Linq;

namespace KEC.ECommerce.Web.UI.Models
{
    public class ShoppingCartViewModel
    {
        private readonly IUnitOfWork _uow;
        private readonly ShoppingCart _shoppingCart;
        private readonly bool _includeItems;

        public ShoppingCartViewModel(IUnitOfWork uow, ShoppingCart shoppingCart, bool includeItems=false)
        {
            _uow = uow;
            _shoppingCart = shoppingCart;
            _includeItems = includeItems;
        }
        public long ItemsCount
        {
            get
            {
                return _uow.ShoppingCartsRepository
                          .GetShoppingCartItemsCount(_shoppingCart.Id);
            }
        }

        public List<ShoppingCartItemViewModel> Items
        {
            get
            {
                List<ShoppingCartItemViewModel> itemsList = default(List<ShoppingCartItemViewModel>);
                if (_includeItems)
                {
                    var items = _uow.ShoppingCartsRepository.GetShoppingCartItems(_shoppingCart.Id);
                    itemsList = items.Any() ?
                        items.Select(p => new ShoppingCartItemViewModel(_uow, p)).ToList() : new List<ShoppingCartItemViewModel>();

                }
                return itemsList;
            }
        }
        public int Id
        {
            get
            {
                return _shoppingCart.Id;
            }
        }
        public string TotalCost
        {
            get
            {
                return string.Format("KES {0:#.00}", _uow.ShoppingCartsRepository.GetShoppingTotalCost(_shoppingCart.Id));
            }
        }

    }
}
