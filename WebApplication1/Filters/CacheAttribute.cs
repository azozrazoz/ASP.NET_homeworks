using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Filters
{
    public class CacheAttribute : ActionFilterAttribute
    {
        public int Duraction { get; set; }
        public CacheAttribute()
        {
            Duraction = 2000;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (Duraction <= 0)
            {
                return;
            }

            HttpCachePolicyBase cache = filterContext.HttpContext.Response.Cache;
            TimeSpan cacheDuration = TimeSpan.FromSeconds(Duraction);

            cache.SetCacheability(HttpCacheability.Public);
            cache.SetExpires(DateTime.Now.Add(cacheDuration));
            cache.SetMaxAge(cacheDuration);

            cache.AppendCacheExtension("must-revalidate, proxy-revalidate");
        }
    }
}