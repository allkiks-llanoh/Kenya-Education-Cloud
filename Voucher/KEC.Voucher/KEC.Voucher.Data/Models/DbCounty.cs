using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEC.Voucher.Data.Models
{
    public class DbCounty
    {
        public int Id { get; set; }
        public string CountyName { get; set; }
        public string CountyCode { get; set; }
        
        public virtual ICollection<DbBatch> Batches { get; set; }
        public virtual ICollection<DbSchool> Schools { get; set; }

    }
}
