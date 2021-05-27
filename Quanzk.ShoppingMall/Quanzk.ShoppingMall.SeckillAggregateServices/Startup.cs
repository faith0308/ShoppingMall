using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Quanzk.Commons.Exceptions.Handlers;
using Quanzk.Commons.Filters;
using Quanzk.Commons.Users;
using Quanzk.Cores.IOC;
using Quanzk.Cores.IOC.Extentions;
using Quanzk.Cores.MicroClients.Extentions;
using Quanzk.Cores.Registry.Extentions;
using Quanzk.Cores.Swagger;
using Quanzk.Cores.Swagger.Extentions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.SeckillAggregateServices
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
        /// 
        /// </summary>
        /// <param name="services"></param>
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // 服务、仓储等依赖注入
            services.AddDataService();

            //// 添加微服务注册服务
            //services.AddServiceRegistry(options =>
            //{
            //    options.ServiceId = Guid.NewGuid().ToString();
            //    options.ServiceName = "SeckillAggregateServices";
            //    options.ServiceAddress = "https://localhost:5012/api/v1";
            //    options.HealthCheckAddress = "/HealthCheck";
            //    options.RegistryAddress = "http://localhost:8500";
            //});

            // 服务发现
            services.AddMicroClient(options =>
            {
                options.AssmelyName = "Quanzk.ShoppingMall.SeckillAggregateServices";
                options.dynamicMiddlewareOptions = mo =>
                {
                    mo.serviceDiscoveryOptions = sdo =>
                    {
                        sdo.DiscoveryAddress = "http://localhost:8500";
                    };
                };
            });

            // 设置跨域
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", builder =>
                {
                    builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
                });
            });

            // 添加身份认证
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
            {
                // 授权中心地址
                options.Authority = "https://localhost:5001";
                // api名称(项目具体名称)
                options.ApiName = "QuzkService";
                // 不使用https
                options.RequireHttpsMetadata = false;
            });

            // 添加控制器
            services.AddControllers(options =>
            {
                // 通用结果
                options.Filters.Add<CommonResultWrapperFilter>(1);
                // 通用异常
                options.Filters.Add<BusinessExceptionHandler>(2);
                // 自定义模型绑定(用户)
                options.ModelBinderProviders.Insert(0, new SysUserModelBinderProvider());
            }).AddNewtonsoftJson(options =>
            {
                // 防止将大写转换成小写
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

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
                        Title = "Quanzk.ShoppingMall.SeckillAggregateServices",
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="provider"></param>
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
            // 开启身份认证
            app.UseAuthentication();
            app.UseAuthorization();
            // 配置跨域
            app.UseCors("AllowSpecificOrigin");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseApiVersioning();
        }
    }
}
