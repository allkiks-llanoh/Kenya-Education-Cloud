using KEC.ECommerce.Data.Models;
using KEC.ECommerce.Data.UnitOfWork.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.ECommerce.Web.UI.Models
{
    public class BooksListViewModel
    {
        private readonly LineItem _lineItem;
        private readonly IUnitOfWork _uow;

        public BooksListViewModel(LineItem lineItem, IUnitOfWork uow)
        {
            _lineItem = lineItem;
            _uow = uow;
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

    }
}

