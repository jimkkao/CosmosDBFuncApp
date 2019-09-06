using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

namespace MainFunctionApp
{
    public class Bootstrap
    {
        public static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();


            return services.BuildServiceProvider();
        }
    }
}
