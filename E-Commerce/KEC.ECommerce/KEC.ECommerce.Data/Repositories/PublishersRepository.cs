using KEC.ECommerce.Data.Database;
using KEC.ECommerce.Data.Models;
using KEC.ECommerce.Data.Repositories.Core;
using System;
using System.Linq;

namespace KEC.ECommerce.Data.Repositories
{
    public class PublishersRepository : Repository<Publisher>
    {
        private readonly ECommerceDataContext _ecommerceDataContext;

        public PublishersRepository(ECommerceDataContext context) : base(context)
        {
            _ecommerceDataContext = _context as ECommerceDataContext;
        }
        public Publisher AddPublicationPublisher(string name, string guid)
        {
            var retrievedPublisher = _ecommerceDataContext.Publishers.FirstOrDefault(p => p.Guid.Equals(guid));
            if (retrievedPublisher == null)
            {
                var publisher = new Publisher
                {
                    Company = name,
                    Guid = guid,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                };
                _ecommerceDataContext.Publishers.Add(publisher);
                _ecommerceDataContext.SaveChanges();
                return publisher;
            }
            else
            {
                return retrievedPublisher;
            }

        }
    }
}
