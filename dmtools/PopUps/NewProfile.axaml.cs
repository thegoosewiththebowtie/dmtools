using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
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
        int num;
        if (prof.GetFiles().Length == 0)
        {
            profile.ProfileID = 1;
            num = 1;
        }
        else
        {
            num = Convert.ToInt32(prof.GetFiles()[prof.GetFiles().Length-1].Name.Replace("q0", "").Replace(".ini", "")) +1;
            profile.ProfileID = num;
            profile.ProfileName = profilename.Text;
        }
        var tst = $"Settings/q0{num}.ini";
        ISettings settings = new ConfigurationBuilder<ISettings>().UseIniFile(tst).Build();
        settings.DataPath = "Data/q0" + (num);
        settings.Profile = profilename.Text;
        settings.Player = playername.Text;
        shouldi = false;
        (addpfp.Source as Bitmap).Save($"Data/q0{num}.bmp");
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

    private async void Addpfp_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        var topLevel = TopLevel.GetTopLevel(this);
        var result = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions(){ Title = "Choose profile picture", 
            FileTypeFilter = new[] { FilePickerFileTypes.ImageAll }});
        if (result.Count == 0)
        {
            return;
        }
        Bitmap pfp = new Bitmap(result[0].Path.ToString().Replace("file:///", ""));
        addpfp.Source = pfp;

    }

    private void Import_OnClick(object? sender, RoutedEventArgs e)
    {
        SettingsView.Importing();
    }
}