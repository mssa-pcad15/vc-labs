using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace LearnAzFunction
{
    public class HttpFirstFunction
    {
        private readonly ILogger<HttpFirstFunction> _logger;

        public HttpFirstFunction(ILogger<HttpFirstFunction> logger)
        {
            _logger = logger;
        }

        [Function("QuickStartHttp")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
