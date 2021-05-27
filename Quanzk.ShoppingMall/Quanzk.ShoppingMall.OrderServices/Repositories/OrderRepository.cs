using Quanzk.ShoppingMall.OrderServices.Context;
using Quanzk.ShoppingMall.OrderServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.OrderServices.Repositories
{
    /// <summary>
    /// 订单仓储实现
    /// </summary>
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderContext _orderContext;
        public OrderRepository(OrderContext orderContext)
        {
            _orderContext = orderContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool Create(Order order)
        {
            var flag = false;
            if (order != null)
            {
                _orderContext.Orders.Add(order);
                if (_orderContext.SaveChanges() > 0)
                {
                    flag = true;
                }
            }
            return flag;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool Delete(Order order)
        {
            var flag = false;
            if (order != null)
            {
                var entity = _orderContext.Orders.Find(order.Id);
                if (entity != null)
                {
                    _orderContext.Orders.Remove(entity);
                    if (_orderContext.SaveChanges() > 0)
                    {
                        flag = true;
                    }
                }
            }
            return flag;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Order GetOrderById(int id)
        {
            var result = _orderContext.Orders.Find(id);
            return result ?? new Order();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Order> GetOrders()
        {
            var result = _orderContext.Orders.ToList();
            return result ?? new List<Order>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool OrderExists(int id)
        {
            return _orderContext.Orders.Any(p => p.Id == id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool Update(Order order)
        {
            var falg = false;
            if (order != null && order.Id > 0)
            {
                var entity = _orderContext.Orders.Find(order.Id);
                if (entity != null)
                {
                    entity.Id = order.Id;
                    entity.OrderAddress = order.OrderAddress;
                    entity.OrderFlag = order.OrderFlag;
                    entity.OrderName = order.OrderName;
                    entity.OrderRemark = order.OrderRemark;
                    entity.OrderSn = order.OrderSn;
                    entity.OrderStatus = order.OrderStatus;
                    entity.OrderTel = order.OrderTel;
                    entity.OrderTotalPrice = order.OrderTotalPrice;
                    entity.OrderType = order.OrderType;
                    entity.Paytime = order.Paytime;
                    entity.Updatetime = DateTime.Now;
                    entity.UserId = order.UserId;
                    _orderContext.Orders.Update(entity);
                    if (_orderContext.SaveChanges() > 0)
                    {
                        falg = true;
                    }
                }
            }
            return falg;
        }
    }
}
