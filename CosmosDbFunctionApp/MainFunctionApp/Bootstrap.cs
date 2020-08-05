using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using Model;
using Repository;
using RepositoryContract;

namespace MainFunctionApp
{
    public class Bootstrap
    {

        public static IServiceProvider ConfigureServices()
        {
            
            var services = new ServiceCollection()
                .AddSingleton<ISqlRepository<Customer>, SQLRepository<Customer>>()
                .AddSingleton<IMongoRepository<MongoCustomer>, MongoRepository<MongoCustomer>>()
                .AddSingleton<ISqlConfig>(new SqlConfig())
                .AddSingleton<IMongoConfig>(new MongoConfig());

            return services.BuildServiceProvider();
        }
    }
}
