﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEC.Voucher.Data.Models
{
    public class DbBatch
    {
        public DbBatch()
        {
            Vouchers = new List<DbVoucher>();
        }
        public int Id { get; set; }
        public int CountyId { get; set; }
        //Batch number will be Countycode plus some appended figures :-)
        public string BatchNumber { get; set; }
        //Serial number should be a GUID
        public string SerialNumber { get; set; }
        public int Year { get; set; }
        public int SchoolTypeId { get; set; }

        public virtual DbCounty County { get; set; }
        public virtual DbSchoolType SchoolType {get; set;}
        public virtual ICollection<DbVoucher> Vouchers { get; set; }

    }
}
