using KEC.Voucher.Data.Models;
using System.Collections.Generic;

namespace KEC.Voucher.Web.Api.Models
{
    public class VoucherApprovalParam
    {
        public int VoucherId { get; set; }
        public VoucherStatus Status { get; set; }
        public int BatchId { get; set; }
        public string UserGuid { get; set; }
        public List<int> SelectedVouchers { get; set; }
    }
}