using KEC.Voucher.Data.Models;
using System.Data.Entity;

namespace KEC.Voucher.Data.Repositories
{
    public class WalletRepository : Repository<DbWallet>
    {
        public WalletRepository(DbContext context) : base(context)
        {
        }
    }
}
