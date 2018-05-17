using System.Web.Mvc;

namespace KEC.Voucher.UI.ActionFilters
{
    public class AllowCrossSiteJsonAttribute: ActionFilterAttribute
    {
        
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
          
            filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Controll-Allow-Origin", "https://curationapi-d/api");
            base.OnActionExecuting(filterContext);
        }
    }
}