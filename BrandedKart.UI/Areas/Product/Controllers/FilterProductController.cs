using Brandedkart.DTO.Product;
using BrandedKart.Domain.Repositories.ProductRepo;
using BrandedKart.UI.Controllers;
using BrandedKart.UI.Utilities;
using Microsoft.Ajax.Utilities;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace BrandedKart.UI.Areas.Product.Controllers
{
    [RouteArea("Product", AreaPrefix = "")]
    public class FilterProductController : BaseController
    {
        private readonly IProductService _productService;

        public FilterProductController(IProductService service)
        {
            this._productService = service;
        }

        [Route("Products")]
        public ActionResult GetAllProducts(string productName, int? page)
        {
            if (string.IsNullOrWhiteSpace(productName))
            {
                productName = string.Empty;
            }

            var products = _productService
                .GetProductsByName(productName ??= string.Empty)
                .ToPagedList(page ?? 1, 3);

            return View(products);
        }

        [Route("Product/Details/{productId:int}")]
        public ActionResult SingleProduct(int productId)
        {
            ViewBag.CartItems = TempData[AppStringVariables.sessionCartTemp];
            ProductDTO product = _productService.GetProductByID(productId);
            if (product?.Product_ID is null) return RedirectToAction("NotFound", "error", new { area = "" });

            return View(product);
        }


        [ChildActionOnly]
        public PartialViewResult GetCategories()
        {
            return PartialView("_CategoryFilter", _productService.GetCategories());
        }

        [HttpGet, Route("GetCategoryFilteredProducts"), OutputCache(Duration = 180, VaryByParam = "categoryId", Location = OutputCacheLocation.Client)]
        public PartialViewResult GetCategoryFilteredProducts(int categoryId)
        {
            var products = _productService.GetCategoryProducts(categoryId);
            return PartialView("_ProductCardOne", products);
        }

        protected override void Dispose(bool disposing)
        {
            _productService.Dispose();
            base.Dispose(disposing);
        }


    }
}