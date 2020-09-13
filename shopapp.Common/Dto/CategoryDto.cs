using System;
using System.Collections.Generic;
using System.Text;

namespace shopapp.Common.Dto
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<ProductCategoryDto> ProductCategories { get; set; }
    }
}
