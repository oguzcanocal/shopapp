using shopapp.Core.Abstract;
using shopapp.Domain.Entities;
using shoppapp.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace shoppapp.Services.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderManager(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public void Create(Order entity)
        {
            _orderRepository.Create(entity);
        }

        public List<Order> GetOrders(string userId)
        {
            return _orderRepository.GetOrders(userId);
        }

        public void Save()
        {
            _orderRepository.Save();
        }
    }
}
