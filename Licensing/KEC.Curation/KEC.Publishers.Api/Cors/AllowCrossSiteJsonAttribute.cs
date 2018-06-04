using Microsoft.AspNetCore.Mvc.Filters;

namespace KEC.Publishers.Api.Cors
{
    public class AllowCrossSiteJsonAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Response != null && !context.HttpContext.Response.Headers.ContainsKey("Access-Control-Allow-Origin"))
            {
                context.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            }
            base.OnActionExecuted(context);
        }
    }
}
