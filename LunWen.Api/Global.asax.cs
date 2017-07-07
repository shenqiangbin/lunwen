using LunWen.Api.Common;
using LunWen.Api.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Routing;

namespace LunWen.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            GlobalConfiguration.Configuration.Filters.Add(new WebApiAuthAttribute());

            AutofacHelper.Inject();
        }
    }
}
