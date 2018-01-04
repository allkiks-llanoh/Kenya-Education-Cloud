using KEC.Voucher.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KEC.Voucher.Web.Api.Models
{
    public class FundAllocation
    {
        private readonly DbFundAllocation _dbFundAllocation;

        public FundAllocation(DbFundAllocation dbFundAllocation)
        {
            _dbFundAllocation = dbFundAllocation;
        }
        public int id
        {
            get
            {
                return _dbFundAllocation.id;
            }
        }
        public decimal Amount
        {
            get
            {
                return _dbFundAllocation.Amount;
            }
            set
            {
                _dbFundAllocation.Amount = value;
            }
        }
        public int SchoolId
        {
            get
            {
                return _dbFundAllocation.SchoolId;
            }
            set
            {
                _dbFundAllocation.SchoolId = value;
            }
        }
        public int Year
        {
            get
            {
                return _dbFundAllocation.Year;
            }
            set
            {
                _dbFundAllocation.Year = value;
            }
        }
      
    }
}