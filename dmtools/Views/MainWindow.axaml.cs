using System;
using System.Collections.Generic;
using System.Diagnostics;
using Avalonia.Controls;
using System.IO;
using System.Threading;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using AvaloniaWebView;
using Config.Net;
using dmtools.PopUps;

namespace dmtools.Views;

public interface Profile
{
    public int ProfileID { get; set; }
    public string ProfileName { get; set; }
}

public partial class MainWindow : Window
{
    public static event EventHandler SizzeChanged;
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
        HomeView.YTC += YTLInit;
        YTInit();
        if (File.Exists($"Data/q0{settings.ID}.bmp"))
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
                YTControl.IsVisible = false;
                home.IsVisible = false;
            }
            else if (tb.Name == "Home" && tb.IsChecked == true)
            {
                home.IsVisible = true;
                if (isactiveyt == true)
                {
                    YTControl.IsVisible = true;
                }
            }
        }
    }
    
    
    //YT
    public List<WebView> browsers { get; set; }
    public bool isactiveyt { get; set; }
    public void YTLInit(object? sender, EventArgs eventArgs)
    {
        if (!this.home.IsLoaded)
        {
            return;
        }
        if ((sender as TabControl).SelectedIndex != 1)
        {
            isactiveyt = false;
            YTControl.IsVisible = false;
            return;
        }
        YTControl.IsVisible = true;
        isactiveyt = true;
        if (settings.BrLink0 != null && settings.BrLink0.Contains("https://www.youtube.com/embed/") == true)
        {
            BrowserBox0.Text = settings.BrLink0.Replace("https://www.youtube.com/embed/", "https://youtu.be/");
        }
        if (settings.BrLink1 != null && settings.BrLink1.Contains("https://www.youtube.com/embed/"))
        {
            BrowserBox1.Text = settings.BrLink1.Replace("https://www.youtube.com/embed/", "https://youtu.be/");
        }
        if (settings.BrLink2 != null && settings.BrLink2.Contains("https://www.youtube.com/embed/"))
        {
            BrowserBox2.Text = settings.BrLink2.Replace("https://www.youtube.com/embed/", "https://youtu.be/");
        }
        if (settings.BrLink3 != null && settings.BrLink3.Contains("https://www.youtube.com/embed/")) 
        {
            BrowserBox3.Text = settings.BrLink3.Replace("https://www.youtube.com/embed/", "https://youtu.be/");
        }
        
    }
    public void YTInit()
    {
        settings = new ConfigurationBuilder<ISettings>().UseIniFile("Settings/q0" + profid +".ini").Build();
        browsers = new List<WebView>()
        {
            new WebView(),
            new WebView(),
            new WebView(),
            new WebView()
        };
    }

    public void BrowserButton0_OnClick(object? sender, RoutedEventArgs e)
    {
        //https://www.youtube.com/embed/
        //https://youtu.be/
        switch ((sender as Button).Name)
        {
            case "BrowserButton0":
                if (BrowserBox0.Text == null || !BrowserBox0.Text.Contains("https://youtu.be/"))
                {
                    var noo = new NO("Link has to start with 'https://youtu.be/' "); 
                    noo.Height = 250;
                    noo.ShowDialog(this);
                   break;
                }
                settings.BrLink0 = BrowserBox0.Text.Replace("https://youtu.be/", "https://www.youtube.com/embed/");
                if (settings.BrLink0 == null || !settings.BrLink0.Contains("https://www.youtube.com/embed/"))
                {
                    break;
                }
                browsers[0].Url = new Uri(settings.BrLink0);
                Browser0.Child = browsers[0];
                break;
            case "BrowserButton1":
                if (BrowserBox0.Text == null || !BrowserBox1.Text.Contains("https://youtu.be/"))
                {
                    var noo = new NO("Link has to start with 'https://youtu.be/' "); 
                    noo.Height = 250;
                    noo.ShowDialog(this);
                    break;
                }
                settings.BrLink1 = BrowserBox1.Text.Replace("https://youtu.be/", "https://www.youtube.com/embed/");
                if (settings.BrLink1 == null || !settings.BrLink1.Contains("https://www.youtube.com/embed/"))
                {
                    break;
                }
                browsers[1].Url = new Uri(settings.BrLink1);
                Browser1.Child = browsers[1];
                break;
            case "BrowserButton2":
                if (BrowserBox0.Text == null || !BrowserBox2.Text.Contains("https://youtu.be/"))
                {
                    var noo = new NO("Link has to start with 'https://youtu.be/' ");
                    noo.Height = 250;
                    noo.ShowDialog(this);
                   break;
                }
                settings.BrLink2 = BrowserBox2.Text.Replace("https://youtu.be/", "https://www.youtube.com/embed/");
                if (settings.BrLink2 == null || !settings.BrLink2.Contains("https://www.youtube.com/embed/"))
                {
                    break;
                }
                browsers[2].Url = new Uri(settings.BrLink2);
                Browser2.Child = browsers[2];
                break;
            case "BrowserButton3":
                if (BrowserBox0.Text == null || !BrowserBox3.Text.Contains("https://youtu.be/"))
                {
                    var noo = new NO("Link has to start with 'https://youtu.be/' "); 
                    noo.Height = 250;
                    noo.ShowDialog(this);
                    break;
                }
                settings.BrLink3 = BrowserBox3.Text.Replace("https://youtu.be/", "https://www.youtube.com/embed/");
                if (settings.BrLink3 == null || !settings.BrLink3.Contains("https://www.youtube.com/embed/"))
                {
                    break;
                }
                browsers[3].Url = new Uri(settings.BrLink3);
                Browser3.Child = browsers[3];
                break;
        }
    }

    private void InputElement_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        throw new Exception();
    }
}