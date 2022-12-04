


using Brandedkart.DTO.User;
using Order = BrandedKart.DAL.Order;

namespace BrandedKart.Domain.Repositories.OrderRepo
{
    public interface IOrderService
    {
        Order PlaceOrder(OrderDetailsDTO order);
        void Dispose();
    }
}