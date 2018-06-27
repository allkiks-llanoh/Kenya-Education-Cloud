using System.Collections.Generic;
using System.Linq;

namespace KEC.ECommerce.Web.UI.Pagination
{
    public interface IPageHelper<T>
    {
        IResultSet<T> GetPage(IQueryable<T> items, int pageNumber);
    }

    public interface IResultSet<T>
    {
        IEnumerable<T> Items { get; set; }
        Pager Pager { get; }
    }
}
