using System.Diagnostics;
using System.IO;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Config.Net;
using dmtools.ViewModels;
using dmtools.Views;

namespace dmtools.PopUps;

public partial class NewProfile : Window
{
    public bool shouldi { get; set; }
    public NewProfile(bool x)
    {
        InitializeComponent();
        if (x == false)
        {
            Cancel.IsEnabled = false;
            shouldi = true;
        }
    }

    private void Add_OnClick(object? sender, RoutedEventArgs e)
    {
        Profile profile = new ConfigurationBuilder<Profile>().UseIniFile("Profile.ini").Build();
        DirectoryInfo prof = new DirectoryInfo("Settings");
        if (prof.GetFiles().Length == 0)
        {
            profile.ProfileID = 1;
        }
        else
        {
            profile.ProfileID = prof.GetFiles().Length + 1;
            profile.ProfileName = profilename.Text;
        }
        ISettings settings = new ConfigurationBuilder<ISettings>().UseIniFile("Settings/q0" + (prof.GetFiles().Length + 1) +".ini").Build();
        settings.DataPath = "Data/q0" + (prof.GetFiles().Length + 1) + ".db";
        settings.Profile = profilename.Text;
        settings.Player = playername.Text;
        shouldi = false;
        this.Close();
        Process p = Process.GetCurrentProcess();    
        Process.Start(p.ProcessName + ".exe");
        if (Avalonia.Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            Thread.Sleep(1000);
            desktop.Shutdown();
        }
    }

    private void Cancel_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void Window_OnClosing(object? sender, WindowClosingEventArgs e)
    {
        if (shouldi == true && e.CloseReason != WindowCloseReason.OwnerWindowClosing && Avalonia.Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow.Close();
        }
    }
}