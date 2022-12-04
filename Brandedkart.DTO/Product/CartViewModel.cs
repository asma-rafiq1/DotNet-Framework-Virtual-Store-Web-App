using Brandedkart.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrandedKart.UI.ViewModel
{
    public class CartViewModel
    {
        public CartViewModel()
        {
            CartItems = new List<CartDTO>();
        }

        public IEnumerable<CartDTO> CartItems { get; set; }

        public Decimal SubTotal { get; set; }

        public decimal ShippingFee { get; set; }

        public decimal Tax { get; set; }

        public decimal Total { get; set; }


    }
}