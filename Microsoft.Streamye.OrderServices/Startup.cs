using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Streamye.Commons.CommonResults;
using Microsoft.Streamye.Commons.Exceptions.Handlers;
using Microsoft.Streamye.Cores.Registry.Extensions;
using Microsoft.Streamye.OrderServices.Context;
using Microsoft.Streamye.OrderServices.Repositories;
using Microsoft.Streamye.OrderServices.Repositories.Impl;
using Microsoft.Streamye.OrderServices.Services;
using Microsoft.Streamye.OrderServices.Services.Impl;
using Newtonsoft.Json.Serialization;

namespace Microsoft.Streamye.OrderServices
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            //db context
            services.AddDbContext<OrderContext>(optionsBuilder =>
            {
                optionsBuilder.UseMySql(configuration.GetConnectionString("DefaultConnection"));
            });

            //local services
            AddLocalServices(services, configuration);

            //registry center
            services.AddConsulServiceRegistry(options =>
            {
                options.ServiceId = Guid.NewGuid().ToString();
                options.ServiceName = configuration["ConsulRegistry:ServiceName"];
                options.ServiceAddress = Environment.GetEnvironmentVariable("ASPNETCORE_URLS") != null
                    ? Environment.GetEnvironmentVariable("ASPNETCORE_URLS")
                    : configuration["ConsulRegistry:ServiceAddress"];
                options.HealthCheckAddress = configuration["ConsulRegistry:HealthCheckAddress"];
                options.RegistryAddress = configuration["ConsulRegistry:RegistryAddress"]; //"http://localhost:8500";
            });

            //controllers
            services.AddControllers(options =>
            {
                options.Filters.Add<MicroServiceCommonResultHandler>(1);
                options.Filters.Add<BizExceptionHandler>(2);
            }).AddNewtonsoftJson(option =>
            {
                //防止将大写转换为小写
                option.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            //cap
            services.AddCap(x =>
            {
                //如果错误的message
                //x.UseInMemoryStorage();

                //当然放在内存不太合适
                x.UseEntityFramework<OrderContext>();
                x.UseMySql(configuration.GetConnectionString("DefaultConnection"));
                // rabbitmq
                x.UseRabbitMQ(rb =>
                {
                    rb.HostName = configuration.GetSection("Cap").GetValue<string>("RabbitMQ.HostName");
                    rb.UserName = configuration.GetSection("Cap").GetValue<string>("RabbitMQ.UserName");
                    rb.Password = configuration.GetSection("Cap").GetValue<string>("RabbitMQ.Password");
                    rb.Port = configuration.GetSection("Cap").GetValue<int>("RabbitMQ.Port");
                    rb.VirtualHost = configuration.GetSection("Cap").GetValue<string>("RabbitMQ.VirtualHost");
                });

                // dashboard ，方便人工处理
                x.UseDashboard();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private void AddLocalServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderItemService, OrderItemService>();

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        }
    }
}