using Microsoft.EntityFrameworkCore;
using Quanzk.ShoppingMall.OrderServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.OrderServices.Context
{
    /// <summary>
    /// 订单服务上下文
    /// </summary>
    public class OrderContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {

        }

        /// <summary>
        /// 订单集合
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        ///  订单项集合
        /// </summary>
        public DbSet<OrderItem> OrderItems { set; get; }
    }
}
