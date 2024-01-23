using System;
using System.Collections.Generic;
using System.Diagnostics;
using Avalonia.Controls;
using LiteDB;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Platform.Storage;
using Config.Net;
using dmtools.Resources;
using dmtools.ViewModels;

namespace dmtools.Views;

public interface Profile
{
    public int ProfileID { get; set; }
    public string ProfileName { get; set; }
}

public partial class MainWindow : Window
{
    public static event EventHandler SizzeChanged;

    public MainWindow()
    {
        InitializeComponent();
        System.IO.Directory.CreateDirectory("Data");
        System.IO.Directory.CreateDirectory("Settings");
        System.IO.Directory.CreateDirectory("Music");
        System.IO.Directory.CreateDirectory("Music/Amb");
        System.IO.Directory.CreateDirectory("Music/Mus");
        System.IO.Directory.CreateDirectory("Music/Snd");
        System.IO.Directory.CreateDirectory("Images");
        InitProf();
    }
    Profile profile = new ConfigurationBuilder<Profile>().UseIniFile("Profile.ini").Build();
    public void InitProf()
    {
        int i = 1;
        DirectoryInfo prof = new DirectoryInfo("Settings");
        foreach (var p in prof.GetFiles())
        {
            ISettings settings = new ConfigurationBuilder<ISettings>().UseIniFile(p.FullName).Build();
            ToggleButton tb = new ToggleButton();
            settings.DataPath = "Data/" + p.Name;
            settings.ID = i;
            tb.Name = i.ToString();
            tb.Content = settings.Profile;
            tb.HorizontalAlignment = HorizontalAlignment.Stretch;
            if (profile.ProfileID == i)
            {
                tb.IsChecked = true;
            }
            tb.IsCheckedChanged += ProfileChanger;
            i++;
            ProfilesButtons.Children.Add(tb);
        }
        ProfSwitcher.Header = profile.ProfileName;
        profIDd.Text = profile.ProfileID.ToString();
        Button add = new Button();
        add.HorizontalAlignment = HorizontalAlignment.Stretch;
        add.Content = "New Profile";
        add.Click += AddOnClick;
        ProfilesButtons.Children.Add(add);
    }

    private void AddOnClick(object? sender, RoutedEventArgs e)
    {
        bool x = true;
        NewProfile np = new NewProfile(x);
        np.ShowDialog(this);
    }

    private void ProfileChanger(object? sender, RoutedEventArgs e)
    {
        profile.ProfileID = Convert.ToInt32((sender as ToggleButton).Name);
        profile.ProfileName = (sender as ToggleButton).Content.ToString();
        Process p = Process.GetCurrentProcess();    
        Process.Start(p.ProcessName + ".exe");
        if (Avalonia.Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            Thread.Sleep(1000);
            desktop.Shutdown();
        }
    }

    public void Control_OnSizeChanged(object? sender, SizeChangedEventArgs e)
    {
        if (!this.IsLoaded)
        {
            return;
        }
        SizzeChanged(sender, e);
    }

    private void Expand_OnClick(object? sender, RoutedEventArgs e)
    {
        if (SplitView.IsPaneOpen)
        {
            SplitView.IsPaneOpen = false;
            ProfSwitcher.IsVisible = false;
            ProfPrev1.IsVisible = true;
            Expand.Content = ">";
        }
        else
        {
            SplitView.IsPaneOpen = true;
            ProfSwitcher.IsVisible = true;
            ProfPrev1.IsVisible = false;
            Expand.Content = "<";
        }

    }
    
}