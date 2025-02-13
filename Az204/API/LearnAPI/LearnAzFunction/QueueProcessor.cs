using System;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace LearnAzFunction
{
    public class QueueProcessor
    {
        private readonly ILogger<QueueProcessor> _logger;

        public QueueProcessor(ILogger<QueueProcessor> logger)
        {
            _logger = logger;
        }

        [Function(nameof(QueueProcessor))]
        public void Run([QueueTrigger("output-queue", Connection = "QueueConnectionString")] QueueMessage message)
        {
            _logger.LogInformation($"C# Queue trigger function processed: {message.MessageText}");
        }
    }
}
