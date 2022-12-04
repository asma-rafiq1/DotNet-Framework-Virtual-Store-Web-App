using AutoMapper;
using Brandedkart.DTO.Product;
using Brandedkart.DTO.User;
using BrandedKart.DAL;
using BrandedKart.DAL.GenericOperations;
using BrandedKart.DAL.NonGenericOperations.CustomerOp;
using BrandedKart.DAL.NonGenericOperations.ProductOp;
using BrandedKart.UI.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BrandedKart.Domain.DataMapper;

namespace BrandedKart.Domain.Repositories.ProductRepo
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork = new UnitOfWork();
        private readonly IProductOperations productOperations;
        IMapper mapper = Mapping.Mapper;
        private BrandedKartEntities _context = new BrandedKartEntities();

        public ProductService()
        {
            this.productOperations = new ProductOperations(_context);
        }

        public void AddProduct(ProductDTO product)
        {
            Product ProductDetails = mapper.Map<Product>(product);
            ProductDetails.Publish_Date = DateTime.Now;
            _unitOfWork.ProductRepo.Add(ProductDetails);
            _unitOfWork.Save();
        }

        public void UpdateProduct(ProductDTO product)
        {
            Product ProductDetails = mapper.Map<Product>(product);
            ProductDetails.Publish_Date = DateTime.Now;
            _unitOfWork.ProductRepo.Update(ProductDetails);
            _unitOfWork.Save();
        }

        public void RemoveProduct(int productId)
        {
            var product = new Product() { Product_Id = productId };
            _unitOfWork.ProductRepo.Delete(product);
            _unitOfWork.Save();
        }

        public IEnumerable<ProductDTO> GetAllProducts()
        {
            return Mapping.Mapper.Map<IEnumerable<ProductDTO>>(_unitOfWork.ProductRepo.GetAll());
        }

        public IEnumerable<ProductDTO> GetProductsByName(string productName)
        {
            return productOperations.GetProductsByName(productName);
        }
        public ProductDTO GetProductByID(int ProductID)
        {
            return mapper.Map<ProductDTO>(productOperations.GetProductById(ProductID));
        }

        IEnumerable<CartDTO> IProductService.GetCartItems(Dictionary<int, int> productCartList)
        {
            return productOperations.GetListOfProductsById(productCartList);
        }
        public IEnumerable<CategoryDTO> GetCategories()
        {
            IEnumerable<CategoryDTO> categories = mapper.Map<IEnumerable<CategoryDTO>>(_unitOfWork.CategoryRepo.GetAll());
            return categories;
        }

        public IEnumerable<ProductDTO> GetTopProducts()
        {
            return productOperations.GetTopProducts();
        }

        public IEnumerable<ProductDTO> GetCategoryProducts(int categoryId)
        {
            return productOperations.GetProductsByCategory(categoryId);
        }


        public CartViewModel CalculateBill(CartViewModel cartDetails)
        {

            decimal SubTotal = 0;
            const decimal Tax = 0.5M;
            decimal ShippingFee = 3;

            cartDetails.CartItems.ToList().ForEach(cartItem =>
            {
                SubTotal += (cartItem.Product_quantity * cartItem.Product_Price);

            });

            cartDetails.SubTotal = SubTotal;
            cartDetails.ShippingFee = ShippingFee;
            cartDetails.Tax = Tax;

            switch (SubTotal)
            {
                //case > 50:
                //    cartDetails.ShippingFee = 0;
                //    cartDetails.Total = Tax + SubTotal + ShippingFee;
                //    return cartDetails;
                default:
                    cartDetails.Total = Tax + SubTotal + ShippingFee;
                    return cartDetails;
            }

        }


        //MS Recommended Approach to dipose instead of finalizer(Destructor)

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
                    _unitOfWork.Dispose();
                    _context = null;
                }
            }
            this.isDisposed = true;
        }




    }
}
