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
    public class MongoAPIGetFunction
    {
        protected IMongoRepository<MongoCustomer> _repo = null;

        public MongoAPIGetFunction(IMongoRepository<MongoCustomer> repository)
        {
            _repo = repository;
        }

        [FunctionName("MongoAPIGetFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "mongo/customer/{uniqueid}")] HttpRequest req, string uniqueid,
            ILogger log)
        {
            log.LogInformation($"C# HTTP trigger MongoAPI Get function processed a request. uniqueid={uniqueid}");

            try
            {
                Expression<Func<MongoCustomer, bool>> lambda = x => x.UniqueId == uniqueid;

                var result = await _repo.Get(lambda);

                var jsonResult = JsonConvert.SerializeObject(result);

                log.LogInformation($"result: {jsonResult}");

                return (ActionResult)new OkObjectResult(jsonResult);
            }
            catch (Exception e)
            {
                log.LogError(e, $"Mongo API failed to get {uniqueid}");

                return (ActionResult)new BadRequestResult();
            }

        }
    }
}
