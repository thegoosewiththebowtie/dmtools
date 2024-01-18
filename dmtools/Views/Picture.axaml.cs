using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace dmtools.Views;

public partial class Picture : Window
{
    public static event EventHandler Closeed;
    public Picture()
    {
        InitializeComponent();
    }

    private void Window_OnClosing(object? sender, WindowClosingEventArgs e)
    {
            Closeed(sender, e);
            this.Hide();
            e.Cancel = true;
    }
}