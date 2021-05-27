using Quanzk.ShoppingMall.OrderServices.Context;
using Quanzk.ShoppingMall.OrderServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.OrderServices.Repositories
{
    /// <summary>
    /// 订单项仓储实现
    /// </summary>
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly OrderContext _orderContext;
        public OrderItemRepository(OrderContext orderContext)
        {
            _orderContext = orderContext;
        }

        public bool Create(OrderItem OrderItem)
        {
            var flag = false;
            if (OrderItem != null)
            {
                _orderContext.OrderItems.Add(OrderItem);
                if (_orderContext.SaveChanges() > 0)
                {
                    flag = true;
                }
            }
            return flag;
        }

        public bool Delete(OrderItem OrderItem)
        {
            var flag = false;
            if (OrderItem != null)
            {
                var entity = _orderContext.OrderItems.Find(OrderItem.Id);
                if (entity != null)
                {
                    _orderContext.OrderItems.Remove(entity);
                    if (_orderContext.SaveChanges() > 0)
                    {
                        flag = true;
                    }
                }
            }
            return flag;
        }

        public OrderItem GetOrderItemById(int id)
        {
            var result = _orderContext.OrderItems.Find(id);
            return result ?? new OrderItem();
        }

        public IEnumerable<OrderItem> GetOrderItems()
        {
            var result = _orderContext.OrderItems.ToList();
            return result ?? new List<OrderItem>();
        }

        public bool OrderItemExists(int id)
        {
            return _orderContext.OrderItems.Any(p => p.Id == id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderItem"></param>
        /// <returns></returns>
        public bool Update(OrderItem orderItem)
        {
            var falg = false;
            if (orderItem != null && orderItem.Id > 0)
            {
                var entity = _orderContext.OrderItems.Find(orderItem.Id);
                if (entity != null)
                {
                    entity.Id = orderItem.OrderId;
                    entity.OrderSn = orderItem.OrderSn;
                    entity.ProductId = orderItem.ProductId;
                    entity.ProductUrl = orderItem.ProductUrl;
                    entity.ProductName = orderItem.ProductName;
                    entity.ItemPrice = orderItem.ItemPrice;
                    entity.ItemCount = orderItem.ItemCount;
                    entity.ItemTotalPrice = orderItem.ItemTotalPrice;
                    _orderContext.OrderItems.Update(entity);
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
