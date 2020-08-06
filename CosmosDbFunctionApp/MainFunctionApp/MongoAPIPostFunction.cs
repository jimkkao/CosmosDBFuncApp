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
    public class MongoAPIPostFunction
    {

        static IServiceProvider _serviceProvider = Bootstrap.ConfigureServices();

        protected IMongoRepository<MongoCustomer> _repo = null;

        public MongoAPIPostFunction(IMongoRepository<MongoCustomer> repository)
        {
            _repo = repository;
        }

        [FunctionName("MongoAPIPostFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "customer")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                log.LogInformation($"request body:{requestBody}");
                var cust = JsonConvert.DeserializeObject<MongoCustomer>(requestBody);

                //IMongoRepository<MongoCustomer> repo = _serviceProvider.GetService(typeof(IMongoRepository<MongoCustomer>)) as IMongoRepository<MongoCustomer>;

                if (string.IsNullOrEmpty(cust.id))
                {
                    cust.id = Guid.NewGuid().ToString();
                }

                var result = await _repo.Insert(cust);

                string jsonResult = JsonConvert.SerializeObject(result);

                return (ActionResult)new OkObjectResult(jsonResult);
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Function MongoAPI Post Failed!");
                return (ActionResult)new BadRequestResult();
            }
        }
    }
}
