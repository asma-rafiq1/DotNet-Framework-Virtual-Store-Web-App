using BrandedKart.UI.CommonTasks;
using BrandedKart.UI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BrandedKart.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            UnityConfig.RegisterComponents();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AreaRegistration.RegisterAllAreas();
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }


        //Method 2 of Logging and Handling all remaining Exceptions (Http Application Level)
        protected void Application_Error()
        {
            var exception = Server.GetLastError();
            Server.ClearError();
            //log the error!
            Logger.GetInstance.LogException(exception);
            Context.Response.Clear();
            Context.ClearError();
            var httpException = exception as HttpException;
            var routeData = new RouteData();
            routeData.Values["controller"] = "error";
            routeData.Values["action"] = "ServerError";
            routeData.Values.Add("url", Context.Request.Url.OriginalString);
            Response.TrySkipIisCustomErrors = true;
            if (httpException is not null)
            {
                switch (httpException.GetHttpCode())
                {
                    case 404:
                        routeData.Values["action"] = "NotFound";
                        break;
                    case 403:
                        routeData.Values["action"] = "ServerError";
                        break;
                    case 500:
                        routeData.Values["action"] = "ServerError";
                        break;
                }
            }
            IController controller = new ErrorController();
            controller.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
        }
    }
}
