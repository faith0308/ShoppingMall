using Quanzk.ShoppingMall.OrderServices.Models;
using Quanzk.ShoppingMall.OrderServices.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.OrderServices.Services
{
    /// <summary>
    /// 订单项服务实现
    /// </summary>
    public class OrderItemServiceImpl : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderItemServiceImpl(IOrderItemRepository orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }

        public bool Create(OrderItem orderItem)
        {
            return _orderItemRepository.Create(orderItem);
        }

        public bool Delete(OrderItem orderItem)
        {
            return _orderItemRepository.Delete(orderItem);
        }

        public OrderItem GetOrderItemById(int id)
        {
            return _orderItemRepository.GetOrderItemById(id);
        }

        public IEnumerable<OrderItem> GetOrderItems()
        {
            return _orderItemRepository.GetOrderItems();
        }

        public bool OrderItemExists(int id)
        {
            return _orderItemRepository.OrderItemExists(id);
        }

        public bool Update(OrderItem orderItem)
        {
            return _orderItemRepository.Update(orderItem);
        }
    }
}
