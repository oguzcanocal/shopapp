using shopapp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace shoppapp.Services.Abstract
{
    public interface IOrderService
    {
        void Create(Order entity);
        void Save();
        List<Order> GetOrders(string userId);
    }
}
