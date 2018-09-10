using KEC.ECommerce.Data.Models;
using KEC.ECommerce.Data.UnitOfWork.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.ECommerce.Web.UI.Helpers
{
    public class ShoppingCartActions
    {
        private readonly IUnitOfWork _uow;
        private readonly HttpContext _context;
        public ShoppingCart ShoppingCart
        {
            get
            {
                return _uow.ShoppingCartsRepository.Get(GetCartId());
            }
        }
        public ShoppingCartActions(IUnitOfWork uow, HttpContext context)
        {
            _uow = uow;
            _context = context;
        }
        public async Task AddItem(int productId, int quantity)
        {
            var cartId = GetCartId();
            var item = _uow.ShoppingCartItemsRepository
                .Find(p => p.PublicationId.Equals(productId)
                && p.CartId.Equals(cartId)).FirstOrDefault();
            var unitPrice = await _uow.PublicationsRepository.PublicationUnitPrice(productId);
            if (item == null)
            {
                item = new ShoppingCartItem
                {
                    CartId = cartId,
                    PublicationId = productId,
                    UnitPrice = unitPrice,
                    Quantity = quantity
                };
                _uow.ShoppingCartItemsRepository.Add(item);
                _uow.Complete();
            }
            else
            {
                item.Quantity += quantity;
                _uow.Complete();
            }

        }
        public void RemoveItem(int productId)
        {
            var cartId = GetCartId();
            var item = _uow.ShoppingCartItemsRepository
                .Find(p => p.PublicationId.Equals(productId)
                && p.CartId.Equals(cartId)).FirstOrDefault();
            _uow.ShoppingCartItemsRepository.Remove(item);
            _uow.Complete();
        }
        public int GetCartId()
        {

            var cartId = _context.Session.GetInt32(CartKey());
            if (cartId.HasValue)
            {
                return cartId.Value;
            }
            else
            {
                var cart = new ShoppingCart { CreatedAt = DateTime.Now };
                _uow.ShoppingCartsRepository.Add(cart);
                _uow.Complete();
                _context.Session.SetInt32(CartKey(), cart.Id);
                return cart.Id;
            }

        }

        public void DestroyCart()
        {
            var cartId = GetCartId();
            var cart = _uow.ShoppingCartsRepository.Get(cartId);
            _uow.ShoppingCartsRepository.Remove(cart);
            _uow.Complete();
            _context.Session.Remove(CartKey());

        }
        public int CreateOrder(string customerGuid)
        {
            var cartId = GetCartId();
            var cartItems = _uow.ShoppingCartsRepository.GetShoppingCartItems(cartId);
            var orderId = default(int);
            if (cartItems.Any())
            {
                var order = new Order
                {
                    OrderNumber = _uow.OrdersRepository.GetNextOrderNumber(),
                    CustomerEmail = customerGuid,
                    Amount = cartItems.Sum(p => p.Quantity * p.UnitPrice),
                    SubmittedAt = DateTime.Now,
                    Status = OrderStatus.Submitted
                };
               
                cartItems.ForEach(item =>
                {
                    var lineItem = new LineItem
                    {
                        Order = order,
                        PublicationId = item.PublicationId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice
                    };
                    _uow.LineItemsRepository.Add(lineItem);
                    var userEmail = _context.User.FindFirst("Email")?.Value;
                    var existingBook = _uow.PurchasedBookRepository.Find(p => p.PublicationId.Equals(item.PublicationId)
                                                                          && p.IdentificationCode.Equals(userEmail)).Any();
                    if (existingBook==false)
                    {
                        var purchasedModel = new PurchasedBook
                        {
                            PublicationId = item.PublicationId,
                            IdentificationCode = _context.User.FindFirst("Email")?.Value,
                            PaymentStatus = false,
                            OrderNumber = order.OrderNumber
                        };
                        _uow.PurchasedBookRepository.Add(purchasedModel);
                    }                   
                });
                _uow.OrdersRepository.Add(order);
                _uow.ShoppingCartsRepository.Remove(ShoppingCart);
                _uow.Complete();
                _context.Session.Remove(CartKey());
                orderId = order.Id;
            }
            return orderId;
        }
        private string CartKey()
        {
            var userMail = _context.User.FindFirst("Email")?.Value;
            var cartKey = $"{userMail}-CartId";
            return cartKey;
        }
    }
}
