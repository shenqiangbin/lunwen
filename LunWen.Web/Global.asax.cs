﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using LunWen.Web.Filters;
using LunWen.Infrastructure;
using LunWen.Web.Common;
using System.Web.Optimization;

namespace LunWen.Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalFilters.Filters.Add(new ExcepitonFilter());
            GlobalFilters.Filters.Add(new UserAuthorizeAttribute());
            GlobalFilters.Filters.Add(new TimingActionFilter());

            AutofacHelper.Inject();
        }

        protected void Application_AuthorizeRequest(object sender, System.EventArgs e)
        {
            SessionHelper.SetUser();
        }
    }
}