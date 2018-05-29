using System;
using System.Collections.Generic;
using System.Text;

namespace KEC.ECommerce.Data.Models
{
    public class Subject
    {
        public Subject()
        {
            Publications = new List<Publication>();
        }
        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        #endregion

        #region Virtual Properties
        public virtual ICollection<Publication> Publications { get; set; }
        #endregion
    }
}
