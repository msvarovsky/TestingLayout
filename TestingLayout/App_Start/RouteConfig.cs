using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TestingLayout
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Lang",
                url: "Home/LanguageChange",
                defaults: new { controller = "Home", action = "LanguageChange", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "UsersAdmin",
                url: "users/{action}/{id}",
                defaults: new { controller = "Users", action = "AllUsers", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
