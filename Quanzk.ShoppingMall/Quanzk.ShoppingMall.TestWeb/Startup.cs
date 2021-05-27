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

namespace Quanzk.ShoppingMall.TestWeb
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

            // ���񡢲ִ�������ע��
            services.AddDataService();

            // ��������֤ (ʹ��cookie�����ص�¼�û���ͨ����Cookies����ΪDefaultScheme�������ҽ�DefaultChallengeScheme����Ϊoidc����Ϊ��������Ҫ�û���¼ʱ�����ǽ�ʹ��OpenID ConnectЭ�顣)
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                // openid connect
                options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("Cookies")
            .AddOpenIdConnect("oidc", options =>
            {
                // ����idtoken
                options.Authority = "https://localhost:5014";
                options.RequireHttpsMetadata = false;
                options.ClientId = "client-code";
                options.ClientSecret = "secret";
                options.ResponseType = "code";
                // ���ڽ�����IdentityServer�����Ʊ�����cookie��
                options.SaveTokens = true;

                // �����Ȩ����api��֧��(access_token)
                options.Scope.Add("UserServices");
                options.Scope.Add("offline_access");
            });

            //������ MVC �ڲ�����
            services.AddControllersWithViews(options =>
            {
                // ͨ�ý��
                options.Filters.Add<CommonResultWrapperFilter>(1);
                // ͨ���쳣
                options.Filters.Add<BusinessExceptionHandler>(2);
            }).AddNewtonsoftJson(options =>
            {
                // ��ֹ����д���Сд
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
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
                    name: "Default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
