using KEC.Voucher.Data.Models;
using KEC.Voucher.Services;
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
