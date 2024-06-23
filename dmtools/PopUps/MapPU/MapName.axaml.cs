using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace dmtools.PopUps;

public partial class MapName : Window
{
    public string result { get; set; }
    public MapName()
    {
        InitializeComponent();
    }
    private void ok(object? sender, RoutedEventArgs e)
    {
        result = Initiative.Text;
        this.Close();
    }

    private void Cancel(object? sender, RoutedEventArgs e)
    {
        result = "";
        this.Close();
    }
}