using Azure.Messaging.ServiceBus;

namespace AspireSBDemo.WorkerService;

public sealed class Worker(
    ILogger<Worker> logger,
    ServiceBusClient client) : BackgroundService
{

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var processor = client.CreateProcessor(
                "vcnotificationtopic",
                "vcmobile",
                new ServiceBusProcessorOptions());

            // Add handler to process messages
            processor.ProcessMessageAsync += MessageHandler;

            // Add handler to process any errors
            processor.ProcessErrorAsync += ErrorHandler;

            // Start processing
            await processor.StartProcessingAsync();

            logger.LogInformation("""
                Wait for a minute and then press any key to end the processing
                """);

            Console.ReadKey();

            // Stop processing
            logger.LogInformation("""

                Stopping the receiver...
                """);

            await processor.StopProcessingAsync();

            logger.LogInformation("Stopped receiving messages");
        }
    }



    async Task MessageHandler(ProcessMessageEventArgs args)
    {
        string body = args.Message.Body.ToString();

        logger.LogInformation("Received: {Body} from subscription.", body);

        // Complete the message. messages is deleted from the subscription.
        await args.CompleteMessageAsync(args.Message);
    }

    // Handle any errors when receiving messages
    Task ErrorHandler(ProcessErrorEventArgs args)
    {
        logger.LogError(args.Exception, "{Error}", args.Exception.Message);

        return Task.CompletedTask;
    }
}
