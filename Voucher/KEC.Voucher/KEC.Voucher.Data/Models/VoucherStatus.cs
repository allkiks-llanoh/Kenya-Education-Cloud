using System.ComponentModel;

namespace KEC.Voucher.Data.Models
{
    public enum VoucherStatus
    {
    [Description("Created")]
     Created=0,
     [Description("Active")]
     Active =1,
     [Description("Suspended")]
     Suspended=2,
     [Description("Expired")]
     Expired = 3,
     [Description("Rejected")]
     Rejected=4

    }
}
