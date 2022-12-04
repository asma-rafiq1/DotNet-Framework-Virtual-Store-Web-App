using BrandedKart.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brandedkart.DTO.User
{
    public class OrderDetailsDTO
    {
        public OrderDetailsDTO()
        {
            this.customerOrderProducts = new CartViewModel();
            this.address = new AddressDTO();
            this.transactionDetails = new TransactionDetailsDTO();
            this.orderdetails = new Order();
        }

        public CartViewModel customerOrderProducts { get; set; }
        public AddressDTO address { get; set; }

        public TransactionDetailsDTO transactionDetails { get; set; }

        public Order orderdetails { get; set; }

        public int Customer_Id { get; set; }

    }

    public enum PaymentType
    {
        CreditCard,
        DebitCard,
        PayPal
    }
}
