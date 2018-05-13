using System;
using System.Collections.Generic;
using System.Text;

namespace KEC.ECommerce.Data.Models
{
    public class LineItem
    {
        #region Properties
        public int Id { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        #endregion

        #region Foreign Keys
        public int PublicationId { get; set; }
        public int OrderId { get; set; }
        #endregion

        #region Virtual Properties
        public Order Order { get; set; }
        public Publication Publication { get; set; }
        #endregion

    }
}
