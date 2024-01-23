using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using dmtools.Resources;

namespace dmtools.Views;

public partial class AddProfileView : UserControl
{
    public AddProfileView()
    {
        InitializeComponent();
        if (Avalonia.Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            bool x = false;
            NewProfile np = new NewProfile(x);
            np.ShowDialog(desktop.MainWindow);
        }
    }
}