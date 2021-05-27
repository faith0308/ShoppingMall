using IdentityServer4.AccessTokenValidation;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
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
using Newtonsoft.Json.Serialization;
using Quanzk.Commons.Exceptions.Handlers;
using Quanzk.Commons.Filters;
using Quanzk.Commons.Middlewares;
using Quanzk.Commons.Users;
using Quanzk.Cores.IOC.Extentions;
using Quanzk.Cores.Registry.Extentions;
using Quanzk.Cores.Swagger.Extentions;
using Quanzk.ShoppingMall.UserServices.Configs;
using Quanzk.ShoppingMall.UserServices.Context;
using Quanzk.ShoppingMall.UserServices.IdentityServer;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Quanzk.ShoppingMall.UserServices
{
    /// <summary>
    /// 启动程序
    /// https://localhost:5001/api/v1/Users/GetUsers  请求地址
    /// Identity Server4 默认访问地址：/.well-known/openid-configuration
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 启动程序构造函数
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

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // IOC容器中注入dbcontext
            services.AddDbContext<UserContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("MysqlConnection"), MySqlServerVersion.LatestSupportedServerVersion);
            });

            // 服务、仓储等依赖注入
            services.AddDataService();

            // 添加微服务注册服务
            services.AddServiceRegistry(options =>
            {
                options.ServiceId = Guid.NewGuid().ToString();
                options.ServiceName = Configuration.GetSection("AppSettings:ServiceName").Value;
                options.ServiceAddress = Configuration.GetSection("AppSettings:ServiceAddress").Value;
                options.HealthCheckAddress = Configuration.GetSection("AppSettings:HealthCheckAddress").Value;
                options.RegistryAddress = Configuration.GetSection("AppSettings:RegistryAddress").Value;
            });

            //控制器 MVC 内部控制
            services.AddControllers(options =>
            {
                // 通用结果
                options.Filters.Add<CommonResultWrapperFilter>(1);
                // 通用异常
                options.Filters.Add<BusinessExceptionHandler>(2);
                // 自定义模型绑定(用户)
                options.ModelBinderProviders.Add(new SysUserModelBinderProvider());
            }).AddNewtonsoftJson(options =>
            {
                // 防止将大写变成小写
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
            // 数据迁移时使用
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            // 新版API认证
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddJwtBearer(IdentityServerAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = "https://localhost:5014";// 授权中心地址
                    options.RequireHttpsMetadata = false;// 不使用Https
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });
            // 新版API授权
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "UserServices");
                });
            });

            //// 添加IdentityServer4
            //services.AddIdentityServer()
            //    // 添加证书加密方式，执行该方法，会先判断tempkey.rsa证书文件是否存在，如果不存在的话，就创建一个新的tempkey.rsa证书文件，如果存在的话，就使用此证书文件。
            //    .AddDeveloperSigningCredential() // 配置签署证书  生产环境要换成正式证书
            //    .AddConfigurationStore(options =>
            //    {
            //        options.ConfigureDbContext = builder =>
            //        {
            //            builder.UseMySql(Configuration.GetConnectionString("MysqlConnection"), MySqlServerVersion.LatestSupportedServerVersion, sql =>
            //            {
            //                sql.MigrationsAssembly(migrationsAssembly);
            //            });
            //        };
            //    })
            //    // 把受保护的Api资源添加到内存中
            //    .AddInMemoryApiResources(Config.GetApiResources())
            //    // 客户端配置添加到内存中
            //    .AddInMemoryClients(Config.GetClients())
            //    //.AddTestUsers(Config.GetUsers())
            //    //自定义用户校验
            //    .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();

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
                        Title = "Quanzk.ShoppingMall.UserServices",
                        Version = description.ApiVersion.ToString(),
                        Description = "多版本管理【请点右上角版本切换】"
                    });
                }
                // 启动注释功能
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                //var xmlPath = Path.Combine(basePath, "Quanzk.ShoppingMall.UserServices.xml");
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
        /// <summary>
        /// 配置HTTP请求管道
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="provider"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            // 请求管道异常处理
            app.UsePipelineExcetion();
            // 环境判断
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // 开发环境 开放swagger接口文档
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
            // 使用IdentityServer
            //app.UseIdentityServer();
            // 路由配置
            app.UseRouting();
            // 开启身份验证(鉴权放在路由之后，授权之前)
            app.UseAuthentication();
            // 使用授权
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            // 服务版本控制
            app.UseApiVersioning();
            // 将config数据存储起来  填充数据库后，请考虑删除对API的调用
            // InitializeDatabase(app);
        }

        #region 将config中数据存储起来
        /// <summary>
        /// 将config中数据存储起来
        /// Identity Server 4 数据表迁移
        /// </summary>
        /// <param name="app"></param>
        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
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
                    foreach (var resource in Config.GetApiResources())
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
            }
        }
        #endregion
    }
}
