using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TimekeepingSystem
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
             name: "Record",
             url: "record-attendance",
             defaults: new { controller = "RecordAttendance", action = "Index" }
            );


            routes.MapRoute(
               name: "Login",
               url: "login",
               defaults: new { controller = "Login", action = "Index" }
           );

            routes.MapRoute(
              name: "Logout",
              url: "logout",
              defaults: new { controller = "Logout", action = "Index" }
          );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
