using KEC.Voucher.Data.Models;
using System.Data.Entity;

namespace KEC.Voucher.Data.Repositories
{
    public class TransactionRepository : Repository<DbTransaction>
    {
        public TransactionRepository(DbContext context) : base(context)
        {
        }
    }
}
