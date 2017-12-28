using KEC.Voucher.Data.Models;
using System.Data.Entity;

namespace KEC.Voucher.Data.Repositories
{
    public class StatusRepository : Repository<DbStatus>
    {
        public StatusRepository(VoucherDb context) : base(context)
        {
        }
    }
}
