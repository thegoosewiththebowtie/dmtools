using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using dmtools.ViewModels;
using dmtools.Views;
using System;
using System.Configuration;
using System.Globalization;
using System.Net.Mime;
using Avalonia.Controls;
using AvaloniaWebView;

namespace dmtools;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }
    public override void RegisterServices()
    {
        base.RegisterServices();
     
        // if you use only WebView  
        AvaloniaWebViewBuilder.Initialize(default);
    }
    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel()
            };
            desktop.ShutdownMode = Avalonia.Controls.ShutdownMode.OnMainWindowClose;
        }
        base.OnFrameworkInitializationCompleted();
    }
}
