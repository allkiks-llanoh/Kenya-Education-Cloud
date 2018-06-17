using KEC.ECommerce.Data.UnitOfWork.Core;
using KEC.ECommerce.Web.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RestSharp;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KEC.ECommerce.Web.UI.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IConfiguration _configuration;

        public OrdersController(IUnitOfWork uow, IConfiguration configuration)
        {
            _uow = uow;
            _configuration = configuration;
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
           
            var pinEndPoint = _configuration["VoucherPinEndPoint"];
            var client = new RestClient(pinEndPoint);
            var request = new RestRequest(Method.POST);
            var amount = _uow.OrdersRepository.GetOrderTotalCost(orderId);
            var email = HttpContext.User.Identity.Name;
            var pinParam = new { VoucherCode = voucherCode,Amount = amount,Email= email };
            request.AddJsonBody(pinParam);
            var response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var model = new PinRequestViewModel(orderId, voucherCode);
                return PartialView("_PinRequestPartial", model);
            }
            else
            {
                var message = "Your voucher may have insufficient fund or you are not authorized to use it";
                ModelState.AddModelError("", message);
                var model = new VoucherRequestViewModel(orderId, message);
                return PartialView("_VoucherRequestPartial", model);
            }
          
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
