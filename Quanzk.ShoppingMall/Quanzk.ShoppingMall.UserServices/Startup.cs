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
    /// ��������
    /// https://localhost:5001/api/v1/Users/GetUsers  �����ַ
    /// Identity Server4 Ĭ�Ϸ��ʵ�ַ��/.well-known/openid-configuration
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// ���������캯��
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
        /// ���÷���
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // IOC������ע��dbcontext
            services.AddDbContext<UserContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("MysqlConnection"), MySqlServerVersion.LatestSupportedServerVersion);
            });

            // ���񡢲ִ�������ע��
            services.AddDataService();

            // ���΢����ע�����
            services.AddServiceRegistry(options =>
            {
                options.ServiceId = Guid.NewGuid().ToString();
                options.ServiceName = Configuration.GetSection("AppSettings:ServiceName").Value;
                options.ServiceAddress = Configuration.GetSection("AppSettings:ServiceAddress").Value;
                options.HealthCheckAddress = Configuration.GetSection("AppSettings:HealthCheckAddress").Value;
                options.RegistryAddress = Configuration.GetSection("AppSettings:RegistryAddress").Value;
            });

            //������ MVC �ڲ�����
            services.AddControllers(options =>
            {
                // ͨ�ý��
                options.Filters.Add<CommonResultWrapperFilter>(1);
                // ͨ���쳣
                options.Filters.Add<BusinessExceptionHandler>(2);
                // �Զ���ģ�Ͱ�(�û�)
                options.ModelBinderProviders.Add(new SysUserModelBinderProvider());
            }).AddNewtonsoftJson(options =>
            {
                // ��ֹ����д���Сд
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
            // ����Ǩ��ʱʹ��
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            // �°�API��֤
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddJwtBearer(IdentityServerAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = "https://localhost:5014";// ��Ȩ���ĵ�ַ
                    options.RequireHttpsMetadata = false;// ��ʹ��Https
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });
            // �°�API��Ȩ
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "UserServices");
                });
            });

            //// ���IdentityServer4
            //services.AddIdentityServer()
            //    // ���֤����ܷ�ʽ��ִ�и÷����������ж�tempkey.rsa֤���ļ��Ƿ���ڣ���������ڵĻ����ʹ���һ���µ�tempkey.rsa֤���ļ���������ڵĻ�����ʹ�ô�֤���ļ���
            //    .AddDeveloperSigningCredential() // ����ǩ��֤��  ��������Ҫ������ʽ֤��
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
            //    // ���ܱ�����Api��Դ��ӵ��ڴ���
            //    .AddInMemoryApiResources(Config.GetApiResources())
            //    // �ͻ���������ӵ��ڴ���
            //    .AddInMemoryClients(Config.GetClients())
            //    //.AddTestUsers(Config.GetUsers())
            //    //�Զ����û�У��
            //    .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();

            /* 
             * �ϲ� Swagger��Ҫ��xml�ļ�
             * ��Ҫ��ʾ���еĽӿڷ�����ʵ������ע�ͣ���ˣ�Ҫ�Ը���Ŀ������xml�ļ����кϲ���
             * �˲������ļ���ʱ����Ŀ������Ƚ���
             */
            services.AddXmlFileMerge();

            // ����Swagger����
            services.AddSwaggerGen(c =>
            {
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.SwaggerDoc(description.GroupName, new OpenApiInfo
                    {
                        Title = "Quanzk.ShoppingMall.UserServices",
                        Version = description.ApiVersion.ToString(),
                        Description = "��汾����������Ͻǰ汾�л���"
                    });
                }
                // ����ע�͹���
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                //var xmlPath = Path.Combine(basePath, "Quanzk.ShoppingMall.UserServices.xml");
                var xmlPath = Path.Combine(basePath, "Main.xml");
                c.IncludeXmlComments(xmlPath);
            });

            // Api�İ汾����
            services.AddApiVersioning(options =>
            {
                // ��ǵ��ͻ���û��ָ���汾�ŵ�ʱ���Ƿ�ʹ��Ĭ�ϰ汾��
                options.AssumeDefaultVersionWhenUnspecified = true;
                // HTTP Header��ͷ����
                //options.ApiVersionReader = new HeaderApiVersionReader("api-version");
                // ��ѡ���ã�����Ϊtrueʱ��header���ذ汾��Ϣ
                options.ReportApiVersions = true;
                // Ĭ�ϰ汾
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });
            services.AddVersionedApiExplorer(option =>
            {
                // api �汾������ʽ 
                option.GroupNameFormat = "'v'VVVV";
                // �Ƿ��ṩAPI�汾����
                option.AssumeDefaultVersionWhenUnspecified = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// ����HTTP����ܵ�
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="provider"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            // ����ܵ��쳣����
            app.UsePipelineExcetion();
            // �����ж�
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // �������� ����swagger�ӿ��ĵ�
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
            // ʹ��IdentityServer
            //app.UseIdentityServer();
            // ·������
            app.UseRouting();
            // ���������֤(��Ȩ����·��֮����Ȩ֮ǰ)
            app.UseAuthentication();
            // ʹ����Ȩ
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            // ����汾����
            app.UseApiVersioning();
            // ��config���ݴ洢����  ������ݿ���뿼��ɾ����API�ĵ���
            // InitializeDatabase(app);
        }

        #region ��config�����ݴ洢����
        /// <summary>
        /// ��config�����ݴ洢����
        /// Identity Server 4 ���ݱ�Ǩ��
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
