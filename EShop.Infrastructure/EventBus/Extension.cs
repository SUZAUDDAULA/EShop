﻿using EShop.Infrastructure.Query.Product;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Infrastructure.EventBus
{
    public static class Extension
    {
        public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMq = new RabbitMqOption();
            configuration.GetSection("rabbitmq").Bind(rabbitMq);

            //establish connection with rabbitMQ
            services.AddMassTransit(x=> {
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host(new Uri(rabbitMq.ConnectionString), hostcfg =>
                    {
                        hostcfg.Username(rabbitMq.Username);
                        hostcfg.Password(rabbitMq.Password);
                    });
                }));
                x.AddRequestClient<GetProductById>();
            });

            return services;

        }
    }
}
