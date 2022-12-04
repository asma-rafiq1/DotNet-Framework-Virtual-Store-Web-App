using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BrandedKart.UI.Controllers
{
    public class ErrorController : Controller
    {

        public ViewResult NotFound()
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            //ViewBag.URL = RouteData.Values["url"].ToString();
            return View();
        }

        public ViewResult ServerError()
        {
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            ViewBag.Message = "Error occured!";
            return View();
        }


        public ViewResult ForbiddenError()
        {
            Response.StatusCode = (int)HttpStatusCode.Forbidden;
            ViewBag.Message = "Error occured!";
            return View();
        }
    }
}