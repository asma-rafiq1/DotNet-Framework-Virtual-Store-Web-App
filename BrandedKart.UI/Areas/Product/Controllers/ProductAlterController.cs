using Brandedkart.DTO.Product;
using BrandedKart.Domain.Repositories.ProductRepo;
using BrandedKart.UI.CommonTasks;
using BrandedKart.UI.Controllers;
using BrandedKart.UI.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BrandedKart.UI.Areas.Product.Controllers
{
    [RouteArea("Product", AreaPrefix = "Admin/Product"), Authorize(Roles = "Admin")]
    public class ProductAlterController : BaseController
    {
        private readonly IProductService _productService;

        public ProductAlterController(IProductService service)
        {
            this._productService = service;
        }


        [HttpGet, Route("Create")]
        public ViewResult CreateProduct()
        {
            ViewData["Category"] = new SelectList(_productService.GetCategories(), "Category_ID", "Category_Title");
            return View();
        }


        [HttpPost, Route("Create"), ValidateAntiForgeryToken]
        public ActionResult CreateProduct([Bind(Exclude = "Product_ID,Publish_Date,Product_ImagePath,UnitOnOrder")] ProductDTO product)
        {
            ViewData["Category"] = new SelectList(_productService.GetCategories(), "Category_ID", "Category_Title");
            if (ModelState.IsValid && product.ProductImage is not null and { ContentLength: > 0 })
            {
                string ImagePath = HandleImage.SaveImage<ProductDTO>(product.ProductImage, product, HandleImage.productImageFolder, Server);

                if (ImagePath is null)
                {
                    ModelState.AddModelError(String.Empty, "Error: Something went wrong. Please try again");
                    return View(product);
                }

                product.Product_ImagePath = ImagePath;
                _productService.AddProduct(product);
                ModelState.Clear();

                return RedirectToAction("", "", new { area = "" });

            }
            return View(product);
        }

        [HttpGet, Route("Edit/{productId:int}")]
        public ActionResult UpdateProduct(int productId)
        {
            ViewData["Category"] = new SelectList(_productService.GetCategories(), "Category_ID", "Category_Title");

            var product = _productService.GetProductByID(productId);

            if (product?.Product_ID is null) return RedirectToAction("NotFound", "error", new { area = "" });

            return View("CreateProduct", product);
        }

        [HttpPost, Route("Edit"), ValidateAntiForgeryToken]
        public ActionResult UpdateProductDetails(ProductDTO updatedProduct)
        {
            ViewData["Category"] = new SelectList(_productService.GetCategories(), "Category_ID", "Category_Title");

            if (!ModelState.IsValid) return View();

            if (updatedProduct.ProductImage is not null)
            {
                if (updatedProduct.Product_ImagePath is not null)
                {
                    //exception,close resources
                    string existingProductImgPath = Path.Combine(Server.MapPath(AppStringVariables.baseContentDirectoryPath + HandleImage.productImageFolder), updatedProduct.Product_ImagePath);
                    System.IO.File.Delete(existingProductImgPath);
                }
                string productImagePath = HandleImage.SaveImage<ProductDTO>(updatedProduct.ProductImage, updatedProduct, HandleImage.productImageFolder, Server);
                if (productImagePath is null) return RedirectToAction("ServerError", "error", new { area = "" });

                updatedProduct.Product_ImagePath = productImagePath;
            }

            _productService.UpdateProduct(updatedProduct);
            ModelState.Clear();
            return RedirectToAction("Product", "Details", new { productId = updatedProduct.Product_ID });

        }

        [HttpPost, Route("Delete")]
        public ActionResult RemoveProduct(int productId)
        {
            var product = _productService.GetProductByID(productId);

            if (product?.Product_ImagePath is not null)
            {
                string existingProductImgPath = Path.Combine(Server.MapPath(AppStringVariables.baseContentDirectoryPath + HandleImage.productImageFolder), product.Product_ImagePath);
                System.IO.File.Delete(existingProductImgPath);
                _productService.RemoveProduct(productId);
                return View("Home");
            }
            return RedirectToAction("ServerError", "error", new { area = "" });
        }
        protected override void Dispose(bool disposing)
        {
            _productService.Dispose();
            base.Dispose(disposing);
        }

    }
}