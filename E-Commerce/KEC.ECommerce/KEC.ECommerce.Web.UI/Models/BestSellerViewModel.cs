using KEC.ECommerce.Data.Models;
using KEC.ECommerce.Data.UnitOfWork.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.ECommerce.Web.UI.Models
{
    public class BestSellerViewModel
    {
        private IUnitOfWork _uow;
        private ShoppingCartItem _shoppingCartItem;

        public BestSellerViewModel(IUnitOfWork uow, ShoppingCartItem shoppingCartItem)
        {
            _uow = uow;
            _shoppingCartItem = shoppingCartItem;
        }
        public int Id
        {
            get
            {
                return _shoppingCartItem.PublicationId;
            }
        }
        public string Title
        {
            get
            {
                var _content = _uow.PublicationsRepository.Find(p => p.Id.Equals(_shoppingCartItem.PublicationId)).FirstOrDefault();
                return _content.Title;
            }
        }
        public string Author
        {
            get
            {
                var _content = _uow.PublicationsRepository.Find(p => p.Id.Equals(_shoppingCartItem.PublicationId)).FirstOrDefault();
               
                var author = _content.Author ?? _uow.AuthorsRepository.Get(_content.AuthorId);
                return $"{author.FirstName} {author.LastName}";
            }
        }
        public string ThumbnailUrl
        {
            get
            {
                var _content = _uow.PublicationsRepository.Find(p => p.Id.Equals(_shoppingCartItem.PublicationId)).FirstOrDefault();
                return _content.ThumbnailUrl;
            }
        }
        public string UnitPrice
        {
            get
            {
                return _shoppingCartItem.UnitPrice.Equals(0) ? "Free content" : $"@KES {_shoppingCartItem.UnitPrice.ToString("N2")} ";
            }
        }
        public string Category
        {
            get
            {
                var _content = _uow.PublicationsRepository.Find(p => p.Id.Equals(_shoppingCartItem.PublicationId)).FirstOrDefault();
                var category = _content.Category ?? _uow.CategoriesRepository.Get(_content.Id);
                return category.Name;
            }
        }
        public string Description
        {
            get
            {
                var _content = _uow.PublicationsRepository.Find(p => p.Id.Equals(_shoppingCartItem.PublicationId)).FirstOrDefault();
                return _content.Description;
            }
        }
        public string Publisher
        {
            get
            {
                var _content = _uow.PublicationsRepository.Find(p => p.Id.Equals(_shoppingCartItem.PublicationId)).FirstOrDefault();
                var publisher = _content.Publisher ?? _uow.PublishersRepository.Get(_content.PublisherId);
                return publisher.Company;
            }
        }
        public string Subject
        {
            get
            {
                var _content = _uow.PublicationsRepository.Find(p => p.Id.Equals(_shoppingCartItem.PublicationId)).FirstOrDefault();
                var subject = _content.Subject ?? _uow.SubjectsRepository.Get(_content.SubjectId);
                return subject.Name;
            }
        }
        
    }
}
