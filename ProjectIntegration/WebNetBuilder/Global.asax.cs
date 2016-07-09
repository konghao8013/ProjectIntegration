﻿using WebNetBuilder.App_Start;
using System;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;

namespace WebNetBuilder
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configure(WebApiConfig.Configure);
            FilterConfig.Configure(GlobalFilters.Filters);
            RouteConfig.Configure(RouteTable.Routes);
       
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            if (ex != null)
            {
                Trace.TraceError(ex.ToString());
            }
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}