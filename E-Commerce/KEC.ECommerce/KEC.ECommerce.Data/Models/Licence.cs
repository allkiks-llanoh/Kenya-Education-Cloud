using System;
using System.Collections.Generic;
using System.Text;

namespace KEC.ECommerce.Data.Models
{
    public class Licence
    {
        #region Properties
        public int Id { get; set; }
        public string Code { get; set; }
        public string SchoolCode { get; set; }
        public DateTime ExpiryDate { get; set; }
        #endregion

        #region Foreign keys
        public int PublicationId { get; set; }
        public int OrderId { get; set; }
        #endregion

        #region Virtual Properties
        public virtual Order Order { get; set; }
        public virtual Publication Publication { get; set; } 
        #endregion

    }
}
