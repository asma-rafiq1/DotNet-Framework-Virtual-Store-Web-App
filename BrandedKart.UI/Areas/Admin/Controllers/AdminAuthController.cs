using Brandedkart.DTO.Customer;
using Brandedkart.DTO.User;
using BrandedKart.Domain.Repositories.AdminRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BrandedKart.UI.Areas.Admin.Controllers
{
    [RouteArea("Admin", AreaPrefix = "Admin")]
    public class AdminAuthController : Controller
    {
        private readonly IAdminService adminService;

        public AdminAuthController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        [Route("SignIn")]
        public ViewResult AdminLogin() => View();


        [HttpPost, Route("SignIn")]
        public async Task<ActionResult> AdminLogin(AdminDTO admin)
        {
            if (ModelState.IsValid)
            {
                AdminDTO adminInfo = await adminService.AdminLogin(admin);
                if (adminInfo?.Admin_Name is null)
                {
                    ModelState.AddModelError(String.Empty, "Error: Please enter valid credentials");
                    return View();
                }
                FormsAuthentication.SetAuthCookie(adminInfo.Admin_Name, false);
                return RedirectToAction("", "", new { area = "" });
            }
            return View(admin);

        }
        protected override void Dispose(bool disposing)
        {
            adminService.Dispose();
            base.Dispose(disposing);
        }
    }
}