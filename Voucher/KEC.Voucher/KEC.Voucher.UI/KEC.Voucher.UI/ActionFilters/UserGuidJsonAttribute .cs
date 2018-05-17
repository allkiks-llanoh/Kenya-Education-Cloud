using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace KEC.Voucher.UI.ActionFilters
{
    public class UserGuidJsonAttribute : ActionFilterAttribute
    {
        
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            SetUserGuid(filterContext);
            base.OnActionExecuting(filterContext);
        }
        public void SetUserGuid(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var user = filterContext.HttpContext.User.Identity as ClaimsIdentity;
                var signedInUserID = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                filterContext.RequestContext.HttpContext.Response.AddHeader("X-User-Guid", signedInUserID ?? string.Empty);
            }
            else
            {
                filterContext.HttpContext.Response.Headers.Remove("X-User-Guid");
            }
            
        }
    }
}