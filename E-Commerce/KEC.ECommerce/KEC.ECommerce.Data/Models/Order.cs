using System;
using System.Collections.Generic;

namespace KEC.ECommerce.Data.Models
{
    public class Order
    {
        public Order()
        {
            LineItems = new List<LineItem>();
        }
        #region Properties
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime SubmittedAt { get; set; }
        public decimal Amount { get; set; }
        public string CustomerGuid { get; set; }
        #endregion

        #region Virtual Properties
        public ICollection<LineItem> LineItems { get; set; }
        #endregion
    }
}
