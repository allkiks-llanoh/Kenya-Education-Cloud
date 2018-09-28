using KEC.ECommerce.Data.UnitOfWork.Core;
using KEC.ECommerce.Web.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.ECommerce.Web.UI.ViewComponents
{
    public class BestSellerViewComponent : ViewComponent
    {

        private readonly IUnitOfWork _uow;

        public BestSellerViewComponent(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var publications = await _uow.ShoppingCartItemsRepository.BestSellerPublicationsAsyc();
            var productsList = publications.Any() ?
                               publications.Distinct().Select(p => new BestSellerViewModel(_uow, p)).ToList() : new List<BestSellerViewModel>();
            return View(productsList);
        }
    }
}
