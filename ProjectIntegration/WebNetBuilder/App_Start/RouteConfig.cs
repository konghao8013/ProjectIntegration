﻿using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebNetBuilder.App_Start
{
    public class RouteConfig
    {
        public static void Configure(RouteCollection routes)
        {
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: 
                new { controller = "Home", action = "ProjectStatus", id = UrlParameter.Optional }
            );
        }
    }
}