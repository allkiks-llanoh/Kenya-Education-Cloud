using KEC.Voucher.Data.Models;
using System.Data.Entity.ModelConfiguration;
namespace KEC.Voucher.Data.Mappings
{
    internal class DbFundAllocationMap : EntityTypeConfiguration<DbFundAllocation>
    {
        public DbFundAllocationMap()
        {
            ToTable("FundAllocations");
                
        }
    }
}
