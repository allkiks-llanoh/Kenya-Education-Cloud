using KEC.Voucher.Data.Models;
using KEC.Voucher.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KEC.Voucher.Data.Repositories
{
    public class VoucherRepository : Repository<DbVoucher>
    {
        public VoucherRepository(VoucherDb context) : base(context)
        {
        }
        public VoucherDb VoucherDbContext
        {
            get
            {
                return _context as VoucherDb;
            }
        }
        public string GetVoucherCode(string batchNumber, List<DbVoucher> voucherList)
        {
            var voucherCode = string.Empty;
            do
            {
                voucherCode = RandomCodeGenerator.GetVoucherCode(batchNumber);
            } while ((Find(p => p.VoucherCode.Equals(voucherCode)).FirstOrDefault() != null) &&
            (voucherList.Where(p => p.VoucherCode.Equals(voucherCode)).FirstOrDefault() != null));

            return voucherCode;
        }
      
    }
}
