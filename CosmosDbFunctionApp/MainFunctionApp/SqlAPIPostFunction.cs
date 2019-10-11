using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

using Model;
using RepositoryContract;

namespace MainFunctionApp
{
    public static class SqlAPIPostFunction
    {
        static IServiceProvider _serviceProvider = Bootstrap.ConfigureServices();

        [FunctionName("SqlAPIPostFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "customer")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger SQLPost function processed a request.");

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var customer = JsonConvert.DeserializeObject<Customer>(requestBody);

                IRepository<Customer> repo = _serviceProvider.GetService(typeof(IRepository<Customer>)) as IRepository<Customer>;

                var result = repo.Insert(customer);

                string jsonResult = JsonConvert.SerializeObject(result);

                return (ActionResult)new OkObjectResult(jsonResult);
            }
            catch( Exception ex)
            {
                log.LogError(ex, "CosmosDB Inserted Failed!");
                return (ActionResult)new BadRequestResult();
            }
        }
    }
}
