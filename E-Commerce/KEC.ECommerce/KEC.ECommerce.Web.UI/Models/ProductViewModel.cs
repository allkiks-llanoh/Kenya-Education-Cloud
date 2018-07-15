using KEC.ECommerce.Data.Models;
using KEC.ECommerce.Data.UnitOfWork.Core;

namespace KEC.ECommerce.Web.UI.Models
{
    public class ProductViewModel
    {
        private IUnitOfWork _uow;
        private Publication _publication;

        public ProductViewModel(IUnitOfWork uow, Publication publication)
        {
            _uow = uow;
            _publication = publication;
        }
        public int Id
        {
            get
            {
                return _publication.Id;
            }
        }
        public string Title
        {
            get
            {
                return _publication.Title;
            }
        }
        public string Author
        {
            get
            {
                var author = _publication.Author ?? _uow.AuthorsRepository.Get(_publication.AuthorId);
                return $"{author.FirstName} {author.LastName}";
            }
        }
        public string ThumbnailUrl
        {
            get
            {
                return _publication.ThumbnailUrl;
            }
        }
        public string UnitPrice
        {
            get
            {
                return _publication.UnitPrice.Equals(0) ? "Free content" : $"@KES {_publication.UnitPrice.ToString("N2")} ";
            }
        }
        public string Category
        {
            get
            {
                var category = _publication.Category ?? _uow.CategoriesRepository.Get(_publication.Id);
                return category.Name;
            }
        }
        public string Description
        {
            get
            {
                return _publication.Description;
            }
        }
        public string Publisher
        {
            get
            {
                var publisher = _publication.Publisher ?? _uow.PublishersRepository.Get(_publication.PublisherId);
                return publisher.Company;
            }
        }
        public string Subject
        {
            get
            {
                var subject = _publication.Subject ?? _uow.SubjectsRepository.Get(_publication.SubjectId);
                return subject.Name;
            }
        }
        public int Instock
        {
            get
            {
                return _publication.Quantity;
            }
        }
    }
}
