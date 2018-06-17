using KEC.Voucher.Data.Models;
using System.Linq;

namespace KEC.Voucher.Data.Repositories
{
    public class SchoolRepository : Repository<DbSchool>
    {
        private readonly VoucherDb _voucherDbContext;

        public SchoolRepository(VoucherDb context) : base(context)
        {
            _voucherDbContext = context as VoucherDb;
        }
        public void AddFromCSV(DbSchool school, DbFundAllocation fundAllocation, DbSchoolAdmin schoolAdmin)
        {
            var retrievedSchool = _voucherDbContext.Schools
                                   .FirstOrDefault(p => p.SchoolCode.Equals(school.SchoolCode));
           
            if (retrievedSchool != null)
            {

                var hasAllocation = _voucherDbContext.FundAllocations
                                     .Any(p => p.SchoolId == retrievedSchool.Id
                                     && p.Year == fundAllocation.Year);
                
                if (!hasAllocation)
                {
                    retrievedSchool.FundAllocations.Add(fundAllocation);
                }
                retrievedSchool.SchoolAdmin = schoolAdmin;
            }
            else
            {
                school.FundAllocations.Add(fundAllocation);
                school.SchoolAdmin = schoolAdmin;
                Add(school);
            }
           
        }
    }
}
