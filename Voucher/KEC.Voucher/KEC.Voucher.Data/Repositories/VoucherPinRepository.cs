using System.Linq;
using KEC.Voucher.Data.Models;
using KEC.Voucher.Services.Helpers;

namespace KEC.Voucher.Data.Repositories
{
    public class VoucherPinRepository : Repository<DbVoucherPin>
    {
        public VoucherPinRepository(VoucherDb context) : base(context)
        {
        }
        public void MarkPinAsUsed(int id)
        {
            var ctx = _context as VoucherDb;
           var pin = ctx.VoucherPins.Find(id);
            pin.Status = PinStatus.Used;
            ctx.SaveChanges();
        }
        public string GetVoucherPin()
        {
            var pin = string.Empty;
            do
            {
                pin = RandomCodeGenerator.VoucherPin();
            } while (Find(p => p.Pin.Equals(pin)).FirstOrDefault() != null);
           
            return pin;
        }
    }
}
