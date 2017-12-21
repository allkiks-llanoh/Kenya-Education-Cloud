using KEC.Voucher.Data.Models;
using System.Data.Entity;

namespace KEC.Voucher.Data.Repositories
{
    public class VoucherRepository : Repository<DbVoucher>
    {
        public VoucherRepository(VoucherDb context) : base(context)
        {
        }
    }
}
