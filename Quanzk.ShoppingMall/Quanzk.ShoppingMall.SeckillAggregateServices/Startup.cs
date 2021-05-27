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
            // ���񡢲ִ�������ע��
            services.AddDataService();

            //// ���΢����ע�����
            //services.AddServiceRegistry(options =>
            //{
            //    options.ServiceId = Guid.NewGuid().ToString();
            //    options.ServiceName = "SeckillAggregateServices";
            //    options.ServiceAddress = "https://localhost:5012/api/v1";
            //    options.HealthCheckAddress = "/HealthCheck";
            //    options.RegistryAddress = "http://localhost:8500";
            //});

            // ������
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

            // ���ÿ���
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", builder =>
                {
                    builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
                });
            });

            // ��������֤
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
            {
                // ��Ȩ���ĵ�ַ
                options.Authority = "https://localhost:5001";
                // api����(��Ŀ��������)
                options.ApiName = "QuzkService";
                // ��ʹ��https
                options.RequireHttpsMetadata = false;
            });

            // ��ӿ�����
            services.AddControllers(options =>
            {
                // ͨ�ý��
                options.Filters.Add<CommonResultWrapperFilter>(1);
                // ͨ���쳣
                options.Filters.Add<BusinessExceptionHandler>(2);
                // �Զ���ģ�Ͱ�(�û�)
                options.ModelBinderProviders.Insert(0, new SysUserModelBinderProvider());
            }).AddNewtonsoftJson(options =>
            {
                // ��ֹ����дת����Сд
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

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
                        Title = "Quanzk.ShoppingMall.SeckillAggregateServices",
                        Version = description.ApiVersion.ToString(),
                        Description = "��汾����������Ͻǰ汾�л���"
                    });
                }
                // ����ע�͹���
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
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
            // ���������֤
            app.UseAuthentication();
            app.UseAuthorization();
            // ���ÿ���
            app.UseCors("AllowSpecificOrigin");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseApiVersioning();
        }
    }
}
