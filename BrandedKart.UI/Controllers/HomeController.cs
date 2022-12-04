using BrandedKart.Domain.Repositories.ProductRepo;
using BrandedKart.UI.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrandedKart.UI.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        [Route("Home"), Route("")]
        public ViewResult BrandedKartHome() => View("Home");


        [ChildActionOnly]
        public PartialViewResult GetCartItemCount()
        {
            Dictionary<int, int> cartItemList = null;
            if (Session["Cart"] is not null)
            {
                cartItemList = (Dictionary<int, int>)Session["Cart"];
                TempData[AppStringVariables.sessionCartTemp] = cartItemList;
            }
            return PartialView("NavItemCart", cartItemList is not null ? cartItemList.Count : 0);
        }

        [ChildActionOnly]
        public PartialViewResult GetTopProducts()
        {
            return PartialView("_HomeProductCard", _productService.GetTopProducts());
        }

        protected override void Dispose(bool disposing)
        {
            _productService.Dispose();
            base.Dispose(disposing);
        }

    }
}