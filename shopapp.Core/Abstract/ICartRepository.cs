using shopapp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace shopapp.Core.Abstract
{
    public interface ICartRepository:IRepository<Cart>
    {
        Cart getByUserId(string userid);
        void DeleteFromCart(int cartId, int productId);
        void ClearCart(string cartId);

    }
}
