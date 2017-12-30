using KEC.Voucher.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEC.Voucher.Data.Repositories
{
    public class SchoolTypeRepository : Repository<DbSchoolType>
    {
        public SchoolTypeRepository(VoucherDb context) : base(context)
        {
        }
    }
}
