using System;
using Avalonia.Controls;
using LiteDB;
using System.IO;
using System.Net;
using System.Net.Mime;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using dmtools.ViewModels;

namespace dmtools.Views;

public partial class MainWindow : Window
{
    public static event EventHandler SizzeChanged;

    public MainWindow()
    {
        InitializeComponent();
        System.IO.Directory.CreateDirectory("Profiles");
        System.IO.Directory.CreateDirectory("Music");
        System.IO.Directory.CreateDirectory("Music/Amb");
        System.IO.Directory.CreateDirectory("Music/Mus");
        System.IO.Directory.CreateDirectory("Music/Snd");
        System.IO.Directory.CreateDirectory("Images");
    }
    public void Control_OnSizeChanged(object? sender, SizeChangedEventArgs e)
    {
        SizzeChanged(sender, e);
    }

    private void Expand_OnClick(object? sender, RoutedEventArgs e)
    {
        if (SplitView.IsPaneOpen)
        {
            SplitView.IsPaneOpen = false;
            Expand.Content = ">";
        }
        else
        {
            SplitView.IsPaneOpen = true;
            Expand.Content = "<";
        }

    }
    
}