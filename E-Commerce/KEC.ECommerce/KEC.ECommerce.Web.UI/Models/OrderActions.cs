using KEC.ECommerce.Data.Models;
using KEC.ECommerce.Data.UnitOfWork.Core;
using System;

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
        public void PostVoucherPayment(string voucherCode, string pinNumber,PaymentMethod paymentMethod)
        {
            ChangeOrderStatus();
            PostPaymentRecord(pinNumber, voucherCode, paymentMethod);
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

        private void PostPaymentRecord(string transactionId,string voucherCode, PaymentMethod method)
        {
            var payment = new Payment
            {
                TransactionNumber = transactionId,
                TransactionDate = DateTime.Now,
                TransactedBy = _userEmail,
                OrderId = _order.Id,
                PaymentMethod = method,
                VoucherNumber = voucherCode
            };
            _uow.PaymentsRepository.Add(payment);
            _uow.Complete();
        }
    }
}
