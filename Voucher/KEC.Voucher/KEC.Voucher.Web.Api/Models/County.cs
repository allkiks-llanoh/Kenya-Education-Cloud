using KEC.Voucher.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KEC.Voucher.Web.Api.Models
{
    public class County
    {
        private readonly DbCounty _dbCounty;

        public County(DbCounty dbCounty)
        {
            _dbCounty = dbCounty;
        }
        public int Id
        {
            get
            {
                return _dbCounty.Id;
            }
        }
        public string CountyName
        {
            get
            {
                return _dbCounty.CountyName;
            }
        }
        public string CountyCode
        {
            get
            {
                return _dbCounty.CountyCode;
            }
        }
    }
}