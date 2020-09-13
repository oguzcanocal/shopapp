using shopapp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace shopapp.Common.Dto
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}
