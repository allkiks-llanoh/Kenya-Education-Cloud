using KEC.ECommerce.Data.Database;
using KEC.ECommerce.Data.Models;
using KEC.ECommerce.Data.Repositories.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace KEC.ECommerce.Data.Repositories
{
    public class PurchasedBookRepository : Repository<PurchasedBook>
    {
        private readonly ECommerceDataContext _eCommerceContext;

        public PurchasedBookRepository(ECommerceDataContext context) : base(context)
        {
            _eCommerceContext = context as ECommerceDataContext;
        }
    }
}
