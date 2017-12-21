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
        public int CountyCode { get; set; }
        public int MyProperty { get; set; }
        public virtual ICollection<DbBatch> DbBatches { get; set; }
        public virtual ICollection<DbSchool> DbSchools { get; set; }

    }
}
