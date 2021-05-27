using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Quanzk.Commons.Exceptions.Handlers;
using Quanzk.Commons.Filters;
using Quanzk.Cores.IOC.Extentions;

namespace Quanzk.ShoppingMall.Test
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
            // ���񡢲ִ�������ע��
            services.AddDataService();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Quanzk.ShoppingMall.Test", Version = "v1" });
            });

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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Quanzk.ShoppingMall.Test v1"));
            }

            app.UseHttpsRedirection();

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
