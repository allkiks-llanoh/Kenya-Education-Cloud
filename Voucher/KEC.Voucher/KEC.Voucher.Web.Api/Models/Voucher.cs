using KEC.Voucher.Data.Models;
using KEC.Voucher.Services.Extensions;

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
        public string SchoolCode
        {
            get
            {
                return _dbVoucher.School.SchoolCode;
            }
        }
        public string SchoolName
        {
            get
            {
                return _dbVoucher.School.SchoolName;
            }
        }
        public string Status
        {
            get
            {
                return _dbVoucher.Status.StatusValue.GetDescription();
            }
        }
        public Wallet Wallet
        {
            get
            {
                return _dbVoucher.Wallet == null ? null : new Wallet(_dbVoucher.Wallet);
            }
        }
        public int BatchId
        {
            get
            {
                return _dbVoucher.BatchId;
            }
        }
        public decimal Amount
        {
            get
            {
                return _dbVoucher.Wallet.WalletAmount;
            }
        }

    }
}