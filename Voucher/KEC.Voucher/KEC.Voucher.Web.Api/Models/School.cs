using KEC.Voucher.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KEC.Voucher.Web.Api.Models
{
    public class School
    {
        private readonly DbSchool _dbschool;
        public School(DbSchool dbSchool)
        {
            _dbschool = dbSchool;
        }
        public int Id
        {
            get
            {
                return _dbschool.Id;
            }
        }
        public string SchoolName
        {
            get
            {
                return _dbschool.SchoolName;
            }
        }
        public string SchoolCode
        {
            get
            {
                return _dbschool.SchoolCode;
            }
        }
        //public int SchoolAdminId { get; set; }
        public int SchoolTypeId
        {
            get
            {
                return _dbschool.SchoolTypeId;
            }
        }
        public int CountyId
        {
            get
            {
                return _dbschool.CountyId;
            }
        }
        public DateTime DateCreated
        {
            get
            {
                return _dbschool.DateCreated;
            }

        }
        public DateTime DateChanged
        {
            get
            {
                return _dbschool.DateChanged;
            }

        }
        public string SchoolType
        {
            get
            {
                return _dbschool.SchoolType.SchoolType;
            }

        }

        public string CountyCode
        {
            get
            {
                return _dbschool.County.CountyCode;
            }

        }
        public string CountyName
        {
            get
            {
                return _dbschool.County.CountyName;
            }
        }
        public ICollection<FundAllocation> FundAllocations
        {
            get
            {
                return _dbschool.FundAllocations.Select(p => new FundAllocation(p)).ToList();
            }
        }
        public FundAllocation CurrentFundAllocation
        {
            get
            {
                var allocation = _dbschool.FundAllocations.FirstOrDefault(p => p.Year == DateTime.Now.Year);
                return allocation == null ? null : new FundAllocation(allocation);
            }
        }
        public ICollection<Voucher> Vouchers
        {
            get
            {
                return _dbschool.Vouchers.Select(p => new Voucher(p)).ToList();
            }
        }
        public Voucher CurrentVoucher
        {
            get
            {
                var dbVoucher = _dbschool.Vouchers
                    .Where(p => p.VoucherYear == DateTime.Now.Year)
                    .FirstOrDefault();
                return dbVoucher == null ? null : new Voucher(dbVoucher);

            }
        }
    }
}