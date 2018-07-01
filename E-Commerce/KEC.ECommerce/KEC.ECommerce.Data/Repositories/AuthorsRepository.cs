using KEC.ECommerce.Data.Database;
using KEC.ECommerce.Data.Models;
using KEC.ECommerce.Data.Repositories.Core;
using System.Linq;

namespace KEC.ECommerce.Data.Repositories
{
    public class AuthorsRepository : Repository<Author>
    {
        private readonly ECommerceDataContext _ecommerceContext;

        public AuthorsRepository(ECommerceDataContext context) : base(context)
        {
            _ecommerceContext = context as ECommerceDataContext;
        }
        public Author AddPublicationAuthor(string firstName, string lastName)
        {
            var retrievedAuthor = _ecommerceContext.Authors
                                                   .FirstOrDefault(p => p.LastName.Equals(lastName)
                                                    && p.FirstName.Equals(p.FirstName));
            if (retrievedAuthor == null)
            {
                var author = new Author
                {
                    FirstName = firstName,
                    LastName = lastName
                };
                _ecommerceContext.Authors.Add(author);
                _ecommerceContext.SaveChanges();
                return author;
            }
            else
            {
                return retrievedAuthor;
            }
        }
    }
}
