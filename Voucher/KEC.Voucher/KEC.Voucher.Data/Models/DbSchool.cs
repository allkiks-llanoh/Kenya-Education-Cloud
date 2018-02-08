﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEC.Voucher.Data.Models
{
    public class DbSchool
    {
        public DbSchool()
        {
            FundAllocations = new List<DbFundAllocation>();
            Vouchers = new List<DbVoucher>();
        }
        public int Id { get; set; }
        public string SchoolName { get; set; }
        public string SchoolCode { get; set; }
        public int SchoolTypeId { get; set; }
        public int CountyId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateChanged { get; set; }
        public virtual DbSchoolType SchoolType { get; set; }
        public virtual DbSchoolAdmin SchoolAdmin { get; set; }
        public virtual DbCounty County { get; set; }
        public virtual ICollection<DbVoucher> Vouchers { get; set; }
        public virtual ICollection<DbFundAllocation> FundAllocations { get; set; }
        public decimal AllocatedAmount
        {
            get
            {
                var allocation = FundAllocations.Where(p => p.Year.Equals(DateTime.Now.Year)).FirstOrDefault();
                return allocation == null ? 0 : allocation.Amount;
            }
        }
    }
}
