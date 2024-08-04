using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using dmtools.Templates;

namespace dmtools.PopUps.MapPU;

public partial class TextSelector : Window
{
    public string coloret { get; set; }
    public int width0 { get; set; }
    public int sz { get; set; }
    public string bg { get; set; }
    public bool fg { get; set; } = false;
    public TextSelector(ItemCoordinates max, ItemCoordinates cords) //
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
        bg = txtbkg.Color.ToString();
        fg = (bool)Switch.IsChecked;
        sz = (int)fontsize.Value;
        this.Close();
    }

    private void Cancel_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void Txtbkg_OnColorChanged(object? sender, ColorChangedEventArgs e)
    {
        TextBoxx.Background = new SolidColorBrush(txtbkg.Color);
    }

    private void ToggleButton_OnIsCheckedChanged(object? sender, RoutedEventArgs e)
    {
        if ((sender as ToggleSwitch).IsChecked == true)
        {
            TextBoxx.Foreground = Brushes.White;
        }
        else
        {
            TextBoxx.Foreground = Brushes.Black;
        }

    }
}