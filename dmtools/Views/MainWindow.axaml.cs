using System;
using System.Collections.Generic;
using System.Diagnostics;
using Avalonia.Controls;
using System.IO;
using System.Linq;
using System.Threading;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Threading;
using AvaloniaWebView;
using Config.Net;
using dmtools.PopUps;
using NAudio.Wave;

namespace dmtools.Views;

public interface Profile
{
    public int ProfileID { get; set; }
    public string ProfileName { get; set; }
}

public partial class MainWindow : Window
{
    public static event EventHandler SizzeChanged;
    public static event EventHandler<int> TabC;
    public ISettings settings { get; set; }
    public int profid { get; set; }
    public MainWindow()
    {
        InitializeComponent();
        System.IO.Directory.CreateDirectory("Data");
        System.IO.Directory.CreateDirectory("GlossData");
        System.IO.Directory.CreateDirectory("Settings");
        System.IO.Directory.CreateDirectory("Music");
        System.IO.Directory.CreateDirectory("Music/Amb");
        System.IO.Directory.CreateDirectory("Music/Mus");
        System.IO.Directory.CreateDirectory("Music/Snd");
        System.IO.Directory.CreateDirectory("Images");
        InitProf();
        
        if (settings != null && File.Exists($"Data/q0{settings.ID}.bmp"))
        {
            pfp.Source = new Avalonia.Media.Imaging.Bitmap($"Data/q0{settings.ID}.bmp");
        }
    }
   
    Profile profile = new ConfigurationBuilder<Profile>().UseIniFile("Profile.ini").Build();
    public void InitProf()
    {
        DirectoryInfo prof = new DirectoryInfo("Settings");
        foreach (var p in prof.GetFiles())
        {
            ISettings settings = new ConfigurationBuilder<ISettings>().UseIniFile(p.FullName).Build();
            ToggleButton tb = new ToggleButton();
            settings.DataPath = "Data/" + p.Name;
            settings.ID = Convert.ToInt32(p.Name.Replace("q0", "").Replace(".ini", ""));
            tb.Name = settings.ID.ToString();
            tb.Content = settings.Profile;
            tb.HorizontalAlignment = HorizontalAlignment.Stretch;
            if (profile.ProfileID == settings.ID)
            {
                tb.IsChecked = true;
            }
            tb.IsCheckedChanged += ProfileChanger;
            ProfilesButtons.Children.Add(tb);
        }
        profid = profile.ProfileID;
        if (profid == 0 || profid == null)
        {
            return;
        }
        settings = new ConfigurationBuilder<ISettings>().UseIniFile("Settings/q0" + profid +".ini").Build();
        prnm.Text = settings.Profile;
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
            state.IsVisible = false;
            SplitView.IsPaneOpen = false;
            ProfSwitcher.IsVisible = false;
            ProfPrev1.IsVisible = true;
            Expand.Content = ">";
        }
        else
        {
            state.IsVisible = true;
            SplitView.IsPaneOpen = true;
            ProfSwitcher.IsVisible = true;
            ProfPrev1.IsVisible = false;
            Expand.Content = "<";
        }
    }

    
    
    
    
    
    
    
    private void Home_OnClick(object? sender, RoutedEventArgs e)
    {
        if ((sender as ToggleButton).IsChecked == false)
        {
            (sender as ToggleButton).IsChecked = true;
            return;
        }
        List<ToggleButton> mtb = new List<ToggleButton>() { Home, Gens, Glossary, Set, Abt };
        foreach (var tb in mtb)
        {
            if (tb != (sender as ToggleButton))
            {
                tb.IsChecked = false;
            }
            if (tb.Name == "Home" && tb.IsChecked == false)
            {
                media.IsVisible = false;
                home.IsVisible = false;
            }
            else if (tb.Name == "Home" && tb.IsChecked == true)
            {
                home.IsVisible = true;
            }
        }
    }
}