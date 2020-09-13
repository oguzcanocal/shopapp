using shopapp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace shopapp.Core.Abstract
{
    public interface ICategoryRepository:IRepository<Category>
    {
        Category GetByIdWithProducts(int id);
        void DeleteFromCategoty(int categoryId, int productId);
    }
}
