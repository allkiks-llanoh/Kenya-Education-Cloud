using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEC.Voucher.Data.Models
{
    public class DbSchoolType
    {
        public int Id { get; set; }
        public string SchoolType { get; set; }
        public ICollection<DbSchool> Schools { get; set; }
    }
}
