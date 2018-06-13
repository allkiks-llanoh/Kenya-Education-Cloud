using KEC.ECommerce.Data.UnitOfWork.Core;
using KEC.ECommerce.Web.UI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KEC.ECommerce.Web.UI.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IUnitOfWork _uow;

        public OrdersController(IUnitOfWork uow)
        {
            _uow = uow;
        }
       
        [HttpGet]
        public IActionResult Payment(int orderId)
        {
            //TODO: Filter order by current logged in user
            var order = _uow.OrdersRepository.Get(orderId);
            var model = new OrderViewModel(_uow, order, true);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProcessVoucher(int orderId,string voucherCode)
        {
            //TODO: Request pin from the api
            return PartialView("_PinRequestPartial");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CompletePayment(int orderId , string voucherCode)
        {
            //TODO: Verify Pin with the api
            //TODO: Mark order as paid
            //TOD0: Deduct purchased quantities
            //TODO: Post the transaction to voucher api
            //TODO: Generate Licences
            return View();
        }
    }
}
