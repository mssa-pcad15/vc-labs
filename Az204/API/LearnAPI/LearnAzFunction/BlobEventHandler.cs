// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}

using System;
using Azure.Messaging;
using Azure.Messaging.EventGrid;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace LearnAzFunction
{
    public class BlobEventHandler
    {
        private readonly ILogger<BlobEventHandler> _logger;

        public BlobEventHandler(ILogger<BlobEventHandler> logger)
        {
            _logger = logger;
        }

        [Function(nameof(BlobEventHandler))]
        public void Run([EventGridTrigger] EventGridEvent eventGridEvent)
        {
            _logger.LogInformation("Event type: {type}, Event subject: {subject}", eventGridEvent.EventType, eventGridEvent.Subject);
        }
    }
}
