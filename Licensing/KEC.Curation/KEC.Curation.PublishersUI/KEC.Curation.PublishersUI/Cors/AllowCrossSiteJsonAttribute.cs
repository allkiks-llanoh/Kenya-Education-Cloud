using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KEC.Curation.PublishersUI.Cors
{
  
    public class AllowCrossSiteJsonAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Controll-Allow-Origin", "https://curationapi.kec.ac.ke");
            base.OnActionExecuting(filterContext);
        }
    }
}