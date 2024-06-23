using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace dmtools.PopUps;

public partial class IconSelector : Window
{
    public string sel { get; set; }
    public IconSelector()
    {
        InitializeComponent();
    }
    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        sel = (sender as Button).Name;
        this.Close();
    }
}