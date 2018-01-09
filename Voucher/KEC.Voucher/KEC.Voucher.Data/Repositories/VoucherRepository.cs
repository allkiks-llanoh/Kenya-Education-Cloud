using KEC.Voucher.Data.Models;
using KEC.Voucher.Services;
using System.Data.Entity;
using System.Linq;

namespace KEC.Voucher.Data.Repositories
{
    public class VoucherRepository : Repository<DbVoucher>
    {
        public VoucherRepository(VoucherDb context) : base(context)
        {
        }
        public string GetVoucherCode(string batchNumber)
        {
            var voucherCode = string.Empty;
            do
            {
                voucherCode = RandomCodeGenerator.GetVoucherCode(batchNumber);
            } while (Find(p => p.VoucherCode.Equals(voucherCode)).FirstOrDefault() != null);

            return voucherCode;
        }
    }
}
