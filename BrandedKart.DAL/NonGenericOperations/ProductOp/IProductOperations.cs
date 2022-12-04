using Brandedkart.DTO.Product;
using System.Collections.Generic;
using System.Linq;

namespace BrandedKart.DAL.NonGenericOperations.ProductOp
{
    public interface IProductOperations
    {
        IEnumerable<ProductDTO> GetProductsByName(string productName);
        ProductDTO GetProductById(int productId);
        IEnumerable<ProductDTO> GetTopProducts();
        IEnumerable<ProductDTO> GetProductsByCategory(int categoryId);
        IEnumerable<CartDTO> GetListOfProductsById(Dictionary<int, int> productCartList);
    }
}