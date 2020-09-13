using shopapp.Core.Abstract;
using shopapp.Core.Concrete.EFCore;
using shopapp.Domain.Entities;
using shoppapp.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace shoppapp.Services.Concrete
{
    public class CartManager : ICartService
    {

        private readonly ICartRepository _cartRepository;

        public CartManager(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public void AddToCart(string userId, int productId, int quantity)
        {
            var cart = GetCartByUserId(userId);
            if (cart != null)//daha önceden kart oluşturulmuş mu kontrol ediyorum.
            {
                var index = cart.CartItems.FindIndex(i => i.ProductId == productId);//eklenen ürün cart'a daha önce eklenmiş mi kontrol ediyorum.

                if (index < 0)//ürün yoksa yeni ürün oluşturuyorum
                {
                    cart.CartItems.Add(new CartItem()
                    {
                        ProductId = productId,
                        Quantity = quantity,
                        CartId = cart.Id
                    });
                }
                else
                {
                    cart.CartItems[index].Quantity += quantity; //ürün varsa quantity'i kullanıcının girdiği quantity kadar artırıyorum.
                }

                _cartRepository.Update(cart);

            }
        }

        public void ClearCart(string cartId)
        {
            _cartRepository.ClearCart(cartId);
        }

        public void DeleteFromCart(string userId, int productId)
        {
            var cart = GetCartByUserId(userId);
            if (cart != null)
            {
                _cartRepository.DeleteFromCart(cart.Id, productId);
            }
        }

        public Cart GetCartByUserId(string userid)
        {
            return _cartRepository.getByUserId(userid);
        }

        public void InitializeCart(string userid)
        {
            _cartRepository.Create(new Cart() { UserId = userid });
        }

        public void Save()
        {
            _cartRepository.Save();
        }
    }
}
