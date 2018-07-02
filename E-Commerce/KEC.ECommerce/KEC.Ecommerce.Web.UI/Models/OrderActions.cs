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
        private readonly string _userEmail;
        private readonly string _identificationCode;

        public OrderActions(IUnitOfWork uow, Order order, string userEmail,string identificationCode)
        {
            _uow = uow;
            _order = order;
            _userEmail = userEmail;
            _identificationCode = identificationCode;
        }
        public void PostVoucherPayment(string pinNumber)
        {
            ChangeOrderStatus();
            PostPaymentRecord(pinNumber,PaymentMethod.Voucher);
            DeductSoldQuantities();
        }

        private void ChangeOrderStatus()
        {
            var order = _uow.OrdersRepository.Get(_order.Id);
            order.Status = OrderStatus.Paid;
            _uow.Complete();
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
                        ExpiryDate = DateTime.Now.AddYears(1),
                        IdentificationCode = _identificationCode
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
                TransactedBy = _userEmail,
                OrderId = _order.Id,
                PaymentMethod = method
            };
            _uow.PaymentsRepository.Add(payment);
            _uow.Complete();
        }
    }
}
