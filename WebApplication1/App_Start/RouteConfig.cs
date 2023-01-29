using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication1
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("Home/Index/12");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }/*,
                constraints: new { controller = "^P.*", id = @"\d{2}"}*/
            );
            /*Route newRoute = new Route("{controller}/{action}", new MvcRouteHandler());
            Route newRoute2 = new Route("{controller}/{id}", new MvcRouteHandler());
            routes.Add(newRoute);
            routes.Add(newRoute2);*/
        }
    }
}
