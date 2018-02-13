using KEC.Voucher.Data.Models;
using System.Data.Entity;

namespace KEC.Voucher.Data.Repositories
{
    public class FundAllocationRespository : Repository<DbFundAllocation>
    {
        public FundAllocationRespository(DbContext context)
            : base(context)
        {
        }
    }
}
