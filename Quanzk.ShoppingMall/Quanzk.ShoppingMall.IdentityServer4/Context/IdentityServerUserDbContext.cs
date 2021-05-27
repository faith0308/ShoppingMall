using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.IdentityServer4.Context
{
    /// <summary>
    /// 
    /// </summary>
    public class IdentityServerUserDbContext : IdentityDbContext<IdentityUser>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public IdentityServerUserDbContext(DbContextOptions<IdentityServerUserDbContext> options) : base(options)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
