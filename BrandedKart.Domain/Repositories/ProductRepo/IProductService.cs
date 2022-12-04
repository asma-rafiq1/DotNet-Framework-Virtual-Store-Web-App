using Brandedkart.DTO.Product;
using BrandedKart.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandedKart.Domain.Repositories.ProductRepo
{
    public interface IProductService
    {
        void AddProduct(ProductDTO product);
        void UpdateProduct(ProductDTO product);
        void RemoveProduct(int productId);
        ProductDTO GetProductByID(int ProductID);
        IEnumerable<ProductDTO> GetCategoryProducts(int categoryId);
        IEnumerable<ProductDTO> GetProductsByName(string productName);
        IEnumerable<ProductDTO> GetTopProducts();

        IEnumerable<CartDTO> GetCartItems(Dictionary<int, int> productCartList);
        IEnumerable<ProductDTO> GetAllProducts();
        IEnumerable<CategoryDTO> GetCategories();
        CartViewModel CalculateBill(CartViewModel cartDetails);
        void Dispose();
    }
}
