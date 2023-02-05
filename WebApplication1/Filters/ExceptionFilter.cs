using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Filters
{
    public class ExceptionFilter : FilterAttribute, IExceptionFilter
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
    }
}