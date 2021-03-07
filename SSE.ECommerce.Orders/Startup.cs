using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SSE.ECommerce.Orders.Data.Configuration;
using SSE.ECommerce.Orders.Data.Interfaces;
using SSE.ECommerce.Orders.Data.Services;
using SSE.ECommerce.Orders.Domain.Interfaces;
using SSE.ECommerce.Orders.Domain.Managers;
using SSE.ECommerce.Orders.Proxy.Interfaces;
using SSE.ECommerce.Orders.Proxy.Proxies;

namespace SSE.ECommerce.Orders
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
            // TODO: Ensure you have included the values for the required settings in appsettings
            services.Configure<OrdersSettings>(Configuration.GetSection("OrdersSettings"));
            var ordersSettings = new OrdersSettings();
            Configuration.GetSection("OrdersSettings").Bind(ordersSettings);

            // Configure HTTP Client for the Customer API
            // TODO: For Production Release also configure retry and circuit breaker policies
            services.AddHttpClient<ICustomerService, CustomerService>("Customer", client =>
            {
                client.BaseAddress = new Uri(ordersSettings.CustomerApiUrl);
            });
            //.AddPolicyHandler(GetRetryPolicy())
            //.AddPolicyHandler(GetCircuitBreakerPolicy());

            // Configure DI
            services.AddTransient<IDbConnection>(db => new SqlConnection(
                ordersSettings.DbConnectionString));
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICustomerManager, CustomerManager>();
            services.AddScoped<IOrderManager, OrderManager>();
            services.AddScoped<IMostRecentOrderSummaryProxy, MostRecentOrderSummaryProxy>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            // TODO: Configure Production Ready Authentication and Authorization 
            // app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
