using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using dmtools.Resources;
using LiteDB;
using System.IO;
using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Platform;
using NAudio;
using Avalonia.Platform.Storage;
using Avalonia.Platform.Storage.FileIO;
using Avalonia.Threading;
using NAudio.Wave;
using Config.Net;


namespace dmtools.Views;

public class FightData
{
    public int Initiative { get; set; }
    public string Name { get; set; }
    public string Player { get; set; }
    public int ArCl { get; set; }
    public string IDP { get; set; }
    public int Health { get; set; }
    public string IDM { get; set; }
}
public class PlayerCharacter
{
    public int ID { get; set; }
    public string FirstName { get; set; }
    public string OtherName { get; set; }
    public string Player { get; set; }
    public string Race { get; set; }
    public string Class { get; set; }
    public string Backgroundd { get; set; }
    public string Alligment { get; set; }
    public string Notes { get; set; }
    public string Strength { get; set; }
    public string Dexterity { get; set; }
    public string Constitution { get; set; }
    public string Intelligence { get; set; }
    public string Wisdom { get; set; }
    public string Charisma { get; set; }
    public string Level { get; set; }
    public string Experience { get; set; }
    public string Health { get; set; }
    public string ArmorClass { get; set; }
    
    public int Insp { get; set; }
}
public interface ISettings
{
    string MusPath { get; set; }
    string AmbPath { get; set; }
    string SndPath { get; set; }
    double Volume { get; set; }
    double VolumeAmb { get; set; }
}
public partial class HomeView : UserControl
{
    public HomeView()
    {
        InitializeComponent();
        AmbIni();
        SndIni();
        SetSetup();
        VolumeS.Value = settings.Volume;
        MainWindow.SizzeChanged += SizeChange;
        Sure.Delete += pcupdate;
        Sure.Delete += SizeChange;
        DashFightUp();
    }
    ISettings settings = new ConfigurationBuilder<ISettings>().UseIniFile("Settings.ini").Build();

    public void SetSetup()
    {
        if (settings.MusPath == "")
        {
            settings.MusPath = "Music/Mus";
        }

        if (settings.AmbPath == "")
        {
            settings.AmbPath = "Music/Amb";
        }

        if (settings.SndPath == "")
        {
            settings.SndPath = "Music/Snd";
        }

        if (settings.Volume == 0)
        {
            settings.Volume = 10;
        }
        if (settings.VolumeAmb == 0)
        {
            settings.VolumeAmb = 10;
        }
    }
    
    //dashboard
    
    //fight
    public void DashFightUp()
    {
        using (var LdbPC = new LiteDatabase("LdbforPC.db"))
        {
            FightGrid.ItemsSource = null;
            var playChar = LdbPC.GetCollection<PlayerCharacter>();
            List<FightData> fin = new List<FightData>();
            foreach (var pc in playChar.FindAll())
            {
                FightData fightList = new FightData()
                {
                    Initiative = 0,
                    Name =  pc.FirstName,
                    Player = pc.Player,
                    ArCl = Convert.ToInt32(pc.ArmorClass),
                    IDM = "Minus_" + pc.ID.ToString(),
                    Health = Convert.ToInt32(pc.Health),
                    IDP = "Plus_" + pc.ID.ToString(),
                };
                fin.Add(fightList);
            }
            FightGrid.ItemsSource = fin;
        }
    }

    public void PlusMinusHP(object? sender, RoutedEventArgs e)
    {
        var data = (sender as Button).Name.Split("_");
        int ID = Convert.ToInt32(data[1]);
        string pm = data[0];
        int hP = 0;
        using (var LdbPC = new LiteDatabase("LdbforPC.db"))
        {
            var pccol = LdbPC.GetCollection<PlayerCharacter>();
            if (pm == "Plus")
            {
                hP = Convert.ToInt32(pccol.FindById(ID).Health) + 1;
            }
            else if (pm == "Minus")
            {
                hP = Convert.ToInt32(pccol.FindById(ID).Health) - 1;
            }

            var HP = pccol.FindById(ID);
            HP.Health = hP.ToString();
            pccol.Update(HP);
        }
        DashFightUp();
    }
    //media
    
    
    //music
    public WaveOutEvent mainPlayer = new WaveOutEvent();
    public Random rndm = new Random();
    public AudioFileReader auf;
    public DispatcherTimer mainTime { get; set; }
    public string NowPlaying { get; set; } = "0";
    public TimeSpan TotalLength { get; set; }
    private async void MoodChooser_OnDropDownOpened(object? sender, EventArgs e)
    {
        var MusDir = new DirectoryInfo(settings.MusPath).GetDirectories();
        MoodChooser.Items.Clear();
        if (MusDir.Count() != 0)
        {
            foreach (var musdir in MusDir)
            {
                var name = musdir.Name;
                MoodChooser.Items.Add(name);
            }
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
            SongChooser.SelectedIndex = rndm.Next(0, SongChooser.ItemCount);
        }
    }
    private void SongChooser_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (SongChooser.SelectedItem != null)
        {
            MusicControl();
        }
    }
    private void Stop_OnClick(object? sender, RoutedEventArgs e)
    {
        mainPlayer.Stop();
        mainTime.Stop();
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
            i = rndm.Next(0, SongChooser.ItemCount);
        } while (SongChooser.SelectedIndex == i);
        SongChooser.SelectedIndex = i;
    }
    public void MusicControl()
    {
        Uri.TryCreate(settings.MusPath + "/" + MoodChooser.SelectedItem + "/" + SongChooser.SelectedItem,
            UriKind.RelativeOrAbsolute, out Uri s);
        if (s.IsFile)
        {
            if (mainPlayer.PlaybackState == PlaybackState.Stopped)
            {
                auf = new AudioFileReader(settings.MusPath + "/" + MoodChooser.SelectedItem + "/" + SongChooser.SelectedItem);
                NowPlaying = settings.MusPath + "/" + MoodChooser.SelectedItem + "/" + SongChooser.SelectedItem;
                TotalLength = auf.TotalTime;
                mainPlayer.Init(auf);
                mainPlayer.Play();
                mainTime = new DispatcherTimer();
                mainTime.Tick += MainTimeOnTick;
                mainTime.Start();
                nowPlaying.Text = SongChooser.SelectedItem.ToString();
                mainPlayer.Volume = (float)settings.Volume/10;
            }
            else if (mainPlayer.PlaybackState == PlaybackState.Playing && NowPlaying == settings.MusPath + "/" + MoodChooser.SelectedItem + "/" + SongChooser.SelectedItem)
            {
                mainPlayer.Pause();
                mainTime.Stop();
                nowPlaying.Text = nowPlaying.Text = SongChooser.SelectedItem.ToString();
            }
            else if (mainPlayer.PlaybackState == PlaybackState.Paused && NowPlaying == settings.MusPath + "/" + MoodChooser.SelectedItem + "/" + SongChooser.SelectedItem)
            {
                mainPlayer.Play();
                mainTime.Start();
                nowPlaying.Text = SongChooser.SelectedItem.ToString();
            }
            else if (NowPlaying != settings.MusPath + "/" + MoodChooser.SelectedItem + "/" + SongChooser.SelectedItem)
            {
                mainTime.Stop();
                mainPlayer.Stop();
                mainPlayer.Dispose();
                auf = new AudioFileReader(settings.MusPath + "/" + MoodChooser.SelectedItem + "/" + SongChooser.SelectedItem);
                NowPlaying = settings.MusPath + "/" + MoodChooser.SelectedItem + "/" + SongChooser.SelectedItem;
                mainPlayer.Init(auf);
                mainPlayer.Play();
                mainTime = new DispatcherTimer();
                mainTime.Tick += MainTimeOnTick;
                mainTime.Start();
                nowPlaying.Text = SongChooser.SelectedItem.ToString();
            }
        }
    }
    private void Volume_OnPropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
    {
        settings.Volume = (sender as Slider).Value;
        mainPlayer.Volume = (float)settings.Volume/10;
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
    


    
    
    
    //amb
    public List<WaveOutEvent> IDAmb = new List<WaveOutEvent>();
    public List<DispatcherTimer> IDTim = new List<DispatcherTimer>();
    public List<AudioFileReader> IDDin = new List<AudioFileReader>();
    public void AmbIni()
    {
        var ambf = new DirectoryInfo(settings.AmbPath);
        Ambience.Children.Clear();
        int i = 0;
        if (ambf.GetFiles().Count() != 0)
        {

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
                        FontSize = 24,
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
        else
        {
            TextBlock n = new TextBlock()
            {
                Text = "no files",
                FontSize = 46
            };
            Ambience.Children.Add(n);
        }
    }
    private void AmbChangeState(object? sender, RoutedEventArgs e)
    {
        int tick = 0;
        int ID = Convert.ToInt32((sender as ToggleButton).Name.Split("_")[1]);
        IDAmb[ID].Volume =(float)settings.VolumeAmb/10;
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
    
    
    //sounds
    public void SndIni()
    {
        var sndf = new DirectoryInfo(settings.SndPath);
        Sounds.Children.Clear();
        if (sndf.GetFiles().Length != 0)
        {
            foreach (var snd in sndf.GetFiles())
            {
                if (snd.Name.EndsWith(".mp3"))
                {
                    Button tg0 = new Button()
                    {
                        Name = snd.Name,
                        Content = snd.Name.Replace(".mp3", ""),
                        Width = 150,
                        Height = 50,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        FontSize = 24,
                        Margin = new Thickness(5)
                    };
                    tg0.Click += boom;
                    Sounds.Children.Add(tg0);

                }
            }
        }
        else
        {
            TextBlock n = new TextBlock()
            {
              Text = "no files",
                FontSize = 46
            };
            Sounds.Children.Add(n);
        }
    }

    private void boom(object? sender, RoutedEventArgs e)
    {                    
        AudioFileReader af = new AudioFileReader(settings.AmbPath + "/" + (sender as Button).Name);
        WaveOutEvent ap = new WaveOutEvent();
        ap.Init(af);
        ap.Play();
    }


    //playercharacters
    public void Add(object sender, RoutedEventArgs args)
    {
        using (var LdbPC = new LiteDatabase("LdbforPC.db"))
        {
            var playChar = LdbPC.GetCollection<PlayerCharacter>();
            playChar.Insert(new PlayerCharacter{ });
        }
        pcupdate(sender, args);
        SizeChange(sender, args);
    }
    public void pcupdate(object? sender, EventArgs e)
    {
        wppc.Children.Clear();
        using (var LdbPC = new LiteDatabase("LdbforPC.db"))
        {
            var playChar = LdbPC.GetCollection<PlayerCharacter>();
            foreach (var pc in playChar.FindAll())
            {
                CharForm charForm = new CharForm();
                charForm.ID = pc.ID;
                charForm.FirstName = pc.FirstName;
                charForm.OtherNames = pc.OtherName;
                charForm.Player = pc.Player;
                charForm.Class = pc.Class;
                charForm.Race = pc.Race;
                charForm.Alligment = pc.Alligment;
                charForm.Notes = pc.Notes;
                charForm.Backgroundd = pc.Backgroundd;
                charForm.StrVal = pc.Strength;
                charForm.DexVal = pc.Dexterity;
                charForm.ConVal = pc.Constitution;
                charForm.IntVal = pc.Intelligence;
                charForm.WisVal = pc.Wisdom;
                charForm.ChaVal = pc.Charisma;
                charForm.Level = pc.Level;
                charForm.Exp = pc.Experience;
                charForm.Health = pc.Health;
                charForm.Ac = pc.ArmorClass;
                wppc.Children.Add(charForm);
            }
        }
    }
    private void Players_OnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        pcupdate(sender, e);
        SizeChange(sender, e);
    }
    
    
    
    
    //common
    public void SizeChange(object? sender, EventArgs e)
    {
        foreach (CharForm cf in wppc.Children.OfType<CharForm>())
        {
            if (svpc.Bounds.Width > 1500)
            {
                cf.Width = svpc.Bounds.Width / 3 - 10;
                cf.Height = cf.Width / 1.33333;
            }
            else if (svpc.Bounds.Width > 1000)
            {
                cf.Width = svpc.Bounds.Width / 2 - 10;
                cf.Height = cf.Width / 1.33333;
            }
            else
            {
                cf.Width = svpc.Bounds.Width - 10;
                cf.Height = cf.Width / 1.33333;
            }
        }
    }
}