using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brandedkart.DTO.User
{
    public class Order
    {
        public int OrderID { get; set; }
        public byte OrderStatus { get; set; }
        public decimal TotalAmount { get; set; }
        public System.DateTime Order_Date { get; set; }
        public System.DateTime Due_Date { get; set; }
        public float DiscountByCoupon { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public PaymentType Payment_Type { get; set; }
    }
}
