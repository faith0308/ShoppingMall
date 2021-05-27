using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Quanzk.Cores.IOC;
using Quanzk.Cores.IOC.Extentions;
using Quanzk.Cores.Registry.Extentions;
using Quanzk.Cores.Swagger.Extentions;
using Quanzk.ShoppingMall.ProductServices.Context;
using System;
using System.IO;

namespace Quanzk.ShoppingMall.ProductServices
{
    /// <summary>
    /// ��������
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

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // IOC������ע��dbcontext
            services.AddDbContext<ProductContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("MysqlConnection"), MySqlServerVersion.LatestSupportedServerVersion);
            });

            // ���񡢲ִ�������ע��
            services.AddDataService();

            // ���΢����ע�����
            services.AddServiceRegistry(options =>
            {
                options.ServiceId = Guid.NewGuid().ToString();
                options.ServiceName = "ProductServices";
                options.ServiceAddress = "https://localhost:5002/api/v1";
                options.HealthCheckAddress = "/HealthCheck";
                options.RegistryAddress = "http://localhost:8500";
            });

            //������
            services.AddControllers();

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
                        Title = "Quanzk.ShoppingMall.ProductServices",
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
