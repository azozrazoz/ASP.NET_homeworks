using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication1.Controllers
{
    public class MyController : IController
    {
        public void Execute(RequestContext requestContext)
        {
            var ip = requestContext.HttpContext.Request.UserHostAddress;
            var response = requestContext.HttpContext.Response;

            response.Write("<h3>My IP address: " + ip + "</h3>");

            // Console.WriteLine(ip.ToString());
        }
    }
}