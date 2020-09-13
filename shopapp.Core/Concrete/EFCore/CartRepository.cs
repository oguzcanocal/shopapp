using Microsoft.EntityFrameworkCore;
using shopapp.Core.Abstract;
using shopapp.Core.Concrete.EFCore.Base;
using shopapp.Domain.Context;
using shopapp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace shopapp.Core.Concrete.EFCore
{
    public class CartRepository : BaseRepository<Cart, ShopDBContext>, ICartRepository
    {
        public void ClearCart(string cartId)
        {
            using (var context = new ShopDBContext())
            {
                var cmd = @"delete from CartItem where CartId=@p0";
                context.Database.ExecuteSqlCommand(cmd, cartId);
            }
        }

        public void DeleteFromCart(int cartId, int productId)
        {
            using (var context = new ShopDBContext())
            {
                var cmd = @"delete from CartItem where CartId=@p0 And ProductId=@p1";
                context.Database.ExecuteSqlCommand(cmd, cartId, productId);
            }
        }

        public Cart getByUserId(string userid)
        {
            using (var context = new ShopDBContext())
            {
                return context.Carts.Include(x => x.CartItems).ThenInclude(x => x.Product).FirstOrDefault(x => x.UserId == userid);
            }
        }
    }
}
