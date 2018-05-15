using System;
using System.Collections.Generic;
using System.Text;

namespace KEC.ECommerce.Data.Models
{
    public class Cart
    {
        public Cart()
        {
            CartItems = new List<ShoppingCartItem>();
        }
        #region Properties
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        #endregion

        #region Virtual Properties
        public virtual ICollection<ShoppingCartItem> CartItems { get; set; }
        #endregion
    }
}
