using KEC.Voucher.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KEC.Voucher.Web.Api.Models
{
    public class Wallet
    {
        private readonly DbWallet _dbWallet;

        public Wallet(DbWallet dbWallet)
        {
            _dbWallet = dbWallet;
        }
        public int Id
        {
            get
            {
                return _dbWallet.Id;
            }
        }
        public decimal Balance
        {
            get
            {
                return _dbWallet.Balance;
            }
        }
        public decimal Amount
        {
            get
            {
                return _dbWallet.WalletAmount;
            }
        }
    }
}