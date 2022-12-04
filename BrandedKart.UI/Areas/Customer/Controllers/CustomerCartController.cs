using Brandedkart.DTO.Product;
using BrandedKart.Domain.Repositories.ProductRepo;
using BrandedKart.UI.Controllers;
using BrandedKart.UI.Utilities;
using BrandedKart.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BrandedKart.UI.Areas.Customer.Controllers
{
    [RouteArea("Customer", AreaPrefix = "Customer")]
    public class CustomerCartController : BaseController
    {
        private readonly IProductService _productService;

        public CustomerCartController(IProductService productService)
        {
            this._productService = productService;
        }

        [Route("Cart")]
        public ViewResult ViewCart()
        {
            var cartItemsWithId = (Dictionary<int, int>)Session[AppStringVariables.sessionCart];

            if (cartItemsWithId is null or { Count: <= 0 }) return View("EmptyCart");

            var cartItems = _productService.GetCartItems(cartItemsWithId);

            CartViewModel cartViewModel = new()
            {
                CartItems = cartItems
            };

            if (cartViewModel.CartItems.Count().Equals(0)) return View("EmptyCart");

            var CartDetails = _productService.CalculateBill(cartViewModel);
            return View("CartView", CartDetails);
        }

        [Route("AddCart")]
        public ActionResult AddToCart(int ProductID, int Quantity)
        {

            ProductDTO product = _productService.GetProductByID(ProductID);

            if (product?.Product_ID is null) return RedirectToAction("NotFound", "error", new { area = "" });

            var cartItemList = (Dictionary<int, int>)Session[AppStringVariables.sessionCart];

            if (cartItemList is null) cartItemList = new Dictionary<int, int>();

            if (cartItemList.ContainsKey(ProductID))
            {
                cartItemList[product.Product_ID] = Quantity;
            }
            else
            {
                cartItemList.Add(product.Product_ID, Quantity);
            }

            Session[AppStringVariables.sessionCart] = cartItemList;

            return RedirectToAction("Cart", "Customer", new { area = "" });
        }


        [Route("RemoveCart")]
        public ActionResult RemoveFromCart(int ProductID)
        {
            var cartItemList = (Dictionary<int, int>)Session[AppStringVariables.sessionCart];
            if (cartItemList is null) return View("EmptyCart");

            if (cartItemList.ContainsKey(ProductID)) cartItemList.Remove(ProductID);

            if (cartItemList.Count.Equals(0))
            {
                Session.Remove(AppStringVariables.sessionCart);
                return View("EmptyCart");
            }

            Session[AppStringVariables.sessionCart] = cartItemList;

            return RedirectToAction("Cart", "Customer", new { area = "" });
        }

        protected override void Dispose(bool disposing)
        {
            _productService.Dispose();
            base.Dispose(disposing);
        }

    }
}