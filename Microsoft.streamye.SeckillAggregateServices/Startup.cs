using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Streamye.Commons.CommonResults;
using Microsoft.Streamye.Commons.Exceptions.Handlers;
using Microsoft.Streamye.Commons.ModelBinds.Users;
using Microsoft.Streamye.Cores.Middleware.Extensions;
using Newtonsoft.Json.Serialization;

namespace Microsoft.Streamye.SeckillAggregateServices
{
    public class Startup
    {
         public void ConfigureServices(IServiceCollection services)
        {
            services.AddMiddlewareServices(middlewareOptions =>
            {
                middlewareOptions.HttpClientName = "seckill aggregate service";
                middlewareOptions.pollyHttpClientOptions = pollyOptions => { pollyOptions.RetryCount = 3; };
            });

            services.AddControllers(options =>
            {
                options.Filters.Add<AggregateCommonResultHandler>();
                options.Filters.Add<BizExceptionHandler>();
                options.ModelBinderProviders.Insert(0, new SysUserModelBinderProvider());
            }).AddNewtonsoftJson(options => {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Hello World!"); });
            });
        }
    }
}