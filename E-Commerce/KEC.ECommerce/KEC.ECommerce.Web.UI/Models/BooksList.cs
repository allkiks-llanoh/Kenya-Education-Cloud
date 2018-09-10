using KEC.ECommerce.Data.Models;
using KEC.ECommerce.Data.UnitOfWork.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.ECommerce.Web.UI.Models
{
    public class BooksList
    {
        private readonly PurchasedBook _booksList;
        private readonly IUnitOfWork _uow;

        public BooksList(PurchasedBook booksList, IUnitOfWork uow)
        {
            _booksList = booksList;
            _uow = uow;
        }

       
        public string ThumbNailImage
        {
            get
            {
                var publication = _uow.PublicationsRepository.Find(p => p.Id.Equals(_booksList.PublicationId)).FirstOrDefault();
                if (publication == null)
                {
                    return string.Empty;
                }
                else
                {
                    return publication.ThumbnailUrl;
                }

            }
        }
        public string PublicationUrl
        {
            get
            {
                var publication = _uow.PublicationsRepository.Find(p => p.Id.Equals(_booksList.PublicationId)).FirstOrDefault();
                if (publication == null)
                {
                    return string.Empty;
                }
                else
                {
                    return publication.ContentUrl;
                }

            }
        }
        public string Description
        {
            get
            {
                var publication = _uow.PublicationsRepository.Find(p => p.Id.Equals(_booksList.PublicationId)).FirstOrDefault();
                if (publication == null)
                {
                    return string.Empty;
                }
                else
                {
                    var product = _uow.PublicationsRepository.GetPublicationDetails(publication.Id);
                    return $"{product.Level.Name} - {product?.Title} by {product?.Author?.FirstName} {product?.Author?.LastName}({product?.Publisher?.Company})";
                }

            }
        }
        public int Id
        {
            get
            {
                var publication = _uow.PublicationsRepository.Find(p => p.Id.Equals(_booksList.PublicationId)).FirstOrDefault();
                if (publication == null)
                {
                    return int.Parse("Empty");
                }
                else
                {
                    var product = _uow.PublicationsRepository.GetPublicationDetails(publication.Id);
                    return publication.Id;
                }

            }
        }
    }
}
