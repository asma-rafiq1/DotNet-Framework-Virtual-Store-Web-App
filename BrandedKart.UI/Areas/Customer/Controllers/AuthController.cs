using Brandedkart.DTO.Customer;
using BrandedKart.Domain.Repositories.CustomerRepo;
using BrandedKart.UI.CommonTasks;
using BrandedKart.UI.Controllers;
using BrandedKart.UI.Utilities;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BrandedKart.UI.Areas.Customer.Controllers
{
    [RouteArea("Customer", AreaPrefix = "Customer")]
    public class AuthController : BaseController
    {
        private readonly ICustomerService _custservice;

        public AuthController(ICustomerService service)
        {
            this._custservice = service;
        }


        [HttpGet, Route("Register"), OutputCache(CacheProfile = "1minutecache")]
        public ViewResult CustomerRegisteration() => View();



        [HttpPost, Route("Register"), ActionName("CustomerRegisteration"), ValidateAntiForgeryToken]
        public ActionResult Register(HttpPostedFileBase UserImage)
        {
            CustomerDTO customer = new();
            bool UserModelUpdated = TryUpdateModel<CustomerDTO>(customer, null, null, new string[] { "Address_Info", "Image_Path", "Customer_ID" });

            if (UserModelUpdated && UserImage is not null and { ContentLength: > 0 })
            {
                string imagePath = HandleImage.SaveImage<CustomerDTO>(UserImage, customer, HandleImage.userImageFolder, Server);

                if (imagePath is null)
                {
                    ModelState.AddModelError(String.Empty, "Error: Something went wrong. User Registration failed");
                    return View(customer);
                }
                customer.Image_Path = imagePath;
                var customerInfo = _custservice.AddCustomer(customer);

                if (customerInfo?.Customer_ID is null)
                {
                    ModelState.AddModelError(String.Empty, "Error: Something went wrong. User Registration failed");
                    return View(customer);
                }

                FormsAuthentication.SetAuthCookie(customerInfo.Full_Name, false);
                HttpCookie userCookie = new HttpCookie(AppStringVariables.UserIdCookie);
                userCookie.Value = customerInfo.Customer_ID.ToString();
                Response.Cookies.Add(userCookie);
                ModelState.Clear();
                return RedirectToAction("", "Home", new { area = "" });
            }

            return View(customer);
        }



        [HttpGet, Route("SignIn")]
        public ViewResult CustomerSignIn() => View();



        [HttpPost, Route("SignIn"), ValidateAntiForgeryToken]
        public async Task<ActionResult> CustomerSignIn([Bind(Include = "Email,Customer_Password,RememberMe")] CustomerDTO customer)
        {
            if (!ModelState.IsValid) return View(customer);

            CustomerDTO customerInfo = await _custservice.LoginCustomer(customer);

            if (customerInfo?.Customer_ID is null)
            {
                ModelState.AddModelError(String.Empty, "Error: User Not Found");
                return View(customer);
            }

            FormsAuthentication.SetAuthCookie(customerInfo.Full_Name, customer.RememberMe);
            HttpCookie userCookie = new HttpCookie(AppStringVariables.UserIdCookie);
            userCookie.Value = customerInfo.Customer_ID.ToString();
            if (customer.RememberMe is true)
            {
                userCookie.Expires = DateTime.Now.AddDays(1);
            }
            Response.Cookies.Add(userCookie);

            ModelState.Clear();
            return RedirectToAction("", "", new { area = "" });

        }

        [Route("~/Logout"), Authorize]
        public ActionResult CustomerLogout()
        {
            if (Request.Cookies[AppStringVariables.UserIdCookie] is not null)
            {
                HttpCookie userCookie = Request.Cookies[AppStringVariables.UserIdCookie];
                userCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(userCookie);
            }

            FormsAuthentication.SignOut();
            return RedirectToAction("", "", new { area = "" });
        }

        //Remote Email Validation
        [HttpGet, Route("Validate/IsUserEmailAvailable")]
        public async Task<JsonResult> IsUserEmailAvailable(string Email) =>
            Json(await _custservice.IsUserEmailAvailable(Email), JsonRequestBehavior.AllowGet);

        protected override void Dispose(bool disposing)
        {
            _custservice.Dispose();
            base.Dispose(disposing);
        }

    }
}