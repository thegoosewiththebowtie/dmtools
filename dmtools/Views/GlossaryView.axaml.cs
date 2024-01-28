using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using dmtools.PopUps;

namespace dmtools.Views;

public partial class GlossaryView : UserControl
{
    public GlossaryView()
    {
        InitializeComponent();
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        var nno = new NO("PLEASE DO NOT THE CAT");
        nno.Height = 250;
        if (Avalonia.Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            nno.ShowDialog(desktop.MainWindow);
        }
    }
}