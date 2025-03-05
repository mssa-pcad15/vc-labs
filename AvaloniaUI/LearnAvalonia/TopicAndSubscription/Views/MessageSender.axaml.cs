using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using TopicAndSubscription.ViewModels;

namespace TopicAndSubscription.Views;

public partial class MessageSender : UserControl
{
    public MessageSender()
    {
        InitializeComponent();
        DataContext = new MessageSenderViewModel();
    }
}