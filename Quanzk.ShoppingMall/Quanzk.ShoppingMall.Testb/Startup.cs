using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using Quanzk.Commons.Exceptions.Handlers;
using Quanzk.Commons.Filters;
using Quanzk.Cores.IOC.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.Testb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            // 服务、仓储等依赖注入
            services.AddDataService();

            // 添加身份验证 (使用cookie来本地登录用户（通过“Cookies”作为DefaultScheme），并且将DefaultChallengeScheme设置为oidc。因为当我们需要用户登录时，我们将使用OpenID Connect协议。)
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                // openid connect
                options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("Cookies")
            .AddOpenIdConnect("oidc", options =>
            {
                // 生成idtoken
                options.Authority = "https://localhost:5014";
                options.RequireHttpsMetadata = false;
                options.ClientId = "client-code";
                options.ClientSecret = "secret";
                options.ResponseType = "code";
                // 用于将来自IdentityServer的令牌保留在cookie中
                options.SaveTokens = true;

                // 添加授权访问api的支持(access_token)
                options.Scope.Add("UserServices");
                options.Scope.Add("offline_access");
            });

            //控制器 MVC 内部控制
            services.AddControllersWithViews(options =>
            {
                // 通用结果
                // options.Filters.Add<CommonResultWrapperFilter>(1);
                // 通用异常
                options.Filters.Add<BusinessExceptionHandler>(2);
            }).AddNewtonsoftJson(options =>
            {
                // 防止将大写变成小写
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}").RequireAuthorization();
            });
        }
    }
}
