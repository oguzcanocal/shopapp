using shopapp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace shoppapp.Services.Abstract
{
    public interface IProductService
    {
        Product GetById(int id);
        Product GetProductDetails(int id);
        List<Product> GetProductsByCategory(string category, int page, int pageSize);
        List<Product> GetAll();
        void Create(Product entity);
        void Update(Product entity);
        void Delete(Product entity);
        int GetCountByCategory(string category);
        Product GetByIdWithCategories(int id);
        void Update(Product entity, int[] categoryIds);
        void Save();
    }
}
