using System;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

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
        idn.FontSize = totwl.Bounds.Width * 0.05;
        ndi.FontSize = totwl.Bounds.Width * 0.05;
    }

    public void TextIni()
    {
        Readme = File.ReadAllText("Docs/README.txt");
        rm.Text = Readme;
        License = File.ReadAllText("Docs/LICENSE.txt");
        lic.Text = License;
    }
}