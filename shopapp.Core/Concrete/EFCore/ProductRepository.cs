using Microsoft.EntityFrameworkCore;
using shopapp.Core.Abstract;
using shopapp.Core.Concrete.EFCore.Base;
using shopapp.Domain.Context;
using shopapp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace shopapp.Core.Concrete.EFCore
{
    public class ProductRepository : BaseRepository<Product, ShopDBContext>, IProductRepository
    {
        public Product GetByIdWithCategories(int id)
        {
            using (var context = new ShopDBContext())
            {
                return context.Products.Where(i => i.Id == id).Include(x => x.ProductCategories).ThenInclude(x => x.Category).FirstOrDefault();
            }
        }

        public int GetCountByCategory(string category)
        {
            using (var context = new ShopDBContext())
            {
                var products = context.Products.AsQueryable();

                if (!string.IsNullOrEmpty(category))
                {
                    products = products.Include(x => x.ProductCategories).ThenInclude(x => x.Category).Where(x => x.ProductCategories.Any(a => a.Category.Name.ToLower() == category.ToLower()));
                }

                return products.Count();
            }
        }

        public Product GetProductDetails(int id)
        {
            using (var context = new ShopDBContext())
            {
                return context.Products.Where(i => i.Id == id).Include(i => i.ProductCategories).ThenInclude(i => i.Category).FirstOrDefault();
            }
        }

        public List<Product> GetProductsByCategory(string category, int page, int pageSize)
        {
            using (var context = new ShopDBContext())
            {
                var products = context.Products.AsQueryable();

                if (!string.IsNullOrEmpty(category))
                {
                    products = products.Include(x => x.ProductCategories).ThenInclude(x => x.Category).Where(x => x.ProductCategories.Any(a => a.Category.Name.ToLower() == category.ToLower()));
                }

                return products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public void Update(Product entity, int[] categoryIds)
        {
            using (var context = new ShopDBContext())
            {
                var product = context.Products.Include(i => i.ProductCategories).FirstOrDefault(i => i.Id == entity.Id);
                if (product != null)
                {
                    product.Name = entity.Name;
                    product.Description = entity.Description;
                    product.ImageUrl = entity.ImageUrl;
                    product.Price = entity.Price;

                    product.ProductCategories = categoryIds.Select(catid => new ProductCategory()
                    {
                        CategoryId = catid,
                        ProductId = entity.Id
                    }).ToList();

                    context.SaveChanges();
                }
            }
        }
    }
}
