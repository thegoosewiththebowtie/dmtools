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
        HomeView.YTC += YTLInit;
        YTInit();
        uuu.SelectionChanged += UuuOnSelectionChanged;
        if (File.Exists($"Data/q0{settings.ID}.bmp"))
        {
            pfp.Source = new Avalonia.Media.Imaging.Bitmap($"Data/q0{settings.ID}.bmp");
        }
    }
    private void UuuOnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        TabC(sender, uuu.SelectedIndex);
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
        VolumeS.Value = settings.Volume;
        prnm.Text = settings.Profile;
        Button add = new Button();
        add.HorizontalAlignment = HorizontalAlignment.Stretch;
        add.Content = "New Profile";
        add.Click += AddOnClick;
        AmbIni();
        SndIni();
        VolumeAmb.Value = settings.VolumeAmb;
        VolumeSnd.Value = settings.VolumeSnd;
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
    
    
    //amb
    public List<WaveOutEvent> IDAmb = new List<WaveOutEvent>();
    public List<DispatcherTimer> IDTim = new List<DispatcherTimer>();
    public List<AudioFileReader> IDDin = new List<AudioFileReader>();
    public void AmbIni()
    {
        var ambf = new DirectoryInfo(settings.AmbPath);
        Ambience.Children.Clear();
        int i = 0;
        if (ambf.GetFiles().Count() == 0)
        {
            TextBlock n = new TextBlock()
            {
                Text = Application.Current.FindResource("nofiles").ToString(),
                FontSize = 46
            };
            Ambience.Children.Add(n);
            return;
        }
        foreach (var amb in ambf.GetFiles())
        {
            if (amb.Name.EndsWith(".mp3")) 
            {
                ToggleButton tg0 = new ToggleButton()
                {
                        Name = amb.Name + "_" + i,
                        Content = amb.Name.Replace(".mp3", ""),
                        Width = 150,
                        Height = 50,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        FontSize = 16,
                        Margin = new Thickness(5)
                };
                i++;
                tg0.IsCheckedChanged += AmbChangeState;
                Ambience.Children.Add(tg0);
                AudioFileReader af = new AudioFileReader(settings.AmbPath + "/" + amb.Name);
                IDDin.Add(af);
                WaveOutEvent ap = new WaveOutEvent();
                IDAmb.Add(ap);
                DispatcherTimer dt = new DispatcherTimer();
                IDTim.Add(dt);
            }
        }
    }
    private void AmbChangeState(object? sender, RoutedEventArgs e)
    {
        int tick = 0;
        int ID = Convert.ToInt32((sender as ToggleButton).Name.Split("_")[1]);
        IDDin[ID].Volume =(float)settings.VolumeAmb/10;
        IDTim[ID].Tick += AmbControl;
        if ((sender as ToggleButton).IsChecked == true)
        {
            if (IDAmb[ID].PlaybackState == PlaybackState.Stopped)
            {
                IDAmb[ID].Init(IDDin[ID]);
            }
            IDAmb[ID].Play();
            IDTim[ID].Start();
        }
        else
        {
            IDAmb[ID].Pause();
            IDTim[ID].Stop();
        }
        void AmbControl(object? o, EventArgs eventArgs)
        {
            if (tick < 99)
            {
                tick = Convert.ToInt32((IDDin[ID].CurrentTime / IDDin[ID].TotalTime) * 100);
            }
            else
            {
                tick = 0;
                IDAmb[ID].Stop();
                IDDin[ID].CurrentTime = new TimeSpan(0);
                IDAmb[ID].Init(IDDin[ID]);
                IDAmb[ID].Play();
            }
        }
    }
    private void RangeBase_OnValueChanged(object? sender, RangeBaseValueChangedEventArgs e)
    {
        foreach (var pl in IDDin)
        {
            settings.VolumeAmb = (sender as Slider).Value;
            pl.Volume = (float)settings.VolumeAmb/10;
        }
    }
    
    
    
    //sounds
    public List<WaveOutEvent> sIDAmb = new List<WaveOutEvent>();
    public List<DispatcherTimer> sIDTim = new List<DispatcherTimer>();
    public List<AudioFileReader> sIDDin = new List<AudioFileReader>();
    public void SndIni()
    {
        var sndf = new DirectoryInfo(settings.SndPath);
        Sounds.Children.Clear();
        if (sndf.GetFiles().Length != 0)
        {
            int c = 0;
            foreach (var snd in sndf.GetFiles())
            {
                if (snd.Name.EndsWith(".mp3"))
                {
                    ToggleButton tg0 = new ToggleButton()
                    {
                        Name = c.ToString(),
                        Content = snd.Name.Replace(".mp3", ""),
                        Width = 150,
                        Height = 50,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        FontSize = 16,
                        Margin = new Thickness(5)
                    };
                    tg0.IsCheckedChanged += boom;
                    Sounds.Children.Add(tg0);
                    c++;
                    AudioFileReader af = new AudioFileReader(snd.FullName);
                    af.Volume = (float)settings.VolumeSnd;
                    sIDDin.Add(af);
                    WaveOutEvent ap = new WaveOutEvent();
                    sIDAmb.Add(ap);
                    DispatcherTimer dt = new DispatcherTimer();
                    sIDTim.Add(dt);
                }
            }
        }
        else
        {
            TextBlock n = new TextBlock()
            {
              Text = Application.Current.FindResource("nofiles").ToString(),
                FontSize = 46
            };
            Sounds.Children.Add(n);
        }
    }
    private void boom(object? sender, RoutedEventArgs e)
    {
            int tick = 0;
            int ID = Convert.ToInt32((sender as ToggleButton).Name);
            sIDDin[ID].Volume =(float)settings.VolumeAmb/10;
            sIDTim[ID].Tick += SndControl;
            if ((sender as ToggleButton).IsChecked == true)
            {
                if (sIDAmb[ID].PlaybackState == PlaybackState.Stopped)
                {
                    sIDAmb[ID].Init(sIDDin[ID]);
                }
                sIDAmb[ID].Play();
                sIDTim[ID].Start();
            }
            else
            {
                sIDAmb[ID].Pause();
                sIDTim[ID].Stop();
            }
            void SndControl(object? o, EventArgs eventArgs)
            {
                if (tick < 99)
                {
                    tick = Convert.ToInt32((sIDDin[ID].CurrentTime / sIDDin[ID].TotalTime) * 100);
                }
                else
                {
                    tick = 0;
                    sIDAmb[ID].Stop();
                    sIDDin[ID].CurrentTime = new TimeSpan(0);
                }
            }
        
    }
    private void VolumeSnd_OnValueChanged(object? sender, RangeBaseValueChangedEventArgs e)
    {
        foreach (var pl in sIDDin)
        {
            settings.VolumeSnd = (sender as Slider).Value;
            pl.Volume = (float)settings.VolumeSnd/10;
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
    
    //msu
    public WaveOutEvent mainPlayer = new WaveOutEvent();
    public Random rndm = new Random();
    public AudioFileReader auf;
    public DispatcherTimer mainTime { get; set; }
    public string NowPlaying { get; set; }
    public string CurrentSongPath { get; set; }
    public TimeSpan TotalLength { get; set; }
    private void MoodChooser_OnDropDownOpened(object? sender, EventArgs e)
    {
        var MusDir = new DirectoryInfo(settings.MusPath).GetDirectories();
        MoodChooser.Items.Clear();
        if (MusDir.Count() == 0)
        {
            return;
        }
        foreach (var musdir in MusDir)
        {
            var name = musdir.Name;
            MoodChooser.Items.Add(name);
        }
    }
    private void MoodChooser_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var sel = new DirectoryInfo(settings.MusPath + "/" + MoodChooser.SelectedItem);
        SongChooser.Items.Clear();
        foreach (var name in sel.GetFiles())
        {
            if (name.Name.EndsWith(".mp3"))
            {
                SongChooser.Items.Add(name.Name);
            }
        }
        if (MoodChooser.SelectedItem != null)
        {
            SongChooser.SelectedIndex = rndm.Next(0, SongChooser.ItemCount + 1);
        }
    }
    private void SongChooser_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (SongChooser.SelectedItem == null)
        {
            return;
        }
        CurrentSongPath = nowPlaying.Text = settings.MusPath + "/" + MoodChooser.SelectedItem + "/" + SongChooser.SelectedItem;
        MusicControl();
    }
    private void Stop_OnClick(object? sender, RoutedEventArgs e)
    {
        mainPlayer.Stop();
        mainTime.Stop();
        SongChooser.SelectedItem = null;
        MusState.Text = "00:00";
        nowPlaying.Text = "";
    }
    private void Pause_OnClick(object? sender, RoutedEventArgs e)
    {
        MusicControl();
    }
    private void Next_OnClick(object? sender, RoutedEventArgs e)
    {
        int i;
        do
        {
            i = rndm.Next(0, SongChooser.ItemCount+1);
            if (i < 10)
            {
                break;
            }
        } while (SongChooser.SelectedIndex == i);
        SongChooser.SelectedIndex = i;
    }
    public void MusicControl()
    {
        if (!File.Exists(CurrentSongPath))
        {
            return;
        }
        if (mainPlayer.PlaybackState == PlaybackState.Stopped)
        {
                auf = new AudioFileReader(CurrentSongPath);
                NowPlaying = CurrentSongPath;
                TotalLength = auf.TotalTime;
                mainPlayer.Init(auf);
                mainPlayer.Play();
                mainTime = new DispatcherTimer();
                mainTime.Tick += MainTimeOnTick;
                mainTime.Start();
                nowPlaying.Text = SongChooser.SelectedItem.ToString();
                auf.Volume = (float)settings.Volume/10;
                Pause.IsChecked = true;
        }
        else if (mainPlayer.PlaybackState == PlaybackState.Playing && NowPlaying == CurrentSongPath)
        {
                mainPlayer.Pause();
                mainTime.Stop();
                nowPlaying.Text = nowPlaying.Text = SongChooser.SelectedItem.ToString();
                Pause.IsChecked = false;
        }
        else if (mainPlayer.PlaybackState == PlaybackState.Paused && NowPlaying == CurrentSongPath)
        {
                mainPlayer.Play();
                mainTime.Start();
                nowPlaying.Text = SongChooser.SelectedItem.ToString();
                Pause.IsChecked = true;
        }
        else if (NowPlaying != CurrentSongPath)
        {
                mainTime.Stop();
                mainPlayer.Stop();
                mainPlayer.Dispose();
                auf = new AudioFileReader(CurrentSongPath);
                NowPlaying = CurrentSongPath;
                mainPlayer.Init(auf);
                mainPlayer.Play();
                mainTime = new DispatcherTimer();
                mainTime.Tick += MainTimeOnTick;
                mainTime.Start();
                nowPlaying.Text = SongChooser.SelectedItem.ToString();
                Pause.IsChecked = true;
        }
    }
    private void Volume_OnPropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
    {
        if (auf == null)
        {
            return;
        }
        settings.Volume = (sender as Slider).Value;
        auf.Volume = (float)settings.Volume/10;
    }
    private void MainTimeOnTick(object? sender, EventArgs e)
    {
        if (mainPlayer.PlaybackState == PlaybackState.Playing)
        {
            MusState.Text = ($"{auf.CurrentTime.Minutes.ToString("00")}:{auf.CurrentTime.Seconds.ToString("00")}" +
                             $"/{auf.TotalTime.Minutes.ToString("00")}:{auf.TotalTime.Seconds.ToString("00")}");
            MusProg.Value = (auf.CurrentTime / auf.TotalTime) * 100;
        }
        else
        {
            MusState.Text = "00:00";
            MusProg.Value = 0;
            nowPlaying.Text = "";
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
        uuu.SelectedIndex = 1;
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
        //https://www.youtube.com/watch?v=
        switch ((sender as Button).Name)
        {
            case "BrowserButton0":
                if (BrowserBox0.Text == null || !BrowserBox0.Text.Contains("https://youtu.be/") || !BrowserBox0.Text.Contains("https://www.youtube.com/watch?v="))
                {
                    var noo = new NO("Link has to start with 'https://youtu.be/' or https://www.youtube.com/watch?v= "); 
                    noo.Height = 250;
                    noo.ShowDialog(this);
                   break;
                }
                settings.BrLink0 = BrowserBox0.Text.Replace("https://youtu.be/", "https://www.youtube.com/embed/").Replace("https://www.youtube.com/watch?v=", "https://www.youtube.com/embed/");
                if (settings.BrLink0 == null || !settings.BrLink0.Contains("https://www.youtube.com/embed/"))
                {
                    break;
                }
                browsers[0].Url = new Uri(settings.BrLink0);
                Browser0.Child = browsers[0];
                break;
            case "BrowserButton1":
                if (BrowserBox0.Text == null || !BrowserBox1.Text.Contains("https://youtu.be/") || !BrowserBox0.Text.Contains("https://www.youtube.com/watch?v="))
                {
                    var noo = new NO("Link has to start with 'https://youtu.be/' "); 
                    noo.Height = 250;
                    noo.ShowDialog(this);
                    break;
                }
                settings.BrLink1 = BrowserBox1.Text.Replace("https://youtu.be/", "https://www.youtube.com/embed/").Replace("https://www.youtube.com/watch?v=", "https://www.youtube.com/embed/");
                if (settings.BrLink1 == null || !settings.BrLink1.Contains("https://www.youtube.com/embed/"))
                {
                    break;
                }
                browsers[1].Url = new Uri(settings.BrLink1);
                Browser1.Child = browsers[1];
                break;
            case "BrowserButton2":
                if (BrowserBox0.Text == null || !BrowserBox2.Text.Contains("https://youtu.be/") || !BrowserBox0.Text.Contains("https://www.youtube.com/watch?v="))
                {
                    var noo = new NO("Link has to start with 'https://youtu.be/' ");
                    noo.Height = 250;
                    noo.ShowDialog(this);
                   break;
                }
                settings.BrLink2 = BrowserBox2.Text.Replace("https://youtu.be/", "https://www.youtube.com/embed/").Replace("https://www.youtube.com/watch?v=", "https://www.youtube.com/embed/");
                if (settings.BrLink2 == null || !settings.BrLink2.Contains("https://www.youtube.com/embed/"))
                {
                    break;
                }
                browsers[2].Url = new Uri(settings.BrLink2);
                Browser2.Child = browsers[2];
                break;
            case "BrowserButton3":
                if (BrowserBox0.Text == null || !BrowserBox3.Text.Contains("https://youtu.be/") || !BrowserBox0.Text.Contains("https://www.youtube.com/watch?v="))
                {
                    var noo = new NO("Link has to start with 'https://youtu.be/' "); 
                    noo.Height = 250;
                    noo.ShowDialog(this);
                    break;
                }
                settings.BrLink3 = BrowserBox3.Text.Replace("https://youtu.be/", "https://www.youtube.com/embed/").Replace("https://www.youtube.com/watch?v=", "https://www.youtube.com/embed/");
                if (settings.BrLink3 == null || !settings.BrLink3.Contains("https://www.youtube.com/embed/"))
                {
                    break;
                }
                browsers[3].Url = new Uri(settings.BrLink3);
                Browser3.Child = browsers[3];
                break;
        }
    }



}