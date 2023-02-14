using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Filters
{
    public class LogAttribute : ActionFilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            ExceptionDetail exceptionDetail = new ExceptionDetail()
            {
                ExceptionMessage = filterContext.Exception.Message,
                StackTrace = filterContext.Exception.StackTrace,
                ControllerName = filterContext.RouteData.Values["Controller"].ToString(),
                Actionname = filterContext.RouteData.Values["Action"].ToString(),
                Date = DateTime.UtcNow,
            };

            using (LogContext db = new LogContext())
            {
                db.ExceptionDetails.Add(exceptionDetail);
                db.SaveChanges();
            }

            if (filterContext.ExceptionHandled && filterContext.Exception is IndexOutOfRangeException)
            {
                filterContext.Result = new RedirectResult("~/Shared/Error");
                filterContext.ExceptionHandled = true;
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var request = filterContext.HttpContext.Request;

            Visitor visitor = new Visitor()
            {
                Login = (request.IsAuthenticated) ? filterContext.HttpContext.User.Identity.Name : null,
                Ip = request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.UserHostAddress,
                Url = request.RawUrl,
                Date = DateTime.UtcNow
            };

            using (LogContext db = new LogContext())
            {
                db.Visitors.Add(visitor);
                db.SaveChanges();
            }

            base.OnActionExecuted(filterContext);

        }
    }
}