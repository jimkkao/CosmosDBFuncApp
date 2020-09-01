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
    public class SqlAPIGetFunction
    {
        protected ISqlRepository<Customer> _repo = null;

        public SqlAPIGetFunction(ISqlRepository<Customer> repository)
        {
            _repo = repository;
        }

        [FunctionName("SqlAPIGetFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "customer/{uniqueid}")] HttpRequest req,
            string uniqueid,
            ILogger log)
        {
            log.LogInformation($"C# HTTP trigger function processed a request, uniqueid={uniqueid}");

        
            try
            { 
                Expression < Func<Customer, bool> > lambda = x => x.UniqueId == uniqueid;
          
                var result = await _repo.Get(lambda);
                
                var jsonResult = JsonConvert.SerializeObject(result);

                log.LogInformation($"result: {jsonResult}");

                return (ActionResult)new OkObjectResult(jsonResult);
            }
            catch( Exception e )
            {
                log.LogError(e, $"Sql API failed to get={uniqueid}");

                return (ActionResult)new BadRequestResult();
            }
        }


    }
}
