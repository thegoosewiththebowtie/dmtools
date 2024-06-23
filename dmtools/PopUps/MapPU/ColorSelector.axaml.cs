using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using dmtools.Templates;

namespace dmtools.PopUps;

public partial class ColorSelector : Window
{
    public string coloret { get; set; }
    public int width0 { get; set; }
    public int height0 { get; set; }
    public ColorSelector(ItemCoordinates max, ItemCoordinates cords)
    {
        InitializeComponent();
        ini(max, cords);
    }

    private void ini(ItemCoordinates max, ItemCoordinates cords)
    {
        height.Maximum = max.vertical - cords.horizontal;
        width.Maximum = max.horizontal - cords.vertical;
        
    }

    private void Ok_OnClick(object? sender, RoutedEventArgs e)
    {
        coloret = ToHex(colorView.Color);
        width0 = (int)width.Value;
        height0 = (int)height.Value;
        this.Close();
    }

    private void Cancel_OnClick(object? sender, RoutedEventArgs e)
    {
       this.Close();
    }

    private static string ToHex(Avalonia.Media.Color c)
        => $"#{c.R:X2}{c.G:X2}{c.B:X2}";
}