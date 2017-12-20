using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEC.Voucher.Data.Models
{
    class DbSchools
    {
        public int Id { get; set; }
        public string SchoolName { get; set; }
        public string SchoolCode { get; set; }
        public DbSchoolAdmin SchoolAdminID { get; set; }
        public DbSchoolTypes SchoolTypeID { get; set; }
        public DbCounties CountyID { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateChanged { get; set; }

    }
}
