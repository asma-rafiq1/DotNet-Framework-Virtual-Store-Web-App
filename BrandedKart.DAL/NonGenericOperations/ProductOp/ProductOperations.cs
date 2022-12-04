using AutoMapper;
using Brandedkart.DTO.Product;
using BrandedKart.UI.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BrandedKart.DAL.DataMapper;

namespace BrandedKart.DAL.NonGenericOperations.ProductOp
{
    public class ProductOperations : IProductOperations
    {
        private readonly BrandedKartEntities _context;
        readonly IMapper mapper = Mapping.Mapper;
        public ProductOperations(BrandedKartEntities context)
        {
            _context = context;
        }

        public IEnumerable<ProductDTO> GetProductsByName(string productName)
        {
            return mapper.Map<IEnumerable<ProductDTO>>(_context.Products
                .Where(product => product.Product_Name.Contains(productName)));
        }

        public ProductDTO GetProductById(int productId)
        {
            return mapper.Map<ProductDTO>(_context.Products
                .SingleOrDefault(product => product.Product_Id.Equals(productId)));

        }

        IEnumerable<ProductDTO> IProductOperations.GetTopProducts()
        {
            return mapper.Map<IEnumerable<ProductDTO>>(_context.Products.OrderBy(key => key.Product_Id).Skip(5));
        }

        public IEnumerable<ProductDTO> GetProductsByCategory(int categoryId)
        {
            var products = _context.Products.Where(product => product.Category_Id.Equals(categoryId)).ToList();
            return mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public IEnumerable<CartDTO> GetListOfProductsById(Dictionary<int, int> productCartList)
        {
            var productsCart = new List<CartDTO>();

            foreach (var productItem in productCartList)
            {
                var item = mapper.Map<CartDTO>(_context.Products.Where(prod => prod.Product_Id.Equals(productItem.Key)).Single());
                item.Product_quantity = productItem.Value;
                productsCart.Add(item);
            }

            return productsCart;

        }
    }
}
