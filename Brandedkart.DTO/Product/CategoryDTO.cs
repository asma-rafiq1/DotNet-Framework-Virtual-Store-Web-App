using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brandedkart.DTO.Product
{
    public class CategoryDTO
    {
        public int Category_ID { get; set; }
        public string Category_Title { get; set; }
        public string Category_Description { get; set; }
        public IEnumerable<ProductDTO> products { get; set; }
    }
}
