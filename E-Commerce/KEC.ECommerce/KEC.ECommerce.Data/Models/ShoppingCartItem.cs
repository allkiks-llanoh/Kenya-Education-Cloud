using System;
using System.Collections.Generic;
using System.Text;

namespace KEC.ECommerce.Data.Models
{
    public class ShoppingCartItem
    {
        #region Properties
        public int Id { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        #endregion

        #region Foreign Keys
        public int PublicationId { get; set; }
        public int CartId { get; set; }
        #endregion

        #region Virtual Properties
        public virtual Cart Cart { get; set; }
        public virtual Publication Publication { get; set; }
        #endregion
    }
}
