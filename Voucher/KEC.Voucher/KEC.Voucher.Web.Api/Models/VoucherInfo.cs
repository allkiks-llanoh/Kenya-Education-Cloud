using KEC.Voucher.Data.Models;
using KEC.Voucher.Services.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KEC.Voucher.Web.Api.Models
{
    public class VoucherInfo
    {
        private readonly DbVoucher _dbVoucher;
        public VoucherInfo(DbVoucher dbVoucher)
        {
            _dbVoucher = dbVoucher;
        }
        public string County
        {
            get
            {
                return _dbVoucher.School.County.CountyName;
            }
        }
        public string SchoolType
        {
            get
            {
                return _dbVoucher.School.SchoolType.SchoolType;
            }
        }
        public decimal Balance
        {
            get
            {
                return _dbVoucher.Wallet.Balance;
            }
        }
        public string Status
        {
            get
            {
                return _dbVoucher.Status.StatusValue.GetDescription();
            }
        }
        public DateTime BalanceDate
        {
            get
            {
                return _dbVoucher.Wallet.UpdatedOnUtc;
            }
        }
    }
}