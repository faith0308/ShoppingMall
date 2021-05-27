using Microsoft.EntityFrameworkCore;
using Quanzk.ShoppingMall.UserServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.UserServices.Context
{
    /// <summary>
    /// 用户服务上下文
    /// </summary>
    public class UserContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }

        /// <summary>
        /// 用户集合
        /// </summary>
        public DbSet<User> Users { get; set; }
    }
}
