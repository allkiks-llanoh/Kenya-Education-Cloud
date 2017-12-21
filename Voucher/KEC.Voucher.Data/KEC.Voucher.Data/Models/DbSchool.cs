using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEC.Voucher.Data.Models
{
    public class DbSchool
    {
        public int Id { get; set; }
        public string SchoolName { get; set; }
        public string SchoolCode { get; set; }
        public DbSchoolAdmin SchoolAdminID { get; set; }
        public DbSchoolType SchoolTypeID { get; set; }
        public DbCounty CountyID { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateChanged { get; set; }

    }
}
