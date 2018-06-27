using System.Collections.Generic;

namespace KEC.ECommerce.Web.UI.Pagination
{
    public class ResultSet<T> : IResultSet<T>
    {
        public IEnumerable<T> Items { get; set; }

        public Pager Pager { get; set; }
    }
}