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
            string endpointurl = GetEndPointUrl();

            string primaryKey = GetPrimaryKey();

            string database = GetDataBase();

            string container = GetContainer();

            string partitionKey = GetPartitionKey();
            
            var services = new ServiceCollection()
                .AddSingleton<IRepository<Customer>, SQLRepository<Customer>>()
                .AddSingleton<ISqlConfig>(new SqlConfig());



            return services.BuildServiceProvider();
        }
    }
}
