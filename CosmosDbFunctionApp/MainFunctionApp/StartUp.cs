using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

using Model;
using RepositoryContract;
using Repository;

[assembly: FunctionsStartup(typeof(MainFunctionApp.Startup))]

namespace MainFunctionApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            //builder.Services.AddHttpClient();

            //builder.Services.AddSingleton<IMyService>((s) => {
            //    return new MyService();
            //});

            builder.Services.AddSingleton<ISqlRepository<Customer>, SQLRepository<Customer>>()

                .AddSingleton<IMongoRepository<MongoCustomer>, MongoRepository<MongoCustomer>>()
                .AddSingleton<ISqlConfig>(new SqlConfig())
                .AddSingleton<IMongoConfig>(new MongoConfig());

            //builder.Services.AddSingleton<ILoggerProvider, MyLoggerProvider>();
        }
    }
}