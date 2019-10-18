using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            return name != null
                ? (ActionResult)new OkObjectResult($"Hello, {name}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}
