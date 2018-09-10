using KEC.ECommerce.Data.Database;
using KEC.ECommerce.Data.Helpers;
using KEC.ECommerce.Data.Models;
using KEC.ECommerce.Data.Repositories.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.ECommerce.Data.Repositories
{
    public class OrdersRepository : Repository<Order>
    {
        private readonly ECommerceDataContext _eCommerceContext;

        public OrdersRepository(ECommerceDataContext context) : base(context)
        {
            _eCommerceContext = context as ECommerceDataContext;
        }
        public string GetNextOrderNumber(string prefix = "KECORD#")
        {
            var orderNumber = string.Empty;
            do
            {
                orderNumber = RandomCodeGenerator.GetOrderNumber(prefix);
            } while ((Find(p => p.OrderNumber.Equals(orderNumber)).FirstOrDefault() != null));
            return orderNumber;
        }

        public List<LineItem> GetLineItems(int orderId)
        {

            var lineItems = _eCommerceContext.LineItems.Where(p => p.OrderId.Equals(orderId))
                                    .Include(p => p.Publication).ToList();
            return lineItems;
        }

        public long GetShoppingLineItemsCount(int orderId)
        {

            var count = _eCommerceContext.LineItems
                                                .Where(p => p.OrderId.Equals(orderId))
                                                .Sum(p => p.Quantity);
            return count;
        }

        public decimal GetOrderTotalCost(int orderId)
        {
            var totalCost = _eCommerceContext.LineItems
                                             .Where(p => p.OrderId.Equals(orderId))
                                             .Sum(p => p.Quantity * p.UnitPrice);

            return totalCost;
        }
        public async Task<Order> GetOrderByUser(int orderId, string userMail, OrderStatus status)
        {
            var order = await _eCommerceContext.Orders.FirstOrDefaultAsync(p => p.CustomerEmail.Equals(userMail)
                                 && p.Id.Equals(orderId) && p.Status == status);
            return order;
        }
        public IQueryable<Order> GroupBy(string email)
        {
            var orders = default(IQueryable<Order>);
               orders =  _eCommerceContext.Orders.Where(p => p.CustomerEmail.Equals(email)
                                 && p.Status == OrderStatus.Processed);
            return orders;
        }

        public IQueryable<Order> QueryablePaidOrders(string email, string searchTerm)
        {
            var orders = default(IQueryable<Order>);
            var currentDate = DateTime.Now;
            if (searchTerm == null)
            {
                orders = _eCommerceContext.Orders.Where(p => p.CustomerEmail.Equals(email) && p.Status >= OrderStatus.Paid)
                                                      .OrderByDescending(p => p.SubmittedAt);
            }
            else
            {
                orders = _eCommerceContext.Orders.Where(p => p.CustomerEmail.Equals(email)
                                                                   && p.OrderNumber.Contains(searchTerm)
                                                                   && (p.Status >= OrderStatus.Paid))
                                                                   .OrderByDescending(p => p.SubmittedAt);
            }

            return orders;
        }
    }
}
