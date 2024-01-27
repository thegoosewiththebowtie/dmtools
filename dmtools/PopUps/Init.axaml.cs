using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace dmtools.PopUps;

public partial class Init : Window
{
    public int result { get; set; }
    public Init(string name)
    {
        InitializeComponent();
        this.Title = name;
    }
    private void ok(object? sender, RoutedEventArgs e)
    {
        result = Convert.ToInt32(Initiative.Text);
        this.Close();
    }

    private void Cancel(object? sender, RoutedEventArgs e)
    {
        result = -1;
        this.Close();
    }
    

    private void Initiative_OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (!Int32.TryParse(e.KeySymbol, out int i))
        {
            e.Handled = true;
        }
    }
}