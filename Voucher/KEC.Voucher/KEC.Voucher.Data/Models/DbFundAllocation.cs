namespace KEC.Voucher.Data.Models
{
    public class DbFundAllocation
    {
        public int id { get; set; }
        public decimal Amount { get; set; }
        public int SchoolId { get; set; }
        public int Year { get; set; }
        public virtual DbSchool School { get; set; }
    }
}
