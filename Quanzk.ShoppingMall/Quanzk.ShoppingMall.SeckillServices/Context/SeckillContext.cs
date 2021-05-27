using Microsoft.EntityFrameworkCore;
using Quanzk.ShoppingMall.SeckillServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.SeckillServices.Context
{
    /// <summary>
    /// 秒杀服务上下文
    /// </summary>
    public class SeckillContext : DbContext
    {
        public SeckillContext(DbContextOptions<SeckillContext> options) : base(options)
        {

        }

        /// <summary>
        /// 秒杀集合
        /// </summary>
        public DbSet<Seckill> Seckills { get; set; }
        /// <summary>
        /// 秒杀记录集合
        /// </summary>
        public DbSet<SeckillRecord> SeckillRecords { get; set; }
        /// <summary>
        /// 秒杀时间集合
        /// </summary>
        public DbSet<SeckillTimeModel> seckillTimeModels { get; set; }
    }
}
