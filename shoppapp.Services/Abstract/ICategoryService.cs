using shopapp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace shoppapp.Services.Abstract
{
    public interface ICategoryService
    {
        List<Category> GetAll();
        Category GetById(int id);
        Category GetByIdWithProducts(int id);
        void Create(Category entity);
        void Update(Category entity);
        void Delete(Category entity);
        void DeleteFromCategoty(int categoryId, int productId);
        void Save();
    }
}
