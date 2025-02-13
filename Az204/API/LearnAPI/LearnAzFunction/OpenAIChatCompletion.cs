using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.OpenAI.TextCompletion;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace LearnAzFunction
{
    public class OpenAIChatCompletion
    {
        private readonly ILogger _logger;

        public OpenAIChatCompletion(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("negotiate");
        }
        [Function(nameof(WhoIs))]
        public static IActionResult WhoIs(
            [HttpTrigger(AuthorizationLevel.Function, Route = "whois/{name}")] HttpRequestData req,
            [TextCompletionInput("Who is {name}?", Model = "%CHAT_MODEL_DEPLOYMENT_NAME%")] TextCompletionResponse response)
        {
            return new OkObjectResult(response.Content);
        }

        /// <summary>
    }

}