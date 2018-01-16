using KEC.Voucher.Data.Models;
using KEC.Voucher.Services.Helpers;
using System.Linq;

namespace KEC.Voucher.Data.Repositories
{
    public class BatchRepository : Repository<DbBatch>
    {
        public BatchRepository(VoucherDb context) : base(context)
        {
        }
        public string GetBatchNumber(string countycode)
        {
            var batchNumber = string.Empty;
            do
            {
                batchNumber = RandomCodeGenerator.GetBatchNumber(countycode);
            } while (Find(p => p.BatchNumber.Equals(batchNumber)).FirstOrDefault() != null);

            return batchNumber;
        }
    }
}
