using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using dmtools.Resources;
using LiteDB;
using System.IO;
using System.Runtime;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.LogicalTree;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using NAudio;
using Avalonia.Platform.Storage;
using Avalonia.Platform.Storage.FileIO;
using Avalonia.Threading;
using NAudio.Wave;
using Config.Net;
using DynamicData;


namespace dmtools.Views;

public class FightData
{
    public int Initiative { get; set; }
    public string Name { get; set; }
    public string Player { get; set; }
    public int ArCl { get; set; }
    public int ID { get; set; }
    public int Health { get; set; }
}

public class FightDataNpc
{
    public int Initiative { get; set; }
    public string Name { get; set; }
    public string Player { get; set; }
    public int ArCl { get; set; }
    public int ID { get; set; }
    public int Health { get; set; }
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

public class Notes
{
    public int ID { get; set; }
    public string MainNotes { get; set; }
}

public class ToDoL
{
    public int ID { get; set; }
    public bool state { get; set; }
    public string txtt { get; set; }
}
public interface ISettings
{
    string MusPath { get; set; }
    string AmbPath { get; set; }
    string SndPath { get; set; }
    string ImgPath { get; set; }
    double Volume { get; set; }
    double VolumeAmb { get; set; }
    string Profile { get; set; }
}
public partial class HomeView : UserControl
{
    public HomeView()
    {
        InitializeComponent();
        SetSetup();
        AmbIni();
        SndIni();
        VolumeS.Value = settings.Volume;
        MainWindow.SizzeChanged += SizeChange;
        Picture.Closeed += ClozedEv;
        Sure.Delete += pcupdate;
        Sure.Delete += SizeChange;
        NoteInit();
        DashFightUp();
        picinit();
        ToDoInit();
    }
    ISettings settings = new ConfigurationBuilder<ISettings>().UseIniFile("Settings.ini").Build();
    public void SetSetup()
    {
        if (settings.Profile != "Profiles/q01" || settings.Profile != "Profiles/q02" || settings.Profile != "Profiles/q03")
        {
            settings.Profile = "Profiles/q01";
        }
        if (!Uri.TryCreate(settings.MusPath, UriKind.RelativeOrAbsolute, out Uri m))
        {
            settings.MusPath = "Music/Mus";
        }

        if (!Uri.TryCreate(settings.AmbPath, UriKind.RelativeOrAbsolute, out Uri a))
        {
            settings.AmbPath = "Music/Amb";
        }

        if (!Uri.TryCreate(settings.SndPath, UriKind.RelativeOrAbsolute, out Uri s))
        {
            settings.SndPath = "Music/Snd";
        }
        
        if (!Uri.TryCreate(settings.ImgPath, UriKind.RelativeOrAbsolute, out Uri i))
        {
            settings.ImgPath = "Images";
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
    
    
    //notes
    private void NoteInit()
    {
        using (var LdbPC = new LiteDatabase(settings.Profile))
        {
            var nots = LdbPC.GetCollection<Notes>();
            if (nots.FindAll().Count() != 0)
            {
                MainNotesT.Text = nots.FindById(1).MainNotes;
            }
        }
    }
    private void SaveNotes_OnClick(object? sender, RoutedEventArgs e)
    {
        using (var LdbPC = new LiteDatabase(settings.Profile))
        {
            var nots = LdbPC.GetCollection<Notes>();
            nots.DeleteAll();
            nots.Insert(new Notes()
            {
                ID = 0,
                MainNotes = MainNotesT.Text
            });
        }
    }
    private void DeleteNotes_OnClick(object? sender, RoutedEventArgs e)
    {
        using (var LdbPC = new LiteDatabase(settings.Profile))
        {
            var nots = LdbPC.GetCollection<Notes>();
            nots.DeleteAll();
            MainNotesT.Text = "";
        }
    }
    
    
    
    //fight
    public List<int> init = new List<int>();
    public List<FightDataNpc> npcsf = new List<FightDataNpc>(); 
    ObservableCollection<FightData> fin = new ObservableCollection<FightData>();
    public void DashFightUp()
    {
        using (var LdbPC = new LiteDatabase(settings.Profile))
        {
            fin.Clear();
            FightGrid.ItemsSource = null;
            var playChar = LdbPC.GetCollection<PlayerCharacter>();
            int c = 0;
            foreach (var pc in playChar.FindAll())
            {
                int i;
                if (fighttb.IsChecked == true)
                {
                    i = init[c];
                }
                else
                {
                    init.Add(0);
                    i = 0;
                }
                FightData fightList = new FightData()
                {
                    Initiative = i,
                    Name =  pc.FirstName,
                    Player = pc.Player,
                    ArCl = Convert.ToInt32(pc.ArmorClass),
                    ID = pc.ID,
                    Health = Convert.ToInt32(pc.Health),
                };
                c++;
                fin.Add(fightList);
            }
            foreach (var variNpc in npcsf)
            {
                if (fighttb.IsChecked == true)
                {
                    FightData fightList = new FightData()
                    {
                        Initiative = variNpc.Initiative,
                        Name = variNpc.Name,
                        Player = variNpc.Player,
                        ArCl = variNpc.ArCl,
                        ID = variNpc.ID,
                        Health = variNpc.Health,
                    };
                    fin.Add(fightList);
                }
            }
            FightGrid.ItemsSource = fin;
        }
    }
    private void InputElement_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        DashFightUp();
    }
    private async void Startf(object? sender, RoutedEventArgs e)
    {
        if ((sender as ToggleButton).IsChecked == true)
        {
            int s = 1;
            using (var LdbPC = new LiteDatabase(settings.Profile))
            {
                var playChar = LdbPC.GetCollection<PlayerCharacter>();
                int c = 0;
                foreach (var pc in playChar.FindAll())
                {
                    Init inn = new Init(pc.FirstName);
                    int dice = 0;
                    if (Avalonia.Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                    {
                        await inn.ShowDialog(desktop.MainWindow);
                        if (inn.result == -1)
                        {
                            s = 0;
                            break;
                        }
                        else if (inn.result < 21 && inn.result > 0)
                        {
                            dice = inn.result;
                        }
                        else
                        {
                            dice = rndm.Next(1, 20);
                        }
                    }
                    init[c] = init[c] +
                              (int)Math.Floor(Convert.ToDecimal((Convert.ToInt32(pc.Dexterity) - 10) / 2)) + dice;
                    c++;
                }
            }
            if (s == 0)
            {
                (sender as ToggleButton).IsChecked = false;
            }
            else
            {
                AddCus.IsEnabled = true;
                AddNpc.IsEnabled = true;
                FightGrid.Columns[0].IsVisible = true;
                FightGrid.Columns[0].Sort(ListSortDirection.Descending);
            }
        }
        else
        {
            npcsf.Clear();
            AddCus.IsEnabled = false;
            AddNpc.IsEnabled = false;
            FightGrid.Columns[0].IsVisible = false;
            FightGrid.Columns[1].Sort();
            init.Clear();
        }
        DashFightUp();
    }
    private async void AddCus_OnClick(object? sender, RoutedEventArgs e)
    {
        AddCustNpc acn = new AddCustNpc();
        if (Avalonia.Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            await acn.ShowDialog(desktop.MainWindow);
            if (acn.res.initiative != -1)
            {
                npcsf.Add(new FightDataNpc()
                {
                    Name = acn.res.name,
                    Player = acn.res.playe,
                    Initiative = acn.res.initiative,
                    ArCl = acn.res.ac,
                    ID = 999,
                    Health = acn.res.hp
                });
            }
        }
        DashFightUp();
    }
    private void AddNpc_OnClick(object? sender, RoutedEventArgs e)
    {
        
    }
    
    
    
    //todoo
    private void ToDoInit()
    {
        tdl.Items.Clear();
        using (var LdbPC = new LiteDatabase(settings.Profile))
        {
            var todos = LdbPC.GetCollection<ToDoL>();
            foreach (var todo in todos.FindAll())
            {
                CheckBox cb = new CheckBox();
                cb.Name = todo.ID.ToString();
                cb.IsChecked = todo.state;
                cb.Content = todo.txtt;
                cb.IsCheckedChanged += CbOnIsCheckedChanged;
                tdl.Items.Add(cb);
            }
        }
    }
    public void CbOnIsCheckedChanged(object? sender, RoutedEventArgs e)
    {
        using (var LdbPC = new LiteDatabase(settings.Profile))
        {
            var todos = LdbPC.GetCollection<ToDoL>();
            var tt = todos.FindById(Convert.ToInt32((sender as CheckBox).Name));
            tt.state = (bool)(sender as CheckBox).IsChecked;
            todos.Update(tt);
        }
    }
    private void Deltd_OnClick(object? sender, RoutedEventArgs e)
    {
        if (tdl.SelectedItem != null)
        {
            CheckBox delth = (tdl.SelectedItem as CheckBox);
            using (var LdbPC = new LiteDatabase(settings.Profile))
            {
                var todos = LdbPC.GetCollection<ToDoL>();
                todos.Delete(Convert.ToInt32(delth.Name));
            }
        }
        ToDoInit();
    }

    private void Addtd_OnClick(object? sender, RoutedEventArgs e)
    {
        maintodoll.IsVisible = false;
        addtodol.IsVisible = true;

    }
    private void SaveNTD_OnClick(object? sender, RoutedEventArgs e)
    {
        maintodoll.IsVisible = true;
        addtodol.IsVisible = false;
        using (var LdbPC = new LiteDatabase(settings.Profile))
        {
            var todos = LdbPC.GetCollection<ToDoL>();
            todos.Insert(new ToDoL()
            {
                state = false,
                txtt = textadd.Text,
            });
        }
        ToDoInit();
        
    }

    private void CanNTD_OnClick(object? sender, RoutedEventArgs e)
    {
        maintodoll.IsVisible = true;
        addtodol.IsVisible = false;
    }
    
    
    
    //picture
    public string openclose { get; set; }
    public Picture picwin = new Picture();
    public bool win = false;
    public List<string> pics = new List<string>();
    public string selimg { get; set; }
    public void picinit()
    {
        DirectoryInfo dirim = new DirectoryInfo(settings.ImgPath);
        foreach (var imgs in dirim.GetFiles())
        {
            if (imgs.Name.EndsWith(".png") || imgs.Name.EndsWith(".jpg") || imgs.Name.EndsWith(".jpeg") || imgs.Name.EndsWith(".bmp"))
            {
                Bitmap bitmap = new Bitmap(imgs.FullName);
                Image image = new Image()
                {
                    Source = bitmap,
                };
                ToggleButton button = new ToggleButton()
                {
                    Height = 100,
                    Width = 100,
                    Name = imgs.FullName,
                    Content = image,
                };
                pics.Add(imgs.FullName);
                button.IsCheckedChanged += ImgChanger;
                wpip.Children.Add(button);
            }
        }
    }
    private void ImgChanger(object? sender, RoutedEventArgs e)
    {
        if ((sender as ToggleButton).IsChecked == true)
        {
            Bitmap mainimg = new Bitmap((sender as ToggleButton).Name);
            selimg = (sender as ToggleButton).Name;
            ImagePreview.Source = mainimg;
            picwin.pichange(mainimg);
            foreach (var tgche in wpip.Children.OfType<ToggleButton>())
            {
                if (tgche.Name != (sender as ToggleButton).Name)
                {
                    tgche.IsChecked = false;
                }
            }
        }
        else if ((sender as ToggleButton).IsChecked != true && selimg == (sender as ToggleButton).Name)
        {
            (sender as ToggleButton).IsChecked = true;
        }
    }
    private void LeftRight_OnClick(object? sender, RoutedEventArgs e)
    {
        int i = 0;
        foreach (var path in pics)
        {
            if (path == selimg)
            {
                break;
            }
            else
            {
                i++;
            }
        }
        if ((sender as Button).Name == "Right")
        {
            foreach (var but in wpip.Children.OfType<ToggleButton>())
            {
                if (i != pics.Count - 1 && but.Name == pics[i+1])
                {
                    but.IsChecked = true;
                }
            }
        }
        else if ((sender as Button).Name == "Left")
        {
            foreach (var but in wpip.Children.OfType<ToggleButton>())
            {
                if (i != 0 && but.Name == pics[i-1])
                {
                    but.IsChecked = true;
                    break;
                }
            }
        }
    }
    private void OpenPictureWindow(object? sender, RoutedEventArgs e)
    {
        if (win)
        {
            picwin.Hide();
            win = false;
        }
        else if (!win)
        {
            picwin.Show();
            win = true;
        }
    }
    private void ClozedEv(object? o, EventArgs e)
    {
        win = false;
    }
    private void GridVVI_OnIsCheckedChanged(object? sender, RoutedEventArgs e)
    {
        if ((sender as ToggleButton).IsChecked == true)
        {
            wpip.IsVisible = true;
            ImagePreview.IsVisible = false;
        }
        else
        {
            wpip.IsVisible = false;
            ImagePreview.IsVisible = true;
        }
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
        using (var LdbPC = new LiteDatabase(settings.Profile))
        {
            var playChar = LdbPC.GetCollection<PlayerCharacter>();
            playChar.Insert(new PlayerCharacter
            {
                Level = "1", ArmorClass = "10", Health = "5", Strength = "10", Dexterity = "10",
                Constitution = "10", Experience = "0", Wisdom = "10", Intelligence = "10", Charisma = "10"
            });
        }
        pcupdate(sender, args);
        SizeChange(sender, args);
    }
    public void pcupdate(object? sender, EventArgs e)
    {
        wppc.Children.Clear();
        using (var LdbPC = new LiteDatabase(settings.Profile))
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

    public List<int> his = new List<int>();
    private void DiceThrow(object? sender, RoutedEventArgs e)
    {
        foreach (var g in (sender as Button).GetLogicalChildren().OfType<Grid>())
        {
            foreach (var res in g.Children.OfType<TextBlock>())
            {
                int ress = 0;
                for (int i = 0; i < Convert.ToInt32(seldm.Remove(0,1)); i++)
                {
                    var rsr = rndm.Next(1, Convert.ToInt32((sender as Button).Name.Remove(0, 1))+1);
                    ress += rsr;
                    HistoryD.Items.Insert(0, rsr);
                }
                res.Text = ress.ToString();
                TotalD.Text = (Convert.ToInt32(TotalD.Text) + ress).ToString();
            }
        }
    }

    public string seldm { get; set; } = "x1";
    private void moredice(object? sender, RoutedEventArgs e)
    {
        if (Dicess == null)
        {
            return;
        }
        if ((sender as ToggleButton).IsChecked == false)
        {
            return;
        }
        seldm = (sender as ToggleButton).Name;
        foreach (var tb in Dicess.Children.OfType<ToggleButton>())
        {
            if (tb.Name != seldm)
            {
                tb.IsChecked = false;
            }
        }
    }

    private void Cleardice_OnClick(object? sender, RoutedEventArgs e)
    {
        foreach (var d in Dicess.Children.OfType<Button>())
        {
            var r = d.Name.Remove(0, 1);
            foreach (var g in d.GetLogicalChildren().OfType<Grid>())
            {
                foreach (var tb in g.Children.OfType<TextBlock>())
                {
                    tb.Text = r;
                }
            }
        }
        HistoryD.Items.Clear();
        TotalD.Text = "0";
    }
}