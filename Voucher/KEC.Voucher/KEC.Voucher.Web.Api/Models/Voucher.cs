using KEC.Voucher.Data.Models;
using System.Collections.Generic;

namespace KEC.Voucher.Web.Api.Models
{
    public class Voucher
    {
        private readonly DbVoucher _dbVoucher;

        public Voucher(DbVoucher dbVoucher)
        {
            _dbVoucher = dbVoucher;
        }
        public int Id
        {
            get
            {
                return _dbVoucher.Id;
            }
        }
        public string VoucherCode
        {
            get
            {
                return _dbVoucher.VoucherCode;
            }
        }
        public string VoucherSerial
        {
            get
            {
                return _dbVoucher.VoucherSerial;
            }
        }
        public int VoucherYear
        {
            get
            {
                return _dbVoucher.VoucherYear;
            }
        }
        public int SchoolId
        {
            get
            {
                return _dbVoucher.SchoolId;
            }
        }
       
        public int WalletId
        {
            get
            {
                return _dbVoucher.WalletId;
            }
        }
        public int BatchId
        {
            get
            {
                return _dbVoucher.BatchId;
            }
        }

    }
}