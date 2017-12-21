using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEC.Voucher.Data.Models
{
   public class DbBatch
    {
        public int Id { get; set; }
        public DbCounty CountyId { get; set; }
        //Batch number will be Countycode plus some appended figures :-)
        public string BatchNumber { get; set; }
        //Serial number should be a GUID
        public string SerialNumber { get; set; }
        public int Year { get; set; }
        public int SchoolType { get; set; }

    }
}
