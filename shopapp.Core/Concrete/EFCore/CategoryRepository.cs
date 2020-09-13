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
    public class CategoryRepository : BaseRepository<Category, ShopDBContext>, ICategoryRepository
    {
        public void DeleteFromCategoty(int categoryId, int productId)
        {
            using (var context = new ShopDBContext())
            {
                var cmd = @"delete from ProductCategory where ProductId=@p0 And CategoryId = @p1";
                context.Database.ExecuteSqlCommand(cmd, productId, categoryId);
            }
        }

        public Category GetByIdWithProducts(int id)
        {
            using (var context = new ShopDBContext())
            {
                return context.Categories.Where(x => x.Id == id).Include(x => x.ProductCategories).ThenInclude(x => x.Product).FirstOrDefault();

            }
        }
    }
}
