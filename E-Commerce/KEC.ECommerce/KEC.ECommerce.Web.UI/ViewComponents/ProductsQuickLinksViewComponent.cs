using KEC.ECommerce.Data.UnitOfWork.Core;
using KEC.ECommerce.Web.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace KEC.ECommerce.Web.UI.ViewComponents
{
    public class ProductsQuickLinksViewComponent: ViewComponent
    {
        private readonly IUnitOfWork _uow;

        public ProductsQuickLinksViewComponent(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public IViewComponentResult Invoke()
        {
            var categories = _uow.CategoriesRepository.GetAll();
            var categoryList = categories.Any() ? categories.Select(p => new CategoryViewModel(_uow, p)) : new List<CategoryViewModel>();
            return View(categoryList?.OrderBy(p => p.Name));
        }
    }
}
