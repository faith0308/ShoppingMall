using Quanzk.ShoppingMall.OrderServices.Models;
using Quanzk.ShoppingMall.OrderServices.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.OrderServices.Services
{
    /// <summary>
    /// 订单服务实现
    /// </summary>
    public class OrderServiceImpl : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderServiceImpl(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public bool Create(Order order)
        {
            return _orderRepository.Create(order);
        }

        public bool Delete(Order order)
        {
            return _orderRepository.Delete(order);
        }

        public Order GetOrderById(int id)
        {
            return _orderRepository.GetOrderById(id);
        }

        public IEnumerable<Order> GetOrders()
        {
            return _orderRepository.GetOrders();
        }

        public bool OrderExists(int id)
        {
            return _orderRepository.OrderExists(id);
        }

        public bool Update(Order order)
        {
            return _orderRepository.Update(order);
        }
    }
}
