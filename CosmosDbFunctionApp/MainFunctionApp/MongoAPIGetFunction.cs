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
    public static class MongoAPIGetFunction
    {
        static IServiceProvider _serviceProvider = Bootstrap.ConfigureServices();

        [FunctionName("MongoAPIGetFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "customer/{uniqueid}")] HttpRequest req, string uniqueid,
            ILogger log)
        {
            log.LogInformation($"C# HTTP trigger MongoAPI Get function processed a request. uniqueid={uniqueid}");

            try
            {
                IMongoRepository<Customer> repo = _serviceProvider.GetService(typeof(IMongoRepository<Customer>)) as IMongoRepository<Customer>;

                Expression<Func<Customer, bool>> lambda = x => x.UniqueId == uniqueid;

                var result = await repo.Get(lambda);

                var jsonResult = JsonConvert.SerializeObject(result);

                log.LogInformation($"result: {jsonResult}");

                return (ActionResult)new OkObjectResult(jsonResult);
            }
            catch (Exception e)
            {
                log.LogError(e.Message);

                return (ActionResult)new BadRequestResult();
            }

        }
    }
}
