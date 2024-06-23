using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace dmtools.PopUps;

public partial class MapSize : Window
{
    public string res { get; set; }

    public MapSize()
    {
        InitializeComponent();
    }
    private void Add_OnClick(object? sender, RoutedEventArgs e)
    {
        res = $"{w.Value}${h.Value}";
        this.Close();
    }

    private void Cancel_OnClick(object? sender, RoutedEventArgs e)
    {
        res = "";
        this.Close();
    }
}