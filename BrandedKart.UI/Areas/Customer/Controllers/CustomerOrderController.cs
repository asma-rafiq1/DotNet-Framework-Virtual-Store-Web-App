using Brandedkart.DTO.Product;
using Brandedkart.DTO.User;
using BrandedKart.Domain.Repositories.OrderRepo;
using BrandedKart.Domain.Repositories.ProductRepo;
using BrandedKart.UI.Utilities;
using BrandedKart.UI.ViewModel;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrandedKart.UI.Areas.Customer.Controllers
{
    [RouteArea("Customer", AreaPrefix = ""), Authorize]
    public class CustomerOrderController : Controller
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;

        public CustomerOrderController(IProductService productService, IOrderService orderService)
        {
            this._productService = productService;
            this._orderService = orderService;
        }

        [Route("Checkout")]
        public ActionResult Checkout()
        {
            var orderDetails = GetItemsFromCart();
            if (orderDetails is null) return View("~/EmptyCart");
            ViewBag.CartItemsCount = orderDetails.customerOrderProducts.CartItems.Count();
            return View(orderDetails);
        }


        [HttpPost, Route("Checkout"), ValidateAntiForgeryToken]
        public ActionResult UserCheckout()
        {
            var order = GetItemsFromCart();
            if (order is null) return View("~/EmptyCart");

            var userId = Request.Cookies[AppStringVariables.UserIdCookie];
            if (userId is null) return RedirectToAction("ForbiddenError", "error", new { area = "" });
            order.Customer_Id = int.Parse(userId.Value);

            bool OrderModelUpdated = TryUpdateModel<OrderDetailsDTO>(order);
            var errors = ModelState.Select(x => x.Value.Errors).ToList();
            if (OrderModelUpdated)
            {
                _orderService.PlaceOrder(order);
                return View("~/Areas/Customer/Views/CustomerOrder/SuccessView.cshtml", order);
            }

            return View("~/Home");
        }

        [Route("PaymentWithPayPal")]
        public ActionResult PaymentWithPayPal()
        {
            var order = GetItemsFromCart();
            if (order is null) return View("~/EmptyCart");

            var apiContext = PayPalConfiguration.GetApiContext();
            try
            {
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    string baseURI = $"{Request.Url.Scheme}://{Request.Url.Authority}/{nameof(PaymentWithPayPal)}?";
                    var Guid = Convert.ToString((new Random().Next(100000000)));
                    var createPayment = this.CreatePayment(apiContext, baseURI + AppStringVariables.Guid + "=" + Guid);

                    var links = createPayment.links.GetEnumerator();
                    string paypalRedirectURL = null;

                    while (links.MoveNext())
                    {
                        Links link = links.Current;
                        if (link.rel.ToLower().Trim().Equals(AppStringVariables.approval_url))
                        {
                            paypalRedirectURL = link.href;
                        }
                    }

                    Session.Add(AppStringVariables.Guid, createPayment.id);
                    return Redirect(paypalRedirectURL);
                }
                else
                {
                    var guid = Request.Params[AppStringVariables.Guid];
                    var executePayment = ExecutePayment(apiContext, payerId, Session[AppStringVariables.Guid] as string);
                    if (executePayment.state.ToLower() != AppStringVariables.approved)
                    {
                        return View("Failure");
                    }
                }
            }
            catch (Exception ex)
            {
                return View("Failure");
            }

            return View("SuccessView");

        }

        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            this.payment = new Payment() { id = paymentId };
            return this.payment.Execute(apiContext, paymentExecution);
        }

        private Payment payment;
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            var order = GetItemsFromCart();

            var itemList = new ItemList() { items = new List<Item>() };

            foreach (var item in order.customerOrderProducts.CartItems)
            {
                itemList.items.Add(new Item()
                {
                    name = item.Product_Name,
                    currency = "USD",
                    price = item.Product_Price.ToString(),
                    quantity = item.Product_quantity.ToString(),
                    sku = item.Product_ID.ToString(),

                });
            }
            var payer = new Payer() { payment_method = "paypal" };
            var redirectUrls = new RedirectUrls()
            {
                return_url = redirectUrl,
                cancel_url = redirectUrl + "&cancel=true"
            };

            var details = new Details()
            {
                tax = order.customerOrderProducts.Tax.ToString(),
                shipping = order.customerOrderProducts.ShippingFee.ToString(),
                subtotal = order.customerOrderProducts.SubTotal.ToString()
            };

            var amount = new Amount()
            {
                currency = "USD",
                total = order.customerOrderProducts.Total.ToString(),
                details = details
            };

            var transactionList = new List<Transaction>();
            transactionList.Add(new Transaction()
            {
                description = "Transaction Description",
                invoice_number = "#10546879",
                amount = amount,
                item_list = itemList

            });

            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirectUrls,

            };

            return this.payment.Create(apiContext);
        }

        private OrderDetailsDTO GetItemsFromCart()
        {
            OrderDetailsDTO orderDetails;
            var cartItemsWithId = (Dictionary<int, int>)Session[AppStringVariables.sessionCart];
            if (cartItemsWithId is null or { Count: <= 0 }) return orderDetails = null;

            orderDetails = new OrderDetailsDTO();
            orderDetails.customerOrderProducts.CartItems = _productService.GetCartItems(cartItemsWithId);

            if (orderDetails.customerOrderProducts.CartItems.Count() <= 0) return orderDetails = null;
            orderDetails.customerOrderProducts = _productService.CalculateBill(orderDetails.customerOrderProducts);
            return orderDetails;
        }

        protected override void Dispose(bool disposing)
        {
            _orderService.Dispose();
            _productService.Dispose();
            base.Dispose(disposing);
        }
    }
}