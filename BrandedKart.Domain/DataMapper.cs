using AutoMapper;
using Brandedkart.DTO.Customer;
using Brandedkart.DTO.Product;
using Brandedkart.DTO.User;
using BrandedKart.DAL;
using BrandedKart.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order = BrandedKart.DAL.Order;

namespace BrandedKart.Domain
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
                //Product
                
                CreateMap<ProductDTO, Product>();
                CreateMap<Product, ProductDTO>();
                CreateMap<CartViewModel,Order>()
                    .ForMember(cartDetails => cartDetails.TotalAmount, order => order.MapFrom(values => values.Total));
                    
                //Category
                CreateMap<Category, CategoryDTO>();
                CreateMap<AddressDTO, Address>();
                CreateMap<TransactionDetailsDTO, TransactionDetail>();
                CreateMap<Address, AddressDTO>();
                CreateMap<TransactionDetail, TransactionDetailsDTO>();

            }
        }
    }
}

