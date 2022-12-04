using BrandedKart.Domain.Repositories.AdminRepo;
using BrandedKart.Domain.Repositories.CustomerRepo;
using BrandedKart.Domain.Repositories.OrderRepo;
using BrandedKart.Domain.Repositories.ProductRepo;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace BrandedKart.UI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            container.RegisterType<ICustomerService, CustomerService>();
            container.RegisterType<IAdminService, AdminService>();
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<IOrderService, OrderService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}