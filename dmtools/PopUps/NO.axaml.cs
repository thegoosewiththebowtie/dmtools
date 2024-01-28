using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace dmtools.PopUps;

public partial class NO : Window
{
    public NO(string huh)
    {
        InitializeComponent();
        CT(huh);
    }
    public void CT(string huh)
    {
        huhh.Text = huh;
    }

    private void dothe(object? sender, RoutedEventArgs e)
    {
        if (Avalonia.Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.Shutdown();
        }
    }

    private void okk(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }
}