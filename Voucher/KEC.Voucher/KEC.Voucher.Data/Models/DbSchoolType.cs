using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEC.Voucher.Data.Models
{
    public class DbSchoolType
    {
        public DbSchoolType()
        {
            Schools = new List<DbSchool>();
        }
        public int Id { get; set; }
        public string SchoolType { get; set; }
        public virtual ICollection<DbSchool> Schools { get; set; }
    }
}
