using Hangfire;
using KEC.ECommerce.Data.Models;
using KEC.ECommerce.Data.UnitOfWork.Core;
using KEC.ECommerce.Web.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace KEC.ECommerce.Web.UI.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> Payment(int orderId)
        {
            var mail = User.FindFirst("Email")?.Value;
            var order = await _uow.OrdersRepository.GetOrderByUser(orderId, mail, OrderStatus.Submitted);
            var model = default(OrderViewModel);
            if (order != null)
            {
                model = new OrderViewModel(_uow, order, true);
            }
            return View(model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcessVoucher(int orderId, string voucherCode)
        {

            var pinEndPoint = _configuration["VoucherPinEndPoint"];
            var client = new RestClient(pinEndPoint);
            var request = new RestRequest(Method.POST);
            var amount = _uow.OrdersRepository.GetOrderTotalCost(orderId);
            var email = HttpContext.User.Identity.Name;
            var pinParam = new { VoucherCode = voucherCode, Amount = amount, Email = email };
            request.AddJsonBody(pinParam);
            var response = await client.ExecuteTaskAsync(request);
            if (response.IsSuccessful)
            {
                var model = new PinRequestViewModel(orderId, voucherCode);
                return PartialView("_PinRequestPartial", model);
            }
            else
            {
                var message = "Your may have entered an invalid voucher code or the voucher has insufficient fund or you are not authorized to use it";
                ModelState.AddModelError("", message);
                var model = new VoucherRequestViewModel(orderId, voucherCode);
                return PartialView("_VoucherRequestPartial", model);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteOrder(int orderId)
        {
            var order = _uow.OrdersRepository.Get(orderId);
            if (order.Status == OrderStatus.Submitted)
            {
                _uow.OrdersRepository.Remove(order);
                _uow.Complete();
                var model = GetOrders();
                return PartialView("_OrdersPartial", model);
            }
            else
            {
                var model = GetOrders();
                ModelState.AddModelError(string.Empty, "Oders cannot be deleted at this point");
                return PartialView("_OrdersPartial", model);
            }
        }

        private List<OrderViewModel> GetOrders()
        {
            var mail = User.FindFirst("Email")?.Value;
            var orders = _uow.OrdersRepository.Find(p => p.CustomerEmail.Equals(mail) && p.Status == OrderStatus.Submitted);
            var model = orders?.Select(p => new OrderViewModel(_uow, p))?.ToList();
            return model;
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompletePayment(int orderId, string voucherCode, string voucherPin)
        {

            //TODO: Generate Licences
            //Send mail
            var mail = User.FindFirst("Email")?.Value;
            var code = User.FindFirst("IdentificationCode")?.Value;
            var order = await _uow.OrdersRepository.GetOrderByUser(orderId, mail, OrderStatus.Submitted);
            if (order == null)
            {
                var model = new PinRequestViewModel(orderId, voucherCode);
                ModelState.AddModelError(string.Empty, "Order record could not be retrieve");
                return PartialView("_PinRequestPartial", model);
            }
            else
            {
                var transEndPoint = _configuration["VoucherTransactionEndPoint"];
                var client = new RestClient(transEndPoint);
                var request = new RestRequest(Method.POST);
                var amount = _uow.OrdersRepository.GetOrderTotalCost(order.Id);
                var transactionParam = new TransactionParam { VoucherCode = voucherCode, VoucherPin = voucherPin, Email = mail, Amount = amount, Description = order.OrderNumber };
                var json = request.JsonSerializer.Serialize(transactionParam);
                request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);
                request.AddHeader("Content-type", "application/json");
                var response = await client.ExecuteTaskAsync(request);
                if (response.IsSuccessful)
                {
                    var model = new OrderViewModel(_uow, order);
                    var orderActions = new OrderActions(_uow, order, mail, code);
                    orderActions.PostVoucherPayment(voucherPin);
                    OrderActions.GenerateLicences(_uow, code, order.Id);
                    BackgroundJob.Enqueue(() => OrderActions.GenerateLicences(_uow, code, order.Id));

                    return PartialView("_MessagePartial", model);
                }
                else
                {
                    var message = "Your may have entered an invalid or expired pin or the voucher has insufficient fund or you are not authorized to use it";
                    ModelState.AddModelError("", message);
                    var model = new PinRequestViewModel(orderId, voucherCode);
                    return PartialView("_PinRequestPartial", model);
                }
            }


        }

        
    }
}
