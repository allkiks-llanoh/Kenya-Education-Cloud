using KEC.ECommerce.Data.Database;
using KEC.ECommerce.Data.Models;
using KEC.ECommerce.Data.Repositories.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.ECommerce.Data.Repositories
{
    public class LineItemsRepository : Repository<LineItem>
    {
        private readonly ECommerceDataContext _eCommerceDataContext;

        public LineItemsRepository(ECommerceDataContext context) : base(context)
        {
            _eCommerceDataContext = context as ECommerceDataContext;
        }
        public async Task<IEnumerable<LineItem>> GetPublisherSales(string publisherGuid, DateTime startDate, DateTime endDate,PaymentMethod paymentMethod)
        {
            endDate = endDate.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);
            var lineItems = await _eCommerceDataContext.LineItems.Where(p => p.Order.SubmittedAt >= startDate
                                                                      && p.Order.SubmittedAt <= endDate
                                                                      && p.Publication.Publisher.Guid.Equals(publisherGuid)
                                                                      && p.Order.Payment!=null
                                                                      && p.Order.Payment.PaymentMethod==paymentMethod)
                                                                      ?.ToListAsync();
            return lineItems;

        }
        public async Task<IEnumerable<LineItem>> BestSellerPublicationsAsyc(int count = 6)
        {
            var publications = await (_context as ECommerceDataContext).LineItems
                                            
                                            .OrderByDescending(p => p.PublicationId)
                                            .Take(count).ToListAsync();
            return publications;
        }
    }
}
