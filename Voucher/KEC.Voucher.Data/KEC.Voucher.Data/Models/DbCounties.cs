using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEC.Voucher.Data.Models
{
    class DbCounties
    {
        public int Id { get; set; }
        public string CountyName { get; set; }
        public int CountyCode { get; set; }
        public int MyProperty { get; set; }
        public virtual ICollection<DbBatches> DbBatches { get; set; }
        public virtual ICollection<DbSchools> DbSchools { get; set; }

    }
}
