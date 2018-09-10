using KEC.ECommerce.Data.Models;
using KEC.ECommerce.Data.UnitOfWork.Core;

namespace KEC.ECommerce.Web.UI.Models
{
    public class SaleItemViewModel
    {
        private readonly LineItem _lineItem;
        private readonly IUnitOfWork _uow;

        public SaleItemViewModel(LineItem lineItem, IUnitOfWork uow)
        {
            _lineItem = lineItem;
            _uow = uow;
        }
        public string OrderNumber
        {
            get
            {
                return _uow.OrdersRepository.Get(_lineItem.OrderId)?.OrderNumber;
            }
        }
        public string TransactionId
        {
            get
            {
                return _uow.PaymentsRepository.GetTransactionNumber(_lineItem.OrderId);
            }
        }
        public string VoucherNumber
        {
            get
            {
                return _uow.PaymentsRepository.GetVoucherNumber(_lineItem.OrderId);
            }
        }
        public int Quantity
        {
            get
            {
                return _lineItem.Quantity;
            }
        }
        public string ContentNumber
        {
            get
            {
                return _uow.PublicationsRepository.Get(_lineItem.PublicationId)?.ContentNumber;
            }
        }
        public string ThumbNailImage
        {
            get
            {
                return _uow.PublicationsRepository.Get(_lineItem.PublicationId)?.ThumbnailUrl;
            }
        }
        public string PublicationUrl
        {
            get
            {
                return _uow.PublicationsRepository.Get(_lineItem.PublicationId)?.ContentUrl;
            }
        }
        public string Description
        {
            get
            {
                var product = _uow.PublicationsRepository.GetPublicationDetails(_lineItem.PublicationId);
                return $"{product.Level.Name} - {product?.Title} by {product?.Author?.FirstName} {product?.Author?.LastName}({product?.Publisher?.Company})";
            }
        }
        public string TotalSales
        {
            get
            {
                return (_lineItem.UnitPrice * _lineItem.Quantity).ToString("N2");
            }
        }
        public string PurchaseDate
        {
            get
            {
                return _uow.OrdersRepository.Get(_lineItem.OrderId)?.SubmittedAt.ToShortDateString();
            }
        }
    }
}
