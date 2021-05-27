using Microsoft.EntityFrameworkCore;
using Quanzk.ShoppingMall.PaymentServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.PaymentServices.Context
{
    /// <summary>
    /// 支付服务上下文
    /// </summary>
    public class PaymentContext : DbContext
    {
        public PaymentContext(DbContextOptions<PaymentContext> options):base(options)
        {

        }

        /// <summary>
        /// 支付集合
        /// </summary>
        public DbSet<Payment> Payments { get; set; }
    }
}
