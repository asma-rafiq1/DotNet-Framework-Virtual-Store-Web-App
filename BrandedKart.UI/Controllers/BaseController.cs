using BrandedKart.UI.CommonTasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BrandedKart.UI.Controllers
{
    public class BaseController : Controller
    {

        //Request matched with controller but action method not found (Controller Level)
        protected override void HandleUnknownAction(string actionName)
        {
            this.Invoke404NotFound(HttpContext);
        }

        public ActionResult Invoke404NotFound(HttpContextBase httpContext)
        {
            IController errorController = new ErrorController();
            var errorRoute = new RouteData();
            errorRoute.Values.Add("controller", "error");
            errorRoute.Values.Add("action", "NotFound");
            errorRoute.Values.Add("url", httpContext.Request.Url.OriginalString);
            errorController.Execute(new RequestContext(httpContext, errorRoute));
            return new EmptyResult();
        }

        //Controller Level Handling
        protected override void OnException(ExceptionContext filterContext)
        {

            Exception exception = filterContext.Exception;
            if (filterContext.HttpContext.Request.IsAjaxRequest() && exception is not null)
            {
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                filterContext.Result = new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new
                    {
                        filterContext.Exception.Message,
                        filterContext.Exception.StackTrace
                    }
                };
                filterContext.ExceptionHandled = true;
            }
            else
            {
                if (filterContext.ExceptionHandled || exception is null)
                    return;

                //log the error!
                Logger.GetInstance.LogException(exception);

                IController errorController = new ErrorController();
                var errorRoute = new RouteData();
                errorRoute.Values.Add("controller", "error");
                errorRoute.Values.Add("action", "ServerError");
                errorRoute.Values.Add("url", HttpContext.Request.Url.OriginalString);
                errorController.Execute(new RequestContext(HttpContext, errorRoute));
                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.Clear();
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            }
        }
    }
}