namespace BasicMVVM1.ViewModels;

public class MainViewModel : ViewModelBase
{
    public SimpleViewModel SimpleViewModel { get; } = new SimpleViewModel();
    public ReactiveViewModel ReactiveViewModel { get; } = new ReactiveViewModel();

}
