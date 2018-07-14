using KEC.ECommerce.Data.UnitOfWork.Core;
using KEC.ECommerce.Web.UI.Helpers;
using KEC.ECommerce.Web.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace KEC.ECommerce.Web.UI.ViewComponents
{
    public class ShoppingCartViewComponent: ViewComponent
    {
        private readonly IUnitOfWork _uow;

        public ShoppingCartViewComponent(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public IViewComponentResult Invoke()
        {
            var cartActions = new ShoppingCartActions(_uow, HttpContext);
            var model = new ShoppingCartViewModel(_uow, cartActions.ShoppingCart);
            return View(model);
        }
    }
}
