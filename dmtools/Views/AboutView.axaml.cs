using System;
using System.Diagnostics;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using dmtools.Generators;

namespace dmtools.Views;

public partial class AboutView : UserControl
{
    public string Readme { get; set; }
    public string License { get; set; }
    public AboutView()
    {
        InitializeComponent();
        TextIni();
        MainWindow.SizzeChanged += SizeChange;
    }

    public void SizeChange(object o, EventArgs e)
    {
        dmt.FontSize = totwl.Bounds.Width * 0.1;
        totw.FontSize = totwl.Bounds.Width * 0.05;
        ImageLogo.Height = this.Bounds.Height * 0.7;
    }

    public void TextIni()
    {
        Readme = File.ReadAllText("Docs/README.txt");
        rm.Text = Readme;
        License = File.ReadAllText("Docs/LICENSE.txt");
        lic.Text = License;
    }

    private void Website_OnClick(object? sender, RoutedEventArgs e)
    {
        Process.Start(new ProcessStartInfo() { FileName = "https://www.triangleonthewall.org/dmtools", UseShellExecute = true });
    }

    private void Github_OnClick(object? sender, RoutedEventArgs e)
    {
        Process.Start(new ProcessStartInfo() { FileName = "https://github.com/thegoosewiththebowtie/dmtools", UseShellExecute = true });
    }

    private void Telegram_OnClick(object? sender, RoutedEventArgs e)
    {
        Process.Start(new ProcessStartInfo() { FileName = "https://t.me/dmtoolsbyvtg", UseShellExecute = true });
    }

    private void Reddit_OnClick(object? sender, RoutedEventArgs e)
    {
        Process.Start(new ProcessStartInfo() { FileName = "https://www.reddit.com/r/dmtools/", UseShellExecute = true });
    }
}