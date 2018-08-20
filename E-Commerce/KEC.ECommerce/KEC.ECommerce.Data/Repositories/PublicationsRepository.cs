using KEC.ECommerce.Data.Database;
using KEC.ECommerce.Data.Models;
using KEC.ECommerce.Data.Repositories.Core;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.ECommerce.Data.Repositories
{
    public class PublicationsRepository : Repository<Publication>
    {
        private ECommerceDataContext _ecommerceContext;

        public PublicationsRepository(ECommerceDataContext context) : base(context)
        {
            _ecommerceContext = context as ECommerceDataContext;
        }
        public async Task<IEnumerable<Publication>> TopPublicationsAsyc(int count = 6)
        {
            var publications = await (_context as ECommerceDataContext).Publications
                                            .Where(p => !p.ThumbnailUrl.Equals(null) 
                                            && p.Available.Equals(true) && p.Quantity>0)
                                            .OrderByDescending(p => p.CreatedAt)
                                            .Take(count).ToListAsync();
            return publications;
        }

        public IEnumerable<Publication> TopProductsPerCategory(int categoryId, int count = 4)
        {
            var publications = _ecommerceContext.Publications
                                            .Include(p => p.Category)
                                            .Include(p => p.Author)
                                            .Where(p => p.CategoryId.Equals(categoryId) && p.Available.Equals(true) && p.Quantity > 0)
                                            .Distinct()
                                            .OrderByDescending(p => p.CreatedAt)
                                            .Take(count).ToList();
            return publications;
        }
        public async Task<decimal> PublicationUnitPrice(int publicationId)
        {

            var publication = await _ecommerceContext.Publications.FindAsync(publicationId);
            return publication.UnitPrice;
        }
        public Publication GetPublicationDetails(int publicationId)
        {
            var publication = _ecommerceContext.Publications
                                               .Include(p => p.Author)
                                               .Include(p => p.Category)
                                               .Include(p => p.Subject)
                                               .Include(p => p.Level)
                                               .Include(p => p.Publisher)
                                               .FirstOrDefault(p => p.Id.Equals(publicationId));
            return publication;
        }
        public bool AddPublicationToStore(Publication publication)
        {
            var notExist = !_ecommerceContext.Publications.Any(p => p.ContentNumber.Equals(publication.ContentNumber));
            var added = default(bool);
            if (notExist)
            {
                _ecommerceContext.Publications.Add(publication);
                _ecommerceContext.SaveChanges();
                added = true;
            }
            return added;
        }
        public IQueryable<Publication> QueryablePublications(int categoryId, string searchTerm)
        {
            var publications = default(IQueryable<Publication>);
            if (searchTerm == null)
            {
                publications = _ecommerceContext.Publications.Where(p => p.CategoryId.Equals(categoryId)
                                                                         && p.Available.Equals(true) && p.Quantity>0);
            }
            else
            {
                publications = _ecommerceContext.Publications.Where(p => p.CategoryId.Equals(categoryId)
                                                                    && (p.Description.Contains(searchTerm)
                                                                    && p.Available.Equals(true)
                                                                    && p.Quantity>0
                                                                    || p.Level.Name.Contains(searchTerm))
                                                                    || p.Title.Contains(searchTerm) ||
                                                                    p.Subject.Name.Contains(searchTerm) ||
                                                                    p.Publisher.Company.Contains(searchTerm) ||
                                                                    p.Author.FirstName.Contains(searchTerm) ||
                                                                    p.Author.LastName.Contains(searchTerm));
            }

            return publications;
        }
    }
}
