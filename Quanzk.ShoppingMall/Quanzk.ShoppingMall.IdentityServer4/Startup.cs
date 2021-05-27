using IdentityModel;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Quanzk.Cores.Swagger.Extentions;
using Quanzk.ShoppingMall.IdentityServer4.Configs;
using Quanzk.ShoppingMall.IdentityServer4.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.IdentityServer4
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // IOC容器中注入dbcontext 
            services.AddDbContext<IdentityServerUserDbContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("MysqlConnection"), MySqlServerVersion.LatestSupportedServerVersion);
            });

            // 将Config配置的数据持久化到Sqlserver
            // 资源客户端持久化操作
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            var connectionString = Configuration.GetConnectionString("MysqlConnection");
            // 添加IdentityServer4
            services.AddIdentityServer()
                // 添加证书加密方式，执行该方法，会先判断tempkey.rsa证书文件是否存在，如果不存在的话，就创建一个新的tempkey.rsa证书文件，如果存在的话，就使用此证书文件。
                .AddDeveloperSigningCredential() // 配置签署证书  生产环境要换成正式证书
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                    {
                        builder.UseMySql(connectionString, MySqlServerVersion.LatestSupportedServerVersion, sql =>
                        {
                            sql.MigrationsAssembly(migrationsAssembly);
                        });
                    };
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                    {
                        builder.UseMySql(connectionString, MySqlServerVersion.LatestSupportedServerVersion, sql =>
                        {
                            sql.MigrationsAssembly(migrationsAssembly);
                        });
                    };
                })
                // 存储API资源
                .AddInMemoryApiScopes(Config.GetApiScopes())
                // 存储客户端模式
                .AddInMemoryClients(Config.GetClients())
                // 测试用户
                .AddTestUsers(Config.GetUsers())
                // openId身份
                .AddInMemoryIdentityResources(Config.Ids);
            //自定义用户校验
            //.AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();

            services.AddControllersWithViews();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            //// 初始化数据
            //InitializeDatabase(app);
            //// 初始化用户数据
            //InitializeUserDatabase(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            // 使用Identity Server 4
            app.UseIdentityServer();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name:"default",
                    pattern:"{controller=Home}/{action=Index}/{id?}"
                    );
            });
        }

        #region 将config中数据存储起来
        /// <summary>
        /// 将config中数据存储起来
        /// </summary>
        /// <param name="app"></param>
        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<PersistedGrantDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetService<ConfigurationDbContext>();
                context.Database.Migrate();
                if (!context.Clients.Any())
                {
                    foreach (var client in Config.GetClients())
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }
                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in Config.Ids)
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
                if (!context.ApiResources.Any())
                {
                    foreach (var resource in Config.GetApiScopes())
                    {
                        context.ApiScopes.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
            }
        }
        #endregion

        #region 将用户中数据存储起来
        /// <summary>
        /// 将用户中数据存储起来
        /// </summary>
        /// <param name="app"></param>
        private void InitializeUserDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<IdentityServerUserDbContext>();
                context.Database.Migrate();

                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var idnetityUser = userManager.FindByNameAsync("faith").Result;
                if (idnetityUser == null)
                {
                    idnetityUser = new IdentityUser
                    {
                        UserName = "faith",
                        Email = "faith0308@163.com"
                    };
                    var result = userManager.CreateAsync(idnetityUser, "123456").Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                    result = userManager.AddClaimsAsync(idnetityUser, new Claim[] {
                        new Claim(JwtClaimTypes.Name, "faith"),
                        new Claim(JwtClaimTypes.GivenName, "faith"),
                        new Claim(JwtClaimTypes.FamilyName, "shenzhen"),
                        new Claim(JwtClaimTypes.Email, "faith0308@163.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://www.quanzk.com")
                    }).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                }
            }
        } 
        #endregion
    }
}
