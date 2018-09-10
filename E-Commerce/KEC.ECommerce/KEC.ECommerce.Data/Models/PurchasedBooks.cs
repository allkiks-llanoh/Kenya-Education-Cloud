using System;
using System.Collections.Generic;
using System.Text;

namespace KEC.ECommerce.Data.Models
{
    public class PurchasedBook
    {
        public PurchasedBook()
        {
            Publications = new List<Publication>();
        }
        public int Id { get; set; }
        public int PublicationId { get; set; }
        public string IdentificationCode { get; set; }
        public string OrderNumber { get; set; }
        public bool PaymentStatus { get; set; }
        public virtual ICollection<Publication> Publications { get; set; }
    }
}
