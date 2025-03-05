using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Messaging.ServiceBus;
using ReactiveUI;



namespace ServiceBusQueueSendReceive.ViewModels;

public class MainViewModel : ViewModelBase
{
    private ServiceBusClient client;
    private int _messageCount = 3;
    private string _statusMessage=string.Empty;

    public int MessageCount
    {
        get => _messageCount;
        set => this.RaiseAndSetIfChanged(ref _messageCount, value);
    }
    public string StatusMessage
    {
        get => _statusMessage;
        set => this.RaiseAndSetIfChanged(ref _statusMessage, value);
    }

    public ObservableCollection<string> _messages = new();
    public ObservableCollection<string> Messages
    {
        get { return _messages; }
        set { this.RaiseAndSetIfChanged(ref _messages, value); }
    }
    public ReactiveCommand<Unit, Unit> SendMessagesCommand { get; }

    public ReactiveCommand<Unit, Unit> ReceiveMessageCommand { get; }
    public MainViewModel()
    {
        var isValidObservable = this.WhenAnyValue(x=>x.MessageCount,
                c => c>0);
        SendMessagesCommand = ReactiveCommand.CreateFromTask(SendMessageBatch,
                                                isValidObservable);
        ReceiveMessageCommand = ReactiveCommand.CreateFromTask(ReceiveMessageAsync);

        var clientOptions = new ServiceBusClientOptions()
        {
            TransportType = ServiceBusTransportType.AmqpWebSockets
        };
        client = new ServiceBusClient(
            "pcad15.servicebus.windows.net",
            new DefaultAzureCredential(),
            clientOptions);


    }

    private async Task ReceiveMessageAsync()
    {
        
        ServiceBusProcessor processor;

        // create a processor that we can use to process the messages
        // TODO: Replace the <QUEUE-NAME> placeholder
        processor = client.CreateProcessor("vc", new ServiceBusProcessorOptions());

            // add handler to process messages
            processor.ProcessMessageAsync += MessageHandler;

            // add handler to process any errors
            processor.ProcessErrorAsync += ErrorHandler;

            // start processing 
            await processor.StartProcessingAsync();
      
  

    }
    async Task MessageHandler(ProcessMessageEventArgs args)
    {
       
        string body = args.Message.Body.ToString();
        Messages.Insert(0,body);
        

        // complete the message. message is deleted from the queue. 
        await args.CompleteMessageAsync(args.Message);
    }

    // handle any errors when receiving messages
    Task ErrorHandler(ProcessErrorEventArgs args)
    {
        Console.WriteLine(args.Exception.ToString());
        return Task.CompletedTask;
    }
    private async Task SendMessageBatch()
    {
       
        ServiceBusSender sender;
       
    
        sender = client.CreateSender("vctopic");

        // create a batch 
        using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();

        
        for (int i = 1; i <= _messageCount; i++)
        {
            // try adding a message to the batch
            if (!messageBatch.TryAddMessage(new ServiceBusMessage($"Message {i}")))
            {
                // if it is too large for the batch
                throw new Exception($"The message {i} is too large to fit in the batch.");
            }
        }

        try
        {
            // Use the producer client to send the batch of messages to the Service Bus queue
            await sender.SendMessagesAsync(messageBatch);
            StatusMessage= $"{_messageCount} Messages sent successfully";
        }
        finally
        {
            // Calling DisposeAsync on client types is required to ensure that network
            // resources and other unmanaged objects are properly cleaned up.
            //await sender.DisposeAsync();
            //await client.DisposeAsync();
        }

    }

}
