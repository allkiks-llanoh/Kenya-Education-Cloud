using System;
using System.Collections.Generic;
using System.Text;

namespace KEC.ECommerce.Data.Models
{
    public class Cart
    {
        public Cart()
        {
            CartItems = new List<CartItem>();
        }
        #region Properties
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        #endregion

        #region Virtual Properties
        public ICollection<CartItem> CartItems { get; set; }
        #endregion
    }
}
