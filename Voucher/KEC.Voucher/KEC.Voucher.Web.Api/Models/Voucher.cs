using KEC.Voucher.Data.Models;
using System.Collections.Generic;

namespace KEC.Voucher.Web.Api.Models
{
    public class Voucher
    {
        private readonly DbVoucher _dbVoucher;

        public Voucher(DbVoucher dbVoucher)
        {
            _dbVoucher = dbVoucher;
        }
        public int Id { get; set; }
        public string VoucherCode { get; set; }
        public string VoucherSerial { get; set; }
        public int VoucherYear { get; set; }
        public int SchoolId { get; set; }
        public int StatusId { get; set; }
        public int WalletId { get; set; }
        public int BatchId { get; set; }
       
    }
}