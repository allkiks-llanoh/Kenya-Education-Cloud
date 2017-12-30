using KEC.Voucher.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEC.Voucher.Data.Repositories
{
    public class BatchRepository : Repository<DbBatch>
    {
        public BatchRepository(VoucherDb context) : base(context)
        {
        }
    }
}
