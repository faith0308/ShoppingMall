﻿using Quanzk.Cores.IOC;
using Quanzk.ShoppingMall.OrderServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.OrderServices.Repositories
{
    /// <summary>
    /// 订单仓储接口
    /// </summary>
    public interface IOrderRepository : IDependency
    {
        IEnumerable<Order> GetOrders();
        Order GetOrderById(int id);
        bool Create(Order order);
        bool Update(Order order);
        bool Delete(Order order);
        bool OrderExists(int id);
    }
}
