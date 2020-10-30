using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Api
{
    public class Raffle
    {
        private readonly BlazorDbContext _context;

        public Raffle(BlazorDbContext context)
        {
            _context = context;
        }

        [FunctionName("Raffle")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");

                var user = (await _context.Users.ToListAsync()).OrderBy(o => Guid.NewGuid()).FirstOrDefault();
                
                if (user != null)
                {
                    return new OkObjectResult(user);
                }
                else
                {
                    return new NoContentResult();
                }
            } 
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
