using shopapp.Core.Abstract;
using shopapp.Domain.Entities;
using shoppapp.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace shoppapp.Services.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryManager(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }


        public void Create(Category entity)
        {
            _categoryRepository.Create(entity);
        }

        public void Delete(Category entity)
        {
            _categoryRepository.Delete(entity);
        }

        public void DeleteFromCategoty(int categoryId, int productId)
        {
            _categoryRepository.DeleteFromCategoty(categoryId, productId);
        }

        public List<Category> GetAll()
        {
            return _categoryRepository.GetAll();
        }

        public Category GetById(int id)
        {
            return _categoryRepository.GetById(id);
        }

        public Category GetByIdWithProducts(int id)
        {
            return _categoryRepository.GetByIdWithProducts(id);
        }

        public void Save()
        {
            _categoryRepository.Save();
        }

        public void Update(Category entity)
        {
            _categoryRepository.Update(entity);
        }
    }
}
