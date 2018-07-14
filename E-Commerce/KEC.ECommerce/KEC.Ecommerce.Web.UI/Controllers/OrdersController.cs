using DinkToPdf.Contracts;
using DNTBreadCrumb.Core;
using Hangfire;
using KEC.ECommerce.Data.Models;
using KEC.ECommerce.Data.UnitOfWork.Core;
using KEC.ECommerce.Web.UI.Helpers;
using KEC.ECommerce.Web.UI.Mailer;
using KEC.ECommerce.Web.UI.Models;
using KEC.ECommerce.Web.UI.PDF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace KEC.ECommerce.Web.UI.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IConfiguration _configuration;
        private readonly ITemplateService _templateService;
        private readonly IEmailService _emailService;
        private readonly IHostingEnvironment _env;
        private readonly IEmailConfiguration _emailConfiguration;

        public OrdersController(IUnitOfWork uow, IConfiguration configuration, ITemplateService templateService,
            IEmailService emailService, IHostingEnvironment env, IEmailConfiguration emailConfiguration)
        {
            _uow = uow;
            _configuration = configuration;
            _templateService = templateService;
            _emailService = emailService;
            _env = env;
            _emailConfiguration = emailConfiguration;
        }

        [HttpGet]
        [BreadCrumb(Title = "Payment", Order = 1)]
        public async Task<IActionResult> Payment(int orderId)
        {
            var mail = User.FindFirst("Email")?.Value;
            var order = await _uow.OrdersRepository.GetOrderByUser(orderId, mail, OrderStatus.Submitted);
            var model = default(OrderViewModel);
            if (order != null)
            {
                model = new OrderViewModel(_uow, order, true);
                var orderCost = _uow.OrdersRepository.GetOrderTotalCost(orderId);
                if (orderCost > 0)
                {
                    return View(model);
                }
                else
                {
                    var code = User.FindFirst("IdentificationCode")?.Value;
                    var customerName = User.FindFirst("DisplayName")?.Value;
                    var msgModel = ProcessPayment(order.OrderNumber, mail, code, customerName, order);
                    return View("FreeContent", msgModel);
                }
            }
            else
            {
                return NotFound();
            }


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

            var mail = User.FindFirst("Email")?.Value;
            var code = User.FindFirst("IdentificationCode")?.Value;
            var customerName = User.FindFirst("DisplayName")?.Value;
            var order = await _uow.OrdersRepository.GetOrderByUser(orderId, mail, OrderStatus.Submitted);
            if (order == null)
            {
                var model = new PinRequestViewModel(orderId, voucherCode);
                ModelState.AddModelError(string.Empty, "Order record could not be retrieve");
                return PartialView("_PinRequestPartial", model);
            }
            else
            {
                IRestResponse response = await ConfirmVoucherPin(voucherCode, voucherPin, mail, order);
                if (response.IsSuccessful)
                {
                    OrderViewModel model = ProcessPayment(voucherPin, mail, code, customerName, order);
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

        private async Task<IRestResponse> ConfirmVoucherPin(string voucherCode, string voucherPin, string mail, Order order)
        {
            var transEndPoint = _configuration["VoucherTransactionEndPoint"];
            var client = new RestClient(transEndPoint);
            var request = new RestRequest(Method.POST);
            var amount = _uow.OrdersRepository.GetOrderTotalCost(order.Id);
            var transactionParam = new TransactionParam
            {
                VoucherCode = voucherCode,
                VoucherPin = voucherPin,
                Email = mail,
                Amount = amount,
                Description = order.OrderNumber
            };
            var json = request.JsonSerializer.Serialize(transactionParam);
            request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);
            request.AddHeader("Content-type", "application/json");
            var response = await client.ExecuteTaskAsync(request);
            return response;
        }

        private OrderViewModel ProcessPayment(string voucherPin, string mail, string code, string customerName, Order order)
        {
            var model = new OrderViewModel(_uow, order);
            var orderActions = new OrderActions(_uow, order, mail, code);
            orderActions.PostVoucherPayment(voucherPin);
            OrderActions.GenerateLicences(_uow, code, order.Id);
            var licences = _uow.LicencesRepository.Find(p => p.IdentificationCode.Equals(code) && p.OrderId.Equals(order.Id))
                                                  ?.Select(p => new LicenceViewModel(_uow, p))?.ToList();
            var viewModel = new LicencesEmailViewModel
            {
                OrderNumber = order.OrderNumber,
                CustomerName = customerName,
                CustomerEmail = mail,
                GenDate = DateTime.Now.ToLongDateString(),
                Licences = licences,
                StoreEmail = _emailConfiguration.SmtpUsername
            };

            var documentContent = _templateService.RenderTemplateAsync("Templates/LicencesEmail", viewModel).Result;
            var emailService = _emailService as EmailService;
            var emailConfiguration = _emailConfiguration as EmailConfiguration;
            var pdfParams = new PDFParams
            {
                PathToMailTemplate = Path.Combine(_env.ContentRootPath, "Mailer", "Templates", "Licences.html"),
                PdfOutputFile = Path.Combine(_env.ContentRootPath, "PDFLicences", $"{order.OrderNumber}.pdf")
            };
            var jobId = BackgroundJob.Schedule(() => MailerActions.ConvertHtml2PDF(documentContent, pdfParams), DateTimeOffset.Now.AddMinutes(10));
            var customer = new Customer
            {
                IdentificationCode = code,
                Name = customerName,
                Order = order,
                PDFParams = pdfParams
            };
            GenerateMailMessage(emailConfiguration, customer, out string messageBody, out EmailMessage message);
            BackgroundJob.ContinueWith(jobId, () => MailerActions.SendLicencesEmail(emailService, emailConfiguration, message));
            return model;
        }

        private static void GenerateMailMessage(EmailConfiguration emailConfiguration, Customer customer, out string messageBody, out EmailMessage message)
        {
            using (var SourceReader = System.IO.File.OpenText(customer.PDFParams.PathToMailTemplate))
            {

                var templateStr = SourceReader.ReadToEnd();
                messageBody = templateStr.Replace("@Name", customer.Name).Replace("@Order", customer.Order.OrderNumber);
            }

            message = new EmailMessage
            {
                Content = messageBody
            };
            message.ToAddresses.Add(new EmailAddress
            {
                Name = customer.Name,
                Address = customer.Order.CustomerEmail
            });
            message.Subject = "Purchased Licences";
            message.FromAddresses.Add(new EmailAddress
            {
                Name = emailConfiguration.SmtpUsername,
                Address = emailConfiguration.SmtpUsername
            });
        }

    }
}
