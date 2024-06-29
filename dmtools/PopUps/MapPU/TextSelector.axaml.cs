using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using dmtools.Templates;

namespace dmtools.PopUps.MapPU;

public partial class TextSelector : Window
{
    public string coloret { get; set; }
    public int width0 { get; set; }
    public int height0 { get; set; }
    public TextSelector(ItemCoordinates max, ItemCoordinates cords)
    {
        InitializeComponent();
        ini(max, cords);
    }
    private void ini(ItemCoordinates max, ItemCoordinates cords)
    {
        width.Maximum = max.horizontal - cords.vertical;
    }
    private void Ok_OnClick(object? sender, RoutedEventArgs e)
    {
        coloret = TextBoxx.Text;
        width0 = (int)width.Value;
        this.Close();
    }

    private void Cancel_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }
}