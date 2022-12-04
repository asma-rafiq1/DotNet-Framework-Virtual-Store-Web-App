using AutoMapper;
using Brandedkart.DTO.Customer;
using Brandedkart.DTO.Product;
using Brandedkart.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandedKart.DAL
{
    internal class DataMapper
    {
        public static class Mapping
        {
            private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    // This line ensures that internal properties are also mapped over.
                    cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                    cfg.AddProfile<ClassMapper>();
                });
                var mapper = config.CreateMapper();
                return mapper;
            });

            public static IMapper Mapper => Lazy.Value;
        }
        class ClassMapper : Profile
        {
            public ClassMapper()
            {
                //Customer Mapping
                CreateMap<CustomerDTO, Customer>();
                CreateMap<Customer, CustomerDTO>();

                //Admin Mapping
                CreateMap<AdminDTO, Admin>();
                CreateMap<Admin, AdminDTO>();

                //Product Mapping
                CreateMap<Product, ProductDTO>();
                CreateMap<ProductDTO, Product>();

                //Cart Product Mapping
                CreateMap<CartDTO, Product>();
                CreateMap<Product, CartDTO>();

            }
        }
    }
}

