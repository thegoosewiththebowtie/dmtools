using ReactiveUI;

namespace dmtools.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    ViewModelBase _content;

    public MainWindowViewModel()
    {
        Content = List = new HomeViewModel();
    }

    public ViewModelBase Content
    {
        get => _content;
        private set => this.RaiseAndSetIfChanged(ref _content, value);
    }

    public HomeViewModel List { get; }

    public void Settings()
    {
        var vm = new SettingsViewModel();
        Content = vm;
    }
    public void Home()
    {
        var vm = new HomeViewModel();
        Content = vm;
    }

    public void About()
    {
        var vm = new AboutViewModel();
        Content = vm;
    }
}