using Azure.Identity;
using Azure.Messaging.ServiceBus.Administration;
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
    public class CreateTopicAndSubscriptionViewModel : ViewModelBase
    {
        ServiceBusAdministrationClient adminClient;

        // connection string to the Service Bus namespace
        readonly string connectionString = "pcad15.servicebus.windows.net";

        // name of the Service Bus topic
        readonly string topicName = "vctopicfiltersampletopic";

        // names of subscriptions to the topic
        readonly string subscriptionAllOrders = "AllOrders";
        readonly string subscriptionColorRed = "ColorRed";
        readonly string subscriptionColorBlueSize10Orders = "ColorBlueSize10Orders";
        readonly string subscriptionHighPriorityRedOrders = "HighPriorityRedOrders";
        public ICommand CreateTopicAndSubscriptionCommand { get; }
       
        public  ObservableCollection<string> Messages { get; init; }
            

        public CreateTopicAndSubscriptionViewModel()
        {
            Messages = new();
            Messages?.Add("Creating adminClient client");
            adminClient = new ServiceBusAdministrationClient(connectionString, new DefaultAzureCredential());
            CreateTopicAndSubscriptionCommand = ReactiveCommand.CreateFromTask(CreateTopicsAndFilterAsync);
           
        }

        public async Task CreateTopicsAndFilterAsync()
        {
            try
            {
  
                Messages.Add($"Creating the topic {topicName}");
                await adminClient.CreateTopicAsync(topicName);

                Messages.Add($"Creating the subscription {subscriptionAllOrders} for the topic with a SQL filter ");

                // Create a True Rule filter with an expression that always evaluates to true
                // It's equivalent to using SQL rule filter with 1=1 as the expression

                await adminClient.CreateSubscriptionAsync(
                        new CreateSubscriptionOptions(topicName, subscriptionAllOrders),
                        new CreateRuleOptions(subscriptionAllOrders, new TrueRuleFilter()));


                Messages.Add($"Creating the subscription {subscriptionColorBlueSize10Orders} with a SQL filter");
                // Create a SQL filter with color set to blue and quantity to 10
                await adminClient.CreateSubscriptionAsync(
                        new CreateSubscriptionOptions(topicName, subscriptionColorBlueSize10Orders),
                        new CreateRuleOptions(subscriptionColorBlueSize10Orders, new SqlRuleFilter("color='blue' AND quantity=10")));


                Messages.Add($"Creating the subscription {subscriptionColorRed} with a SQL filter");
                // Create a SQL filter with color equals to red and a SQL action with a set of statements
                await adminClient.CreateSubscriptionAsync(topicName, subscriptionColorRed);
                // remove the $Default rule
                await adminClient.DeleteRuleAsync(topicName, subscriptionColorRed, "$Default");
                // now create the new rule. notice that user. prefix is used for the user/application property
                await adminClient.CreateRuleAsync(topicName, subscriptionColorRed, new CreateRuleOptions
                {
                    Name = subscriptionColorRed,
                    Filter = new SqlRuleFilter("user.color='red'"),
                    Action = new SqlRuleAction("SET quantity = quantity / 2; REMOVE priority;SET sys.CorrelationId = 'low';")

                }
                );

                Messages.Add($"Creating the subscription {subscriptionHighPriorityRedOrders} with a correlation filter");
                // Create a correlation filter with color set to Red and priority set to High
                await adminClient.CreateSubscriptionAsync(
                        new CreateSubscriptionOptions(topicName, subscriptionHighPriorityRedOrders),
                        new CreateRuleOptions("HighPriorityRedOrders", new CorrelationRuleFilter() { Subject = "red", CorrelationId = "high" }));


            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public async Task DeleteTopicAsync()
        {
            try
            {
             await adminClient.DeleteTopicAsync(topicName);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task ResetTopicAsync()
        {
            try
            {
                await CreateTopicsAndFilterAsync();
                await DeleteTopicAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
