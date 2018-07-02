using System.Threading.Tasks;
using KEC.ECommerce.Data.UnitOfWork.Core;
using KEC.ECommerce.Web.UI.Helpers;
using KEC.ECommerce.Web.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



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
            var productQty = _uow.PublicationsRepository.Get(productId)?.Quantity;
            if (User.Identity.IsAuthenticated)
            {
                if (productQty >= quantity)
                {
                    await cartActions.AddItem(productId, quantity);
                }
                else
                {
                    ModelState.AddModelError("", $"only {productQty} piece(s) remaining");
                }
            }
            else
            {
                ModelState.AddModelError("", "Please login or sign up first");
            }
          
            return CreateView(cartActions, "_ShoppingCartPartial");
        }
        [Authorize]
        public IActionResult Cart()
        {
            var cartActions = new ShoppingCartActions(_uow, HttpContext);
            var model = new ShoppingCartViewModel(_uow, cartActions.ShoppingCart, true);
            return View(model);
        }
        private IActionResult CreateView(ShoppingCartActions cartActions, string view, bool includeItems = false,string errorMessage=null)
        {
            var model = new ShoppingCartViewModel(_uow, cartActions.ShoppingCart, includeItems);
            if (errorMessage != null)
            {
                ModelState.AddModelError("",errorMessage);
            }
            return PartialView(view, model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromCart(int productId)
        {
            var cartActions = new ShoppingCartActions(_uow, HttpContext);
            var cart = cartActions.ShoppingCart;
            cartActions.RemoveItem(productId);
            return CreateView(cartActions, "_ShoppingCartItemsPartial", true);
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult DestroyCart()
        {
            var cartActions = new ShoppingCartActions(_uow, HttpContext);
            var cart = cartActions.ShoppingCart;
            cartActions.DestroyCart();
            return RedirectToAction("Dashboard", "Account");
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult CheckOut()
        {
            var cartActions = new ShoppingCartActions(_uow, HttpContext);
            var email = User.FindFirst("Email")?.Value;
            var orderId = cartActions.CreateOrder(email);
            return RedirectToAction("Payment", "Orders", new { orderId });
        }
    }
}
