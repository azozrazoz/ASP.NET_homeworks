using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Filters
{
    public class AuthorizationFilter : AuthorizeAttribute
    {
        private string[] allowedUsers = new string[]
        {
            "Sergey",
            "Dmitriy",
        };

        private string[] allowedRoles = new string[]
        {
            "admin",
            "moderator",
        };

        protected virtual bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!String.IsNullOrEmpty(base.Users))
            {
                allowedUsers = base.Users.Split(new char[] { ',' });

                for (int i = 0; i< allowedUsers.Length; i++)
                {
                    allowedUsers[i] = allowedUsers[i].Trim();
                }
            }

            if (!String.IsNullOrEmpty(base.Roles))
            {
                allowedRoles = base.Users.Split(new char[] { ',' });

                for (int i = 0; i < allowedRoles.Length; i++)
                {
                    allowedRoles[i] = allowedRoles[i].Trim();
                }
            }

            return httpContext.Request.IsAuthenticated && User(httpContext) && Role(httpContext);
        }

        private bool User(HttpContextBase httpContext)
        {
            if (allowedUsers.Length > 0)
            {
                return allowedUsers.Contains(httpContext.User.Identity.Name);
            }
            return false;
        }

        private bool Role(HttpContextBase httpContext)
        {
            if (allowedRoles.Length > 0)
            {
                for (int i = 0; i < allowedRoles.Length; i++)
                {
                    if (httpContext.User.IsInRole(allowedRoles[i]))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

    }
}
