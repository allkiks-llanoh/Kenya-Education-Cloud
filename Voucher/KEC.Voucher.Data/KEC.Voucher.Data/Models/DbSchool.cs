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
        public int SchoolAdminID { get; set; }
        public int SchoolTypeID { get; set; }
        public int CountyID { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateChanged { get; set; }
        public virtual DbSchoolType SchoolType { get; set; }
        public virtual DbSchoolAdmin SchoolAdmin { get; set; }

    }
}
