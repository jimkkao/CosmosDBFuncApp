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

            var id = uniqueid;

            IRepository<Customer> repo = _serviceProvider.GetService(typeof(IRepository<Customer>)) as IRepository<Customer>;

            Expression < Func<Customer, bool> > lambda = x => x.UniqueId == uniqueid;
            try
            {
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

        static Func<Customer, bool> CreateLambda(string uniqueid)
        {
            Expression<Func<Customer, bool>> lambda = x => x.UniqueId == uniqueid;

            return lambda.Compile();
        }
    }
}
