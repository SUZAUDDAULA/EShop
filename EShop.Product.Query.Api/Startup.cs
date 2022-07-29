using EShop.Infrastructure.EventBus;
using EShop.Infrastructure.Mongo;
using EShop.Product.Api.Services.Interfaces;
using EShop.Product.DataProvider.Repositories;
using EShop.Product.DataProvider.Repositories.Interfaces;
using EShop.Product.DataProvider.Services;
using EShop.Product.Query.Api.Handlers;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace EShop.Product.Query.Api
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
            services.AddControllers();
            services.AddMongoDb(Configuration);
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<GetProductByIdHandlers>();

            //establish connection with rabbitMQ
            services.AddMassTransit(x => {
                x.AddConsumer<GetProductByIdHandlers>();
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var rabbitMq = new RabbitMqOption();
                    Configuration.GetSection("rabbitmq").Bind(rabbitMq);
                    cfg.Host(new Uri(rabbitMq.ConnectionString), hostcfg =>
                    {
                        hostcfg.Username(rabbitMq.Username);
                        hostcfg.Password(rabbitMq.Password);
                    });
                }));
                
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var bus = app.ApplicationServices.GetService<IBusControl>();
            bus.Start();
        }
    }
}
