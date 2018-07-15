using System;

namespace KEC.ECommerce.Data.Models
{
    public class Payment
    {
        #region Properties
        public int Id { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string VoucherNumber { get; set; }
        public string TransactionNumber { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactedBy { get; set; }
        #endregion

        #region Foreign Properties
        public int OrderId { get; set; }
        #endregion

        #region Virtual Properties
        public virtual Order Order { get; set; }
        #endregion

    }
}
