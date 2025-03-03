using System;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
    
using Microsoft.Extensions.Logging;

namespace LearnAzFunction
{
    public class QueueGenerator
    {
        private readonly ILogger _logger;

        public QueueGenerator(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<QueueGenerator>();
        }

        [Function("QueueGenerator")]
        [QueueOutputAttribute("output-queue",Connection ="QueueConnectionString")]
        public string[] Run([TimerTrigger("* * */10 * * *")] TimerInfo myTimer)
        {
            // Use a string array to return more than one message.
            string[] messages = { $"Hello-{DateTime.Now.ToLongTimeString()}", $"World-{DateTime.Now.ToLongTimeString()}" };
        

            _logger.LogInformation("{msg1},{msg2}", messages[0], messages[1]);

            // Queue Output messages
            return messages;
        }
    }
}
