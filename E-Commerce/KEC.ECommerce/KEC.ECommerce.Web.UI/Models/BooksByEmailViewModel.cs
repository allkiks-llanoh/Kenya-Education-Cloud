using KEC.ECommerce.Data.Models;
using KEC.ECommerce.Data.UnitOfWork.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.ECommerce.Web.UI.Models
{
    public class BooksByEmailViewModel
    {
        private readonly Order _order;
        private readonly IUnitOfWork _uow;

        public BooksByEmailViewModel(Order order, IUnitOfWork uow)
        {
            _order = order;
            _uow = uow;
        }

        public string ContentNumber
        {
            get
            {
                var publication = _uow.LineItemsRepository.Find(p => p.OrderId.Equals(_order.Id)).FirstOrDefault();
                if (publication == null)
                {
                    return string.Empty;
                }
                else
                {
                    return _uow.PublicationsRepository.Get(publication.PublicationId)?.ContentNumber;
                }
              
            }
        }
        public string ThumbNailImage
        {
            get
            {
                var publication = _uow.LineItemsRepository.Find(p => p.OrderId.Equals(_order.Id)).FirstOrDefault();
                if (publication == null)
                {
                    return string.Empty;
                }
                else
                {
                    return _uow.PublicationsRepository.Get(publication.PublicationId)?.ThumbnailUrl;
                } 
               
            }
        }
        public string PublicationUrl
        {
            get
            {
                var publication = _uow.LineItemsRepository.Find(p => p.OrderId.Equals(_order.Id)).FirstOrDefault();
                if (publication == null)
                {
                    return string.Empty;
                }
                else
                {
                    return _uow.PublicationsRepository.Get(publication.PublicationId)?.ContentUrl;
                }
                
            }
        }
        public string Description
        {
            get
            {
                var publication = _uow.LineItemsRepository.Find(p => p.OrderId.Equals(_order.Id)).FirstOrDefault();
                if (publication == null)
                {
                    return string.Empty;
                }
                else
                {
                    var product = _uow.PublicationsRepository.GetPublicationDetails(publication.PublicationId);
                    return $"{product.Level.Name} - {product?.Title} by {product?.Author?.FirstName} {product?.Author?.LastName}({product?.Publisher?.Company})";
                }
                
            }
        }
    }
}
