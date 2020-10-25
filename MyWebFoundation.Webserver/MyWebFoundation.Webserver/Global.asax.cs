using MyWebFoundation.Framework.Factories;
using MyWebFoundation.Webserver.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MyWebFoundation.Framework.Extensions;

namespace MyWebFoundation.Webserver
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ControllerBuilder.Current.SetControllerFactory(new MyControllerFactory());
            this.GetType().Info(System.Reflection.MethodBase.GetCurrentMethod().Name, "程序启动");
        }
    }
}
