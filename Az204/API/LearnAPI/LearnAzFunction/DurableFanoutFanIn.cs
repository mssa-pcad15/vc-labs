using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.DurableTask;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace LearnAzFunction
{
    public static class DurableFanoutFanIn
    {
        [Function(nameof(DurableFanoutFanIn))]
        public static async Task<long> RunOrchestrator(
            [OrchestrationTrigger] TaskOrchestrationContext context)
        {
            ILogger logger = context.CreateReplaySafeLogger(nameof(DurableFanoutFanIn));
            PathData? directory = context.GetInput<PathData>();

            string? rootDirectory;
            if (directory is null)
            {
                rootDirectory = Directory.GetParent(typeof(DurableFanoutFanIn).Assembly.Location)!.FullName;
            }
            else
            {
                rootDirectory = directory.Path;
            }

            string[] files = await context.CallActivityAsync<string[]>(
            "GetFileList",
            rootDirectory);

            var tasks = new Task<long>[files.Length];

            for (int i = 0; i < files.Length; i++)
            {
                tasks[i] = context.CallActivityAsync<long>(
                    "CopyFileToBlob",
                    files[i]);
            }
            
            await Task.WhenAll(tasks);
            long totalBytes = tasks.Sum(t => t.Result);
            return totalBytes;
        }
        [Function("CopyFileToBlob")]
        public static long CopyFileToBlob(
            [ActivityTrigger] string filePath,
            FunctionContext executionContext
            )
        {
            var logger = executionContext.GetLogger("CopyFileToBlob");
            logger.LogInformation($"Copying file '{filePath}' to blob storage.");
            // Simulate work by waiting for 1 second
            Thread.Sleep(1000);
            return new FileInfo(filePath).Length;
        }

        [Function("GetFileList")]
        public static string[] GetFileList(
                [ActivityTrigger] string rootDirectory)
        {
            string[] files = Directory.GetFiles(rootDirectory, "*", SearchOption.AllDirectories);
            return files;
        }


        [Function("DurableFanoutFanIn_HttpStart")] //starts here
        public static async Task<HttpResponseData> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req,
            [DurableClient] DurableTaskClient client,
            FunctionContext executionContext)
        {
            ILogger logger = executionContext.GetLogger("DurableFanoutFanIn_HttpStart"); 

            var directory = req.ReadFromJsonAsync<PathData>().Result;
            // Function input comes from the request content.
            string instanceId = await client.ScheduleNewOrchestrationInstanceAsync(
                nameof(DurableFanoutFanIn), directory);

            logger.LogInformation("Started orchestration with ID = '{instanceId}'.", instanceId);

            // Returns an HTTP 202 response with an instance management payload.
            // See https://learn.microsoft.com/azure/azure-functions/durable/durable-functions-http-api#start-orchestration
            return await client.CreateCheckStatusResponseAsync(req, instanceId);
        }

        public class PathData
        {
            public string? Path { get; set; }
        }
    }
}
