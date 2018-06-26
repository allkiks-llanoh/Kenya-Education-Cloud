using System;
using System.Collections.Generic;
using System.Text;

namespace KEC.ECommerce.Data.Models
{
    public class Publisher
    {
        public Publisher()
        {
            Publications = new List<Publication>();
        }
        #region Properties
        public int Id { get; set; }
        public string Company { get; set; }
        public string Guid { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        #endregion

        #region Virtual Properties
        public virtual ICollection<Publication> Publications { get; set; }
        #endregion
    }
}
