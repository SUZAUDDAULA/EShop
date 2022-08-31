using EShop.Infrastructure.EventBus;
using EShop.Infrastructure.Mongo;
using EShop.User.Api.Repositories;
using EShop.User.Api.Services;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace EShop.User.Api
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
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();


            var rabbitmqOption = new RabbitMqOption();
            Configuration.GetSection("rabbitmq").Bind(rabbitmqOption);

            services.AddMassTransit(x => {
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg => {
                    cfg.Host(new Uri(rabbitmqOption.ConnectionString), hostConfig => {
                        hostConfig.Username(rabbitmqOption.Username);
                        hostConfig.Password(rabbitmqOption.Password);
                    });

                    cfg.ReceiveEndpoint("add_user", ep => {
                        ep.PrefetchCount = 16;
                        ep.UseMessageRetry(retryConfig => { retryConfig.Interval(2, 100); });
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
            var dbInitializer = app.ApplicationServices.GetService<IDatabaseInitializer>();
            dbInitializer.InitializeAsync();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
