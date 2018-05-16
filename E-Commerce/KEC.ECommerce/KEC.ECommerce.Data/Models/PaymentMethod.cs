using System.ComponentModel;

namespace KEC.ECommerce.Data.Models
{
    public enum PaymentMethod
    {
        [Description("KEC Voucher")]
        Voucher = 0,
        [Description("Safaricom Mpesa")]
        Mpesa = 1
    }
}
