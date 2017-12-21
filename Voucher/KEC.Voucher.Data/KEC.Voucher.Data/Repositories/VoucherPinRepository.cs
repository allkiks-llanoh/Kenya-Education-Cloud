using System.Data.Entity;
using KEC.Voucher.Data.Models;

namespace KEC.Voucher.Data.Repositories
{
    public class VoucherPinRepository : Repository<DbVoucherPin>
    {
        public VoucherPinRepository(VoucherDb context) : base(context)
        {
        }
    }
}
