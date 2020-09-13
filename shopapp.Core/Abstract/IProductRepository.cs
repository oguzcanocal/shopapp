﻿using shopapp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace shopapp.Core.Abstract
{
    public interface IProductRepository : IRepository<Product>
    {
        List<Product> GetProductsByCategory(string category, int page, int pageSize);
        Product GetProductDetails(int id);
        int GetCountByCategory(string category);
        Product GetByIdWithCategories(int id);
        void Update(Product entity, int[] categoryIds);
    }
}
