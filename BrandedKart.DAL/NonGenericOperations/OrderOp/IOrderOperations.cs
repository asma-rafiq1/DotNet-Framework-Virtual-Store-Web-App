using Brandedkart.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandedKart.DAL.NonGenericOperations.OrderOp
{
    public interface IOrderOperations
    {
         Order PlaceOrder(Order order, IEnumerable<OrderDetail> orderedProducts);

        void Save();
    }
}
