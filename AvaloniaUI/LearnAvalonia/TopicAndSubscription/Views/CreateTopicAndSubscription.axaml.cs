using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Azure.Messaging.ServiceBus.Administration;
using System.Threading.Tasks;
using System;
using Azure.Identity;
using System.Collections.ObjectModel;
using DynamicData;
using TopicAndSubscription.ViewModels;

namespace TopicAndSubscription.Views;

public partial class CreateTopicAndSubscription : UserControl
{
   

    public CreateTopicAndSubscription()
    {
        InitializeComponent();
        this.DataContext = new CreateTopicAndSubscriptionViewModel();
    }
  
}