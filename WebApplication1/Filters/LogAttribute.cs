using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Filters
{
    public class LogAttribute : ActionFilterAttribute
    {
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