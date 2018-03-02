using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace KEC.Curation.UI.ActionFilters
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
            var signedInUserID = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            filterContext.RequestContext.HttpContext.Response.Headers.Set("X-User-Guid", signedInUserID ?? string.Empty);
            
        }
    }
}