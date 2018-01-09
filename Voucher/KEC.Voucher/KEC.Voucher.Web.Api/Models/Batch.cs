using KEC.Voucher.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KEC.Voucher.Web.Api.Models
{
    public class Batch
    {
        private readonly DbBatch _dbbatch;
        public Batch(DbBatch dbBatch)
        {
            _dbbatch = dbBatch;
        }
        public int Id
        {
            get
            {
                return _dbbatch.Id;
            }
        }
        public int CountyId
        {
            get
            {
                return _dbbatch.CountyId;
            }
        }

        public string BatchNumber
        {
            get
            {
                return _dbbatch.BatchNumber;
            }
        }

        public string SerialNumber
        {
            get
            {
                return _dbbatch.SerialNumber;
            }
        }
        public int Year
        {
            get
            {
                return _dbbatch.Year;
            }
            set { }
        }
        public int SchoolTypeId
        {
            get
            {
                return _dbbatch.SchoolTypeId;
            }
        }
    }
    
}