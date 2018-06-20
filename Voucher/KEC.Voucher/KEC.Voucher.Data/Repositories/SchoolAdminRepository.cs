using KEC.Voucher.Data.Models;
using System.Linq;

namespace KEC.Voucher.Data.Repositories
{
    public class SchoolAdminRepository : Repository<DbSchoolAdmin>
    {
        private readonly VoucherDb _voucherDbContext;

        public SchoolAdminRepository(VoucherDb context) : base(context)
        {
            _voucherDbContext = context as VoucherDb;
        }
        public DbSchoolAdmin AddFromCSV(DbSchoolAdmin schoolAdmin)
        {
            var retrievedSchoolAdmin = _voucherDbContext.SchoolAdmins
                                       .FirstOrDefault(p => p.Email.Equals(schoolAdmin.Email));
            var addedSchoolAdmin = default(DbSchoolAdmin);
            if (retrievedSchoolAdmin == null)
            {
                Add(schoolAdmin);
                addedSchoolAdmin = schoolAdmin;
            }
            else
            {
                addedSchoolAdmin = retrievedSchoolAdmin;
            }
            return addedSchoolAdmin;
        }
    }
}
