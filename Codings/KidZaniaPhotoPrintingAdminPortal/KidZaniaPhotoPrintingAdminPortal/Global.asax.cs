using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace KidZaniaPhotoPrintingAdminPortal
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_EndRequest(Object sender, EventArgs e)
        {
            if (HttpContext.Current.Response.Status.StartsWith("401"))
            {
                HttpContext.Current.Response.ClearContent();
                Response.Redirect("/Error/Unauthorized");
            } else if (HttpContext.Current.Response.Status.StartsWith("404"))
            {
                HttpContext.Current.Response.ClearContent();
                Response.Redirect("/Error/NotFound");
            }
        }
    }
}
