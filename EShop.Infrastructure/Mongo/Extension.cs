using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace EShop.Infrastructure.Mongo
{
    public static class Extension
    {
        public static void AddMongoDb(this IServiceCollection services,IConfiguration configuration)
        {
            var configSection = configuration.GetSection("mongo");
            var mongoConfig = new MongoConfig();
            configSection.Bind(mongoConfig);

            services.AddSingleton<IMongoDatabase>(client =>
            {
                var mongoClient = client.GetService<IMongoClient>();
                return mongoClient.GetDatabase(mongoConfig.Database);
            });
        }
    }
}
