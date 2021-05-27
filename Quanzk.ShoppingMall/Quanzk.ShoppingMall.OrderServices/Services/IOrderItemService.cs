using Quanzk.Cores.IOC;
using Quanzk.ShoppingMall.OrderServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.OrderServices.Services
{
    /// <summary>
    /// 订单项服务接口
    /// </summary>
    public interface IOrderItemService : IDependency
    {
        IEnumerable<OrderItem> GetOrderItems();
        OrderItem GetOrderItemById(int id);
        bool Create(OrderItem orderItem);
        bool Update(OrderItem orderItem);
        bool Delete(OrderItem orderItem);
        bool OrderItemExists(int id);
    }
}
