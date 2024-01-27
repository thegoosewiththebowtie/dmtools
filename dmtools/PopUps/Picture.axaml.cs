using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;

namespace dmtools.PopUps;

public partial class Picture : Window
{
    public static event EventHandler Closeed;
    public Picture()
    {
        InitializeComponent();
    }

    public void pichange(Bitmap srs)
    {
        MainImg.Source = srs;
    }

    private void Window_OnClosing(object? sender, WindowClosingEventArgs e)
    {
            Closeed(sender, e);
            this.Hide();
            e.Cancel = true;
    }
}