using System;
using System.Collections.Generic;

namespace KEC.ECommerce.Data.Models
{
    public class Order
    {
        public Order()
        {
            LineItems = new List<LineItem>();
            Licences = new List<Licence>();
        }
        #region Properties
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime SubmittedAt { get; set; }
        public decimal Amount { get; set; }
        public string CustomerEmail { get; set; }
        public OrderStatus Status { get; set;}
        #endregion

        #region Virtual Properties
        public virtual ICollection<LineItem> LineItems { get; set; }
        public virtual ICollection<Licence> Licences { get; set; }
        public virtual Payment Payment { get; set; }
        #endregion
    }
}
