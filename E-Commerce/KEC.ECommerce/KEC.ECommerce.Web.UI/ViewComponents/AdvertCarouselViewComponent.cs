using KEC.ECommerce.Data.UnitOfWork.Core;
using KEC.ECommerce.Web.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.ECommerce.Web.UI.ViewComponents
{
    public class AdvertCarouselViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _uow;

        public AdvertCarouselViewComponent(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var publications =await _uow.PublicationsRepository.TopPublicationsAsyc();
            var productsList = publications.Any() ?
                               publications.Distinct().Select(p => new ProductViewModel(_uow, p)).ToList() : new List<ProductViewModel>();
            return View(productsList);
        }
    }
}
