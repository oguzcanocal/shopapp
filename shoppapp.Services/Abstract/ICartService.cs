using shopapp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace shoppapp.Services.Abstract
{
    public interface ICartService
    {
        void InitializeCart(string userid);
        Cart GetCartByUserId(string userid);
        void AddToCart(string userId, int productId, int quantity);
        void DeleteFromCart(string userId, int productId);
        void ClearCart(string cartId);
        void Save();


    }
}
