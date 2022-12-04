using BrandedKart.DAL.GenericOperations;
using BrandedKart.DAL.NonGenericOperations.CustomerOp;
using Brandedkart.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BrandedKart.DAL.DataMapper;
using Brandedkart.DTO.Product;

namespace BrandedKart.DAL.NonGenericOperations.OrderOp
{
    public class OrderOperations : IOrderOperations
    {
        private readonly BrandedKartEntities _context;

        public OrderOperations(BrandedKartEntities context)
        {
            this._context = context;
        }

        public Order PlaceOrder(Order order, IEnumerable<OrderDetail> orderedProducts)
        {

            var orderDetails = _context.Orders.Add(order);
            orderedProducts.ToList().ForEach(product => product.Order_ID = orderDetails.OrderID);
            _context.OrderDetails.AddRange(orderedProducts);
            return order;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
