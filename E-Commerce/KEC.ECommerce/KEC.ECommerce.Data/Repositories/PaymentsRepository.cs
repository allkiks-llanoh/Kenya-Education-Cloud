using KEC.ECommerce.Data.Database;
using KEC.ECommerce.Data.Models;
using KEC.ECommerce.Data.Repositories.Core;
using System.Linq;

namespace KEC.ECommerce.Data.Repositories
{
    public class PaymentsRepository : Repository<Payment>
    {
        private ECommerceDataContext _ecommerceContext;
        public PaymentsRepository(ECommerceDataContext context) : base(context)
        {
            _ecommerceContext = context;
        }
        public string GetVoucherNumber(int orderId)
        {
            return _ecommerceContext.Payments.Where(p => p.OrderId.Equals(orderId))
                                     ?.Select(p => p.VoucherNumber).FirstOrDefault();
        }
    }
}
