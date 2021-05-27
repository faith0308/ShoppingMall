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
            // IOC������ע��dbcontext 
            services.AddDbContext<IdentityServerUserDbContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("MysqlConnection"), MySqlServerVersion.LatestSupportedServerVersion);
            });

            // ��Config���õ����ݳ־û���Sqlserver
            // ��Դ�ͻ��˳־û�����
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            var connectionString = Configuration.GetConnectionString("MysqlConnection");
            // ���IdentityServer4
            services.AddIdentityServer()
                // ���֤����ܷ�ʽ��ִ�и÷����������ж�tempkey.rsa֤���ļ��Ƿ���ڣ���������ڵĻ����ʹ���һ���µ�tempkey.rsa֤���ļ���������ڵĻ�����ʹ�ô�֤���ļ���
                .AddDeveloperSigningCredential() // ����ǩ��֤��  ��������Ҫ������ʽ֤��
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
                // �洢API��Դ
                .AddInMemoryApiScopes(Config.GetApiScopes())
                // �洢�ͻ���ģʽ
                .AddInMemoryClients(Config.GetClients())
                // �����û�
                .AddTestUsers(Config.GetUsers())
                // openId���
                .AddInMemoryIdentityResources(Config.Ids);
            //�Զ����û�У��
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

            //// ��ʼ������
            //InitializeDatabase(app);
            //// ��ʼ���û�����
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

            // ʹ��Identity Server 4
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

        #region ��config�����ݴ洢����
        /// <summary>
        /// ��config�����ݴ洢����
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

        #region ���û������ݴ洢����
        /// <summary>
        /// ���û������ݴ洢����
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
