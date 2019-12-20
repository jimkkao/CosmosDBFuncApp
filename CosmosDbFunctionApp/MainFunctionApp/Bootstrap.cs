﻿using System;
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
        protected static string GetEndPointUrl()
        {
            return string.Empty;
        }

        protected static string GetPrimaryKey()
        {
            return string.Empty;
        }

        protected static string GetDataBase()
        {
            return string.Empty;
        }

        protected static string GetContainer()
        {
            return string.Empty;
        }

        protected static string GetPartitionKey()
        {
            return string.Empty;
        }

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
