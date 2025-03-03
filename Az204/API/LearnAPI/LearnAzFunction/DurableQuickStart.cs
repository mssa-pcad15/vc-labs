using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.DurableTask;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;

namespace LearnAzFunction
{
    public static class DurableQuickStart
    {
        [Function(nameof(DurableQuickStart))]
        public static async Task<List<string>> RunOrchestrator(
            [OrchestrationTrigger] TaskOrchestrationContext context)
        {
            ILogger logger = context.CreateReplaySafeLogger(nameof(DurableQuickStart));
            logger.LogInformation("Saying hello.");
            var outputs = new List<string>();

            // Replace name and input with values relevant for your Durable Functions Activity
            outputs.Add(await context.CallActivityAsync<string>(nameof(SayHello), "Tokyo"));
            var timer = context.CreateTimer(context.CurrentUtcDateTime.AddSeconds(10), CancellationToken.None);
            await timer;
            outputs.Add(await context.CallActivityAsync<string>(nameof(SayHello), "Seattle"));
            var timer2 = context.CreateTimer(context.CurrentUtcDateTime.AddSeconds(10), CancellationToken.None);
            await timer2;
            outputs.Add(await context.CallActivityAsync<string>(nameof(SayHello), "London"));
            var timer3 = context.CreateTimer(context.CurrentUtcDateTime.AddSeconds(10), CancellationToken.None);
            await timer3;

            // returns ["Hello Tokyo!", "Hello Seattle!", "Hello London!"]
            return outputs;
        }

        [Function(nameof(SayHello))]
        public static string SayHello([ActivityTrigger] string name, FunctionContext executionContext)
        {
            ILogger logger = executionContext.GetLogger("SayHello");
            logger.LogInformation("Saying hello to {name}.", name);
            
            return $"Hello {name}!";
        }

        [Function("DurableQuickStart_HttpStart")]
        public static async Task<HttpResponseData> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
            [DurableClient] DurableTaskClient client,
            FunctionContext executionContext)
        {
            ILogger logger = executionContext.GetLogger("DurableQuickStart_HttpStart");

            // Function input comes from the request content.
            string instanceId = await client.ScheduleNewOrchestrationInstanceAsync(
                nameof(DurableQuickStart));

            logger.LogInformation("Started orchestration with ID = '{instanceId}'.", instanceId);

            // Returns an HTTP 202 response with an instance management payload.
            // See https://learn.microsoft.com/azure/azure-functions/durable/durable-functions-http-api#start-orchestration
            return await client.CreateCheckStatusResponseAsync(req, instanceId);
        }
    }
}
