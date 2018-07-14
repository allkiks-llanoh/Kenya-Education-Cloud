using KEC.ECommerce.Data.Models;
using KEC.ECommerce.Data.UnitOfWork.Core;

namespace KEC.ECommerce.Web.UI.Models
{
    public class LineItemViewModel
    {
        private readonly IUnitOfWork _uow;
        private readonly LineItem _lineItem;

        public LineItemViewModel(IUnitOfWork uow, LineItem lineItem)
        {
            _uow = uow;
            _lineItem = lineItem;
        }
        public int Id
        {
            get
            {
                return _lineItem.Id;
            }
        }
        public int ProductId
        {
            get
            {
                return _lineItem.PublicationId;
            }
        }
        public int OrderId
        {
            get
            {
                return _lineItem.OrderId;
            }
        }
        public int Quantity
        {
            get
            {
                return _lineItem.Quantity;
            }
        }
        public string UnitPrice
        {
            get
            {
                return _lineItem.UnitPrice.ToString("N2");
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
        public string TotalCost
        {
            get
            {
                return string.Format("{0:#.00}", _lineItem.UnitPrice * _lineItem.Quantity);
            }
        }
    }
}

