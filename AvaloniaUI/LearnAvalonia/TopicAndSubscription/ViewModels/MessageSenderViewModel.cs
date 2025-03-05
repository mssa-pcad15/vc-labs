using Azure.Identity;
using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TopicAndSubscription.ViewModels
{
    public class MessageSenderViewModel : ViewModelBase
    {
        readonly string connectionString = "pcad15.servicebus.windows.net";
        readonly string topicName = "vctopicfiltersampletopic";
        public ICommand SendTestMessagesCommand { get; }
        readonly string subscriptionAllOrders = "AllOrders";
        readonly string subscriptionColorRed = "ColorRed";
        readonly string subscriptionColorBlueSize10Orders = "ColorBlueSize10Orders";
        readonly string subscriptionHighPriorityRedOrders = "HighPriorityRedOrders";
        public ObservableCollection<Order> Sub1 { get; init; }
        public ObservableCollection<Order> Sub2 { get; init; }
        public ObservableCollection<Order> Sub3 { get; init; }
        public ObservableCollection<Order> Sub4 { get; init; }

        public MessageSenderViewModel()
        {
            SendTestMessagesCommand = ReactiveCommand.CreateFromTask(SendTestMessagesAsync);
            Sub1 = new ObservableCollection<Order>();
            Sub2 = new ObservableCollection<Order>();
            Sub3 = new ObservableCollection<Order>();
            Sub4 = new ObservableCollection<Order>();
    
        }

        private async Task SendTestMessagesAsync()
        {
            ServiceBusSender sender = new ServiceBusClient(connectionString, new DefaultAzureCredential()).CreateSender(topicName);
            await Task.WhenAll(
                SendOrder(sender, new Order()),
                SendOrder(sender, new Order { Color = "blue", Quantity = 5, Priority = "low" }),
                SendOrder(sender, new Order { Color = "red", Quantity = 10, Priority = "high" }),
                SendOrder(sender, new Order { Color = "yellow", Quantity = 5, Priority = "low" }),
                SendOrder(sender, new Order { Color = "blue", Quantity = 10, Priority = "low" }),
                SendOrder(sender, new Order { Color = "blue", Quantity = 5, Priority = "high" }),
                SendOrder(sender, new Order { Color = "blue", Quantity = 10, Priority = "low" }),
                SendOrder(sender, new Order { Color = "red", Quantity = 5, Priority = "low" }),
                SendOrder(sender, new Order { Color = "red", Quantity = 10, Priority = "low" }),
                SendOrder(sender, new Order { Color = "red", Quantity = 5, Priority = "low" }),
                SendOrder(sender, new Order { Color = "yellow", Quantity = 10, Priority = "high" }),
                SendOrder(sender, new Order { Color = "yellow", Quantity = 5, Priority = "low" }),
                SendOrder(sender, new Order { Color = "yellow", Quantity = 10, Priority = "low" })
                );


            _ = ReceiveMessagesAsync(subscriptionAllOrders, Sub1);
            _ = ReceiveMessagesAsync(subscriptionColorRed, Sub2);
            _ = ReceiveMessagesAsync(subscriptionColorBlueSize10Orders, Sub3);
            _ = ReceiveMessagesAsync(subscriptionHighPriorityRedOrders, Sub4);

        }

        private async Task ReceiveMessagesAsync(string subName, ObservableCollection<Order> sub)
        {
            ServiceBusClient client = new ServiceBusClient(connectionString, new DefaultAzureCredential());
            ServiceBusReceiver receiver = client.CreateReceiver(topicName, subName);
            ServiceBusReceivedMessage message;
            while ((message = await receiver.ReceiveMessageAsync()) != null)
            {
                sub.Add(JsonConvert.DeserializeObject<Order>(Encoding.UTF8.GetString(message.Body)) ?? new Order());
                await receiver.CompleteMessageAsync(message);
            }
        }

        async Task SendOrder(ServiceBusSender sender, Order order)
        {
            var message = new ServiceBusMessage(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(order)))
            {
                CorrelationId = order.Priority,
                Subject = order.Color,
                ApplicationProperties =
                {
                    { "color", order.Color },
                    { "quantity", order.Quantity },
                    { "priority", order.Priority }
                }
            };
            await sender.SendMessageAsync(message);

            Console.WriteLine("Sent order with Color={0}, Quantity={1}, Priority={2}", order.Color, order.Quantity, order.Priority);
        }
    }

    public class Order
    {
        public string Color
        {
            get;
            set;
        }

        public int Quantity
        {
            get;
            set;
        }

        public string Priority
        {
            get;
            set;
        }
    }
}
