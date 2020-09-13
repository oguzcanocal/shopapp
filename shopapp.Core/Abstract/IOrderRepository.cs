using shopapp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace shopapp.Core.Abstract
{
    public interface IOrderRepository:IRepository<Order>
    {
        List<Order> GetOrders(string userId);
    }
}
