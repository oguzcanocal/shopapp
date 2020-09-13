using shopapp.Core.Abstract;
using shopapp.Domain.Entities;
using shoppapp.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace shoppapp.Services.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductManager(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public void Create(Product entity)
        {
            _productRepository.Create(entity);
        }

        public void Delete(Product entity)
        {
            _productRepository.Delete(entity);
        }

        public List<Product> GetAll()
        {
            return _productRepository.GetAll().ToList();
        }

        public Product GetById(int id)
        {
            return _productRepository.GetById(id);
        }

        public Product GetByIdWithCategories(int id)
        {
            return _productRepository.GetByIdWithCategories(id);
        }

        public int GetCountByCategory(string category)
        {
            return _productRepository.GetCountByCategory(category);
        }

        public Product GetProductDetails(int id)
        {
            return _productRepository.GetProductDetails(id);
        }

        public List<Product> GetProductsByCategory(string category, int page, int pageSize)
        {
            return _productRepository.GetProductsByCategory(category, page, pageSize);
        }

        public void Save()
        {
            _productRepository.Save();
        }

        public void Update(Product entity)
        {
            _productRepository.Update(entity);
        }

        public void Update(Product entity, int[] categoryIds)
        {
            _productRepository.Update(entity, categoryIds);
        }
    }
}
