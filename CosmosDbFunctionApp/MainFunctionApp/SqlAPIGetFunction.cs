using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq.Expressions;

using RepositoryContract;
using Model;
using Repository;

namespace MainFunctionApp
{
    public static class SqlAPIGetFunction
    {
        static IServiceProvider _serviceProvider = Bootstrap.ConfigureServices();

        [FunctionName("SqlAPIGetFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "customer/{uniqueid}")] HttpRequest req,
            string uniqueid,
            ILogger log)
        {
            log.LogInformation($"C# HTTP trigger function processed a request, uniqueid={uniqueid}");

        
            try
            { 
                ISqlRepository<Customer> repo = _serviceProvider.GetService(typeof(ISqlRepository<Customer>)) as ISqlRepository<Customer>;
                Expression < Func<Customer, bool> > lambda = x => x.UniqueId == uniqueid;
          
                var result = await repo.Get(lambda);
                
                var jsonResult = JsonConvert.SerializeObject(result);

                log.LogInformation($"result: {jsonResult}");

                return (ActionResult)new OkObjectResult(jsonResult);
            }
            catch( Exception e )
            {
                log.LogError(e.Message);

                return (ActionResult)new BadRequestResult();
            }
        }


    }
}
