using System;
using System.Collections.Generic;
using System.Text;

namespace KEC.ECommerce.Data.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            ShoppingCartItems = new List<ShoppingCartItem>();
        }
        #region Properties
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        #endregion

        #region Virtual Properties
        public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
        #endregion
    }
}
