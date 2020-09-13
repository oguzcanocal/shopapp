using System;
using System.Collections.Generic;
using System.Text;

namespace shopapp.Common.Dto
{
    public class ProductCategoryDto
    {
        public int CategoryId { get; set; }
        public CategoryDto Category { get; set; }
        public int ProductId { get; set; }
        public ProductDto Product { get; set; }
    }
}
