using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Filters
{
    public class ActionFilter : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // filterContext.HttpContext.Response.Write("Action done"); 
            filterContext.HttpContext.Response
                .Write("Current User:" + filterContext.HttpContext.Timestamp);
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            /*if (filterContext.HttpContext.Request.Browser.Browser == "Chrome")
            {
                filterContext.Result = new HttpNotFoundResult();
            }*/

            filterContext.HttpContext.Response
                .Write("Current User:" + filterContext.HttpContext.User.Identity.Name);
        }
    }
}