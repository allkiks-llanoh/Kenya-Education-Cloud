using System;

namespace KEC.Voucher.Data.Models
{
    public class 
        DbStatus
    {
        public int Id { get; set; }
        public VoucherStatus StatusValue { get; set; }
        public DateTime TimeStamp { get; set; }
        public string ActivatedBy { get; set; }
        public virtual DbVoucher Voucher { get; set; }
        
    }
}
