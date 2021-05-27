using Microsoft.EntityFrameworkCore;
using Quanzk.ShoppingMall.ProductServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.ProductServices.Context
{
    /// <summary>
    /// 产品服务上下文
    /// </summary>
    public class ProductContext : DbContext
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options"></param>
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {

        }

        /// <summary>
        /// 商品集合
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// 商品图片集合
        /// </summary>
        public DbSet<ProductImage> ProductImages { get; set; }
    }
}
