﻿using System;
using System.Threading.Tasks;
using KEC.ECommerce.Data.UnitOfWork.Core;
using KEC.ECommerce.Web.UI.Helpers;
using KEC.ECommerce.Web.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace KEC.ECommerce.Web.UI.Controllers
{
    [Authorize]
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
            if (productQty>= quantity)
            {
                await cartActions.AddItem(productId, quantity);
            }
            else
            {
                ModelState.AddModelError("", $"only {productQty} piece(s) remaining");
            }
          
            return CreateView(cartActions, "_ShoppingCartPartial");
        }
      
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
            var email = User.FindFirst("Email")?.Value;
            var orderId = cartActions.CreateOrder(email);
            return RedirectToAction("Payment", "Orders", new { orderId });
        }
    }
}
