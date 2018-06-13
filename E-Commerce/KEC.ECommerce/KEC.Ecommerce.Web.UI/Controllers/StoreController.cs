using KEC.ECommerce.Data.UnitOfWork.Core;
using KEC.ECommerce.Web.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace KEC.ECommerce.Web.UI.Controllers
{
    public class StoreController : Controller
    {
        private readonly IUnitOfWork _uow;

        public StoreController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public IActionResult Publications(int categoryId)
        {
            ViewBag.Category = _uow.CategoriesRepository.Get(categoryId)?.Name;
            var publications = _uow.PublicationsRepository.Find(p => p.CategoryId.Equals(categoryId));
            var publicationList = publications.Any() ? 
                publications.Select(p => new ProductViewModel(_uow, p)).ToList() : new List<ProductViewModel>();
            return View(publicationList);
        }
    }
}