using KEC.ECommerce.Data.UnitOfWork.Core;
using KEC.ECommerce.Web.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace KEC.ECommerce.Web.UI.ViewComponents
{
    public class TopProductsViewComponent: ViewComponent
    {
        private readonly IUnitOfWork _uow;

        public TopProductsViewComponent( IUnitOfWork uow)
        {
            _uow = uow;
        }
        public IViewComponentResult Invoke()
        {
            var categories = _uow.CategoriesRepository.GetAll();
            var categoryList = categories.Any() ?
                categories.Select(p => new CategoryViewModel(_uow, p,true)).ToList() : new List<CategoryViewModel>();
            return View(categoryList);
        }
    }
}
