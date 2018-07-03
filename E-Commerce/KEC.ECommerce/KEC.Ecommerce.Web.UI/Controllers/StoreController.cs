using KEC.ECommerce.Data.Models;
using KEC.ECommerce.Data.UnitOfWork.Core;
using KEC.ECommerce.Web.UI.Models;
using KEC.ECommerce.Web.UI.Pagination;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace KEC.ECommerce.Web.UI.Controllers
{
    public class StoreController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IPageHelper<Publication> _paginationHelper;

        public StoreController(IUnitOfWork uow, IPageHelper<Publication> pageHelper)
        {
            _uow = uow;
            _paginationHelper = pageHelper;
            _paginationHelper.PageConfig.PageSize = 24;
        }
        public IActionResult Publications(int categoryId, int pageNumber=1,string searchTerm=null)
        {
            var category = _uow.CategoriesRepository.Get(categoryId);
            ViewBag.Category = category?.Name;
            ViewBag.CategoryId = category?.Id;
            var query = _uow.PublicationsRepository.QueryablePublications(categoryId,searchTerm?.ToLower());
            var result = _paginationHelper.GetPage(query, pageNumber);
            var model = new ProductPageViewModel
            {
                Products = result.Items?.Select(p => new ProductViewModel(_uow, p)),
                Pager = result.Pager
            };
            if (searchTerm!=null)
            {
                return PartialView("_PublicationListPartial", model);
            }
            else
            {
                return View(model);
            }
           

        }
    }
}