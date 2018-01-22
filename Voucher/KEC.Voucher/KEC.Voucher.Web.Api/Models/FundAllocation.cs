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
           
        }
        public int SchoolId
        {
            get
            {
                return _dbFundAllocation.SchoolId;
            }
           
        }
        public string School
        {
            get
            {
                return _dbFundAllocation.School.SchoolName;
            }
           
        }
        public string SchoolCode
        {
            get
            {
                return _dbFundAllocation.School.SchoolCode;
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