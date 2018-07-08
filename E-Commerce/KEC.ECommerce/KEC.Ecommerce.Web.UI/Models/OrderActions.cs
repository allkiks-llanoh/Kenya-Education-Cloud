using DinkToPdf;
using DinkToPdf.Contracts;
using KEC.ECommerce.Data.Models;
using KEC.ECommerce.Data.UnitOfWork.Core;
using KEC.ECommerce.Web.UI.Mailer;
using KEC.ECommerce.Web.UI.PDF;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.ECommerce.Web.UI.Models
{
    public class OrderActions
    {
        private readonly IUnitOfWork _uow;
        private readonly Order _order;
        private readonly string _userEmail;
        private readonly string _identificationCode;

        public OrderActions(IUnitOfWork uow, Order order, string userEmail, string identificationCode)
        {
            _uow = uow;
            _order = order;
            _userEmail = userEmail;
            _identificationCode = identificationCode;
        }
        public void PostVoucherPayment(string pinNumber)
        {
            ChangeOrderStatus();
            PostPaymentRecord(pinNumber, PaymentMethod.Voucher);
            DeductSoldQuantities();
        }

        private void ChangeOrderStatus()
        {
            var order = _uow.OrdersRepository.Get(_order.Id);
            order.Status = OrderStatus.Paid;
            _uow.Complete();
        }

        public static void GenerateLicences(IUnitOfWork uow, string identificationCode, int orderId)
        {
            var lineItems = uow.OrdersRepository.GetLineItems(orderId);
            var order = uow.OrdersRepository.Get(orderId);
            if (order != null)
            {
                lineItems.ForEach(lineItem =>
                {
                    for (int count = 1; count <= lineItem.Quantity; count++)
                    {
                        var licence = new Licence
                        {
                            Code = uow.LicencesRepository.GetNextLicence(),
                            PublicationId = lineItem.PublicationId,
                            OrderId = order.Id,
                            ExpiryDate = DateTime.Now.AddYears(1),
                            IdentificationCode = identificationCode
                        };
                        uow.LicencesRepository.Add(licence);
                    }
                    uow.Complete();
                });
                order.Status = OrderStatus.Processed;
                uow.Complete();
            }
        }
        public static async Task SendLicencesEmail(IUnitOfWork uow, IEmailService emailService, ITemplateService templateService,
                                      string identificationCode, string customerName, int orderId, IConverter converter, IHostingEnvironment env)
        {
           
            var order = uow.OrdersRepository.Get(orderId);
            var licences = uow.LicencesRepository.Find(p => p.IdentificationCode.Equals(identificationCode)
                                                  && p.OrderId.Equals(orderId))
                                                  ?.Select(p => new LicenceViewModel(uow, p)).ToList();
            var pathToTemplate = Path.Combine(env.ContentRootPath, "Mailer", "Templates", "Licences.html");
            var pdfOutputFile = Path.Combine(env.ContentRootPath,"PDFLicences",$"{order.OrderNumber}.pdf");
            var messageBody = default(string);
            using (var SourceReader = File.OpenText(pathToTemplate))
            {

                var templateStr = SourceReader.ReadToEnd();
                messageBody = string.Format(templateStr, customerName, order.OrderNumber);

            }
            var viewModel = new LicencesEmailViewModel
            {
                OrderNumber = order.OrderNumber,
                CustomerName = customerName,
                GenDate = DateTime.Now.ToShortDateString(),
                Licences = licences
            };
            var documentContent = await templateService.RenderTemplateAsync("Templates/LicencesEmail", viewModel);
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = { ColorMode = ColorMode.Color,
                                   Orientation = Orientation.Portrait,
                                   PaperSize = PaperKind.A4,
                                   Margins = new MarginSettings() { Top = 10 },
                                   Out = pdfOutputFile},
                Objects = { new ObjectSettings() { HtmlContent = documentContent }, }
            };
            var message = new EmailMessage
            {
                Content = messageBody
            };
            message.ToAddresses.Add(new EmailAddress { Name = customerName, Address = order.CustomerEmail });
            message.Attachments.Add(doc.GlobalSettings.Out);
            //emailService.Send(message);

        }

        private void DeductSoldQuantities()
        {
            var lineItems = _uow.OrdersRepository.GetLineItems(_order.Id);
            lineItems.ForEach(lineItem =>
            {
                var publication = _uow.PublicationsRepository.Get(lineItem.PublicationId);
                publication.Quantity -= lineItem.Quantity;
            });
            _uow.Complete();
        }

        private void PostPaymentRecord(string transactionId, PaymentMethod method)
        {
            var payment = new Payment
            {
                TransactionNumber = transactionId,
                TransactionDate = DateTime.Now,
                TransactedBy = _userEmail,
                OrderId = _order.Id,
                PaymentMethod = method
            };
            _uow.PaymentsRepository.Add(payment);
            _uow.Complete();
        }
    }
}
