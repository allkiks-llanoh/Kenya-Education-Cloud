using KEC.ECommerce.Data.Models;
using KEC.ECommerce.Data.UnitOfWork.Core;

namespace KEC.ECommerce.Web.UI.Models
{
    public class ShoppingCartItemViewModel
    {
        private readonly IUnitOfWork _uow;
        private readonly ShoppingCartItem _cartItem;

        public ShoppingCartItemViewModel(IUnitOfWork uow, ShoppingCartItem shoppingCartItem)
        {
            _uow = uow;
            _cartItem = shoppingCartItem;
        }
        public int Id
        {
            get
            {
                return _cartItem.Id;
            }
        }
        public int ProductId
        {
            get
            {
                return _cartItem.PublicationId;
            }
        }
        public int Quantity
        {
            get
            {
                return _cartItem.Quantity;
            }
        }
        public decimal UnitPrice
        {
            get
            {
                return _cartItem.UnitPrice;
            }
        }
        public string Description
        {
            get
            {
                var product = _uow.PublicationsRepository.GetPublicationDetails(_cartItem.PublicationId);
                return $"{product.Level.Name} - {product?.Title} by {product?.Author?.FirstName} {product?.Author?.LastName}({product?.Publisher?.Company})";
            }
        }
        public string TotalCost
        {
            get
            {
                return string.Format("{0:#.00}", _cartItem.UnitPrice * _cartItem.Quantity);
            }
        }
    }
}
