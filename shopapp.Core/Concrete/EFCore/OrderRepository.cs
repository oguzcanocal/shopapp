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
    public class OrderRepository : BaseRepository<Order, ShopDBContext>, IOrderRepository
    {
        public List<Order> GetOrders(string userId)
        {
            using (var context = new ShopDBContext())
            {
                var orders = context.Orders.Include(x => x.OrderItems).ThenInclude(x => x.Product).AsQueryable();

                if (!string.IsNullOrEmpty(userId))
                {
                    orders = orders.Where(x => x.UserId == userId);
                }
                return orders.ToList();
            }
        }
    }
}
