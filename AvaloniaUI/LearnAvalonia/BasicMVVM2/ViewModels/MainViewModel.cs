namespace BasicMVVM2.ViewModels;

public class MainViewModel : ViewModelBase
{
    // Add our RactiveViewModel
    public ReactiveViewModel ReactiveViewModel { get; } = new ReactiveViewModel();
    public SimpleViewModel SimpleViewModel { get; } = new SimpleViewModel();
}
