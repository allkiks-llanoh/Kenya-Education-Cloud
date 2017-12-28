using KEC.Voucher.Data.Models;
using System.Data.Entity;

namespace KEC.Voucher.Data.Repositories
{
    public class WalletRepository : Repository<DbWallet>
    {
        public WalletRepository(VoucherDb context) : base(context)
        {
        }
    }
}
