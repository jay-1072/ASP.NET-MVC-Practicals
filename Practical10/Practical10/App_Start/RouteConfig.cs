using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Practical10
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Test",
                url: "Employee/{name}",
                defaults: new { controller = "Employee", action = "Index", name = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "EmptyTest",
                url: "Result/{action}",
                defaults: new { controller = "Result", action = "Empty", name = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
