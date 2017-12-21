using KEC.Voucher.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEC.Voucher.Data.Repositories
{
    public class SchoolRepository : Repository<DbSchool>
    {
        public SchoolRepository(DbContext context) : base(context)
        {
        }
    }
}
