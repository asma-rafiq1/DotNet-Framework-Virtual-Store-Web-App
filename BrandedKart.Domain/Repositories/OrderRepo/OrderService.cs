using AutoMapper;
using Brandedkart.DTO.Product;
using Brandedkart.DTO.User;
using BrandedKart.DAL;
using BrandedKart.DAL.GenericOperations;
using BrandedKart.DAL.NonGenericOperations.CustomerOp;
using BrandedKart.DAL.NonGenericOperations.OrderOp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrandedKart.Domain;
using static BrandedKart.Domain.DataMapper;
using Order = BrandedKart.DAL.Order;

namespace BrandedKart.Domain.Repositories.OrderRepo
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork unitofwork;
        private readonly IOrderOperations _order;
        readonly IMapper mapper = Mapping.Mapper;
        private BrandedKartEntities _context = new BrandedKartEntities();

        public OrderService()
        {
            this.unitofwork = new UnitOfWork();
            _order = new OrderOperations(_context);
        }

        public Order PlaceOrder(OrderDetailsDTO order)
        {
            Order customerOrder = new Order();

            customerOrder = mapper.Map<Order>(order.customerOrderProducts);
            customerOrder.Addresses.Add(mapper.Map<Address>(order.address));
            customerOrder.TransactionDetails.Add(mapper.Map<TransactionDetail>(order.transactionDetails));

            //On DB Engine Side By default
            //customerOrder.OrderStatus = 0;
            //customerOrder.Order_Date = DateTime.Now;
            customerOrder.CustomerID = order.Customer_Id;

            switch (order.orderdetails.Payment_Type)
            {
                case PaymentType.CreditCard:
                    customerOrder.Payment_Type = (int)PaymentType.CreditCard;
                    break;
                case PaymentType.DebitCard:
                    customerOrder.Payment_Type = (int)PaymentType.DebitCard;
                    break;
                default:
                    throw new Exception();

            }
            var orderedProducts = order.customerOrderProducts.CartItems.Select(product => new OrderDetail
            {
                ProductID = product.Product_ID,
                Unit_Price = product.Product_Price,
                Quantity = product.Product_quantity

            }).ToList();

            var orderdetails = _order.PlaceOrder(customerOrder, orderedProducts);
            _order.Save();
            return orderdetails;
        }

        private bool isDisposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.isDisposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                    unitofwork.Dispose();
                    _context = null;
                }
            }
            this.isDisposed = true;
        }


    }
}
