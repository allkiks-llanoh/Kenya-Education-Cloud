using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KEC.ECommerce.Data.UnitOfWork.Core;
using KEC.ECommerce.Web.UI.Helpers;
using KEC.ECommerce.Web.UI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KEC.ECommerce.Web.UI.Controllers
{
    public class ShoppingController : Controller
    {
        private readonly IUnitOfWork _uow;

        public ShoppingController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            var cartActions = new ShoppingCartActions(_uow, HttpContext);
            await cartActions.AddItem(productId, quantity);
            return CreateView(cartActions, "_ShoppingCartPartial");
        }
        public IActionResult Cart()
        {
            var cartActions = new ShoppingCartActions(_uow, HttpContext);
            var model = new ShoppingCartViewModel(_uow, cartActions.ShoppingCart, true);
            return View(model);
        }
        private IActionResult CreateView(ShoppingCartActions cartActions, string view, bool includeItems = false)
        {
            var model = new ShoppingCartViewModel(_uow, cartActions.ShoppingCart, includeItems);
            return PartialView(view, model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromCart(int productId)
        {
            var cartActions = new ShoppingCartActions(_uow, HttpContext);
            var cart = cartActions.ShoppingCart;
            cartActions.RemoveItem(productId);
            return CreateView(cartActions, "_ShoppingCartItemsPartial", true);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DestroyCart()
        {
            var cartActions = new ShoppingCartActions(_uow, HttpContext);
            var cart = cartActions.ShoppingCart;
            cartActions.DestroyCart();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CheckOut()
        {
            var cartActions = new ShoppingCartActions(_uow, HttpContext);
            //TODO: Use logged in user guid
            var guid = Guid.NewGuid().ToString();
            var orderId = cartActions.CreateOrder(guid);
            return RedirectToAction("Payment", "Orders", new { orderId });
        }
    }
}
