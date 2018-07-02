using Microsoft.Extensions.Configuration;

namespace KEC.ECommerce.Web.UI.Pagination
{
    public class PageConfig : IPageConfig
    {
       
        private int _pageSize;

        
        public int PageSize
        {
            get
            {
                
                return _pageSize < 1 ? 20 : _pageSize;
            }
            set
            {
                _pageSize = value;
            }
        }
    }
}