using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using BlazorSwa.Shared;

namespace Api
{
    public class Register
    {
        private readonly BlazorDbContext _context;

        public Register(BlazorDbContext context)
        {
            _context = context;
        }

        [FunctionName("Register")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<User>(requestBody);

            var validationResult = new UserValidator().Validate(input);
            if (validationResult.IsValid)
            {
                _context.Users.Add(input);
                await _context.SaveChangesAsync();
            }
            else
            {
                return new BadRequestObjectResult(validationResult);
            }

            return new OkObjectResult(input);
        }
    }
}
