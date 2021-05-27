using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Quanzk.Cores.IOC;
using Quanzk.Cores.IOC.Extentions;
using Quanzk.Cores.Registry.Extentions;
using Quanzk.Cores.Swagger.Extentions;
using Quanzk.ShoppingMall.PaymentServices.Context;
using System;
using System.IO;

namespace Quanzk.ShoppingMall.PaymentServices
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
            // IOC容器中注入dbcontext
            services.AddDbContext<PaymentContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("MysqlConnection"), MySqlServerVersion.LatestSupportedServerVersion);
            });

            // 服务、仓储等依赖注入
            services.AddDataService();

            // 添加微服务注册服务
            services.AddServiceRegistry(options =>
            {
                options.ServiceId = Guid.NewGuid().ToString();
                options.ServiceName = "PaymentServices";
                options.ServiceAddress = "https://localhost:5006/api/v1";
                options.HealthCheckAddress = "/HealthCheck";
                options.RegistryAddress = "http://localhost:8500";
            });

            //控制器
            services.AddControllers();

            /* 
             * 合并 Swagger需要的xml文件
             * 需要显示所有的接口方法和实体对象的注释，因此，要对各项目产生的xml文件进行合并。
             * 此操作，文件多时，项目启动会比较慢
             */
            services.AddXmlFileMerge();

            // 接入Swagger服务
            services.AddSwaggerGen(c =>
            {
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.SwaggerDoc(description.GroupName, new OpenApiInfo
                    {
                        Title = "Quanzk.ShoppingMall.PaymentServices",
                        Version = description.ApiVersion.ToString(),
                        Description = "多版本管理【请点右上角版本切换】"
                    });
                }
                // 启动注释功能
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "Main.xml");
                c.IncludeXmlComments(xmlPath);
            });

            // Api的版本控制
            services.AddApiVersioning(options =>
            {
                // 标记当客户端没有指定版本号的时候，是否使用默认版本号
                options.AssumeDefaultVersionWhenUnspecified = true;
                // HTTP Header报头传递
                //options.ApiVersionReader = new HeaderApiVersionReader("api-version");
                // 可选配置，设置为true时，header返回版本信息
                options.ReportApiVersions = true;
                // 默认版本
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });
            services.AddVersionedApiExplorer(option =>
            {
                // api 版本组名格式 
                option.GroupNameFormat = "'v'VVVV";
                // 是否提供API版本服务
                option.AssumeDefaultVersionWhenUnspecified = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseApiVersioning();
        }
    }
}
