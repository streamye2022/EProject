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
using Microsoft.Streamye.UserServices.Context;
using Microsoft.Streamye.UserServices.IdentityServer;
using Microsoft.Streamye.UserServices.Repositories;
using Microsoft.Streamye.UserServices.Repositories.Impl;
using Microsoft.Streamye.UserServices.Services;
using Microsoft.Streamye.UserServices.Services.Impl;
using Newtonsoft.Json.Serialization;

namespace Microsoft.Streamye.UserServices
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            //db 操作
            services.AddDbContext<UserContext>(optionsBuilder =>
            {
                optionsBuilder.UseMySql(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            
            //服务注册
            services.AddConsulServiceRegistry(options =>
            {
                options.ServiceId = Guid.NewGuid().ToString();
                options.ServiceName = Configuration["ConsulRegistry:ServiceName"];
                options.ServiceAddress = Environment.GetEnvironmentVariable("ASPNETCORE_URLS") != null
                    ? Environment.GetEnvironmentVariable("ASPNETCORE_URLS")
                    : Configuration["ConsulRegistry:ServiceAddress"];
                options.HealthCheckAddress = Configuration["ConsulRegistry:HealthCheckAddress"];
                options.RegistryAddress = Configuration["ConsulRegistry:RegistryAddress"]; //"http://localhost:8500";
            });
            
            //id server 4
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                    {
                        builder.UseMySql(Configuration.GetConnectionString("DefaultConnection"));
                    };
                })
                .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();
            
            //controller
            services.AddControllers(options =>
            {
                options.Filters.Add<MicroServiceCommonResultHandler>(1);
                options.Filters.Add<BizExceptionHandler>(2);
            }).AddNewtonsoftJson(option =>
            {
                // 防止将大写转换成小写
                option.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
            
            
            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}