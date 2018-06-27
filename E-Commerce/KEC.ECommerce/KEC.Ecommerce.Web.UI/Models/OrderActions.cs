using KEC.ECommerce.Data.Models;
using KEC.ECommerce.Data.UnitOfWork.Core;
using Microsoft.AspNetCore.Http;
using System;

namespace KEC.ECommerce.Web.UI.Models
{
    public class OrderActions
    {
        private readonly IUnitOfWork _uow;
        private readonly Order _order;
        private readonly HttpContext _context;

        public OrderActions(IUnitOfWork uow, Order order, HttpContext context)
        {
            _uow = uow;
            _order = order;
            _context = context;
        }
        public void PostVoucherPayment(string pinNumber)
        {
            //TODO: Post transaction to the api
            PostPaymentRecord(pinNumber,PaymentMethod.Voucher);
            DeductSoldQuantities();
        }

        public void GenerateLicences()
        {
            var lineItems = _uow.OrdersRepository.GetLineItems(_order.Id);
            lineItems.ForEach(lineItem =>
            {
                for (int count = 1; count <= lineItem.Quantity; count++)
                {
                    var licence = new Licence
                    {
                        Code = _uow.LicencesRepository.GetNextLicence(),
                        PublicationId = lineItem.PublicationId,
                        OrderId = _order.Id,
                        ExpiryDate = DateTime.Now.AddYears(1)
                    };
                    _uow.LicencesRepository.Add(licence);
                }
                _uow.Complete();
            });
            _order.Status = OrderStatus.Processed;
            _uow.Complete();
            //TODO: Send email to the buyer;
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
                TransactedBy = _context.User.Identity.Name,
                OrderId = _order.Id,
                PaymentMethod = method
            };
            _uow.PaymentsRepository.Add(payment);
            _uow.Complete();
        }
    }
}
