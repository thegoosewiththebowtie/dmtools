using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using dmtools.PopUps;
using LiteDB;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.LogicalTree;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using NAudio.Wave;
using Config.Net;
using dmtools.Generators;
using dmtools.Templates;
using Pango;

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
    public bool IsCStrength { get; set; }
    public bool IsCDexterity { get; set; }
    public bool IsCConstitution { get; set; }
    public bool IsCIntelligence { get; set; }
    public bool IsCWisdom { get; set; }
    public bool IsCCharisma { get; set; }
    public string Level { get; set; }
    public string Experience { get; set; }
    public string Health { get; set; }
    public string ArmorClass { get; set; }
}
public class NPC
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
    public bool IsCStrength { get; set; }
    public bool IsCDexterity { get; set; }
    public bool IsCConstitution { get; set; }
    public bool IsCIntelligence { get; set; }
    public bool IsCWisdom { get; set; }
    public bool IsCCharisma { get; set; }
    public string Level { get; set; }
    public string Experience { get; set; }
    public string Health { get; set; }
    public string ArmorClass { get; set; }
    public string Likes { get; set; }
    public string Dislikes { get; set; }
}
public class ToDoL
{
    public int ID { get; set; }
    public bool state { get; set; }
    public string txtt { get; set; }
}
public interface ISettings
{
    string MainNotes { get; set; }
    string MusPath { get; set; }
    string AmbPath { get; set; }
    string SndPath { get; set; }
    string ImgPath { get; set; }
    string DataPath { get; set; }
    double Volume { get; set; }
    double VolumeAmb { get; set; }
    double VolumeSnd { get; set; }
    string Profile { get; set; }
    string Player { get; set; }
    string Weather { get; set; }
    int MinutesInHour { get; set; }
    int HoursInDay { get; set; }
    int DaysInWeek { get; set; }
    int DaysInYear { get; set; }
    int MonthsInYear { get; set; }
    long DateAndTime { get; set; }
    string BrLink0 { get; set; }
    string BrLink1 { get; set; }
    string BrLink2 { get; set; }
    string BrLink3 { get; set; }
    int PrevDay { get; set; }
    string Language { get; set; }
    int ID { get; set; }
}

public class SelNpcs
{
    public int id { get; set; }
    public int init { get; set; }
}
public class Months0
{
    public int ID { get; set; }
    public int days { get; set; }
    public string monthname { get; set; }
}

public class Weeks0
{
    public int ID { get; set; }
    public string weekname { get; set; }
}

public class PictureWindow
{
    public static Picture picwin { get; set; } = new Picture();
    public static bool win = false;
}
public partial class HomeView : UserControl
{
    public static event EventHandler YTC ;
    Profile profile = new ConfigurationBuilder<Profile>().UseIniFile("Profile.ini").Build();
    public ISettings settings { get; set; }
    public int profid { get; set; }
    public HomeView()
    {
        InitializeComponent();
        TheTabControl.SelectionChanged += TheTabControl_OnSelectionChanged;
        profid = profile.ProfileID;
        SetSetup();
        AmbIni();
        SndIni();
        VolumeS.Value = settings.Volume;
        VolumeAmb.Value = settings.VolumeAmb;
        VolumeSnd.Value = settings.VolumeSnd;
        MainWindow.SizzeChanged += SizeChange;
        Picture.Closeed += ClozedEv;
        Sure.Delete += pcupdate;
        Sure.Delete += npcupdate;
        Sure.Delete += SizeChange;
        NoteInit();
        DashFightUp();
        picinit();
        ToDoInit();
        LanSet();
        GreetIni();
    }
    public void SetSetup()
    {
        settings = new ConfigurationBuilder<ISettings>().UseIniFile("Settings/q0" + profid +".ini").Build();
        settings.DataPath = "Data/q0" + profid + ".db";
        if (!Uri.TryCreate(settings.MusPath, UriKind.RelativeOrAbsolute, out Uri m)) { settings.MusPath = "Music/Mus"; }
        if (!Uri.TryCreate(settings.AmbPath, UriKind.RelativeOrAbsolute, out Uri a)) { settings.AmbPath = "Music/Amb"; }
        if (!Uri.TryCreate(settings.SndPath, UriKind.RelativeOrAbsolute, out Uri s)) { settings.SndPath = "Music/Snd"; }
        if (!Uri.TryCreate(settings.ImgPath, UriKind.RelativeOrAbsolute, out Uri i)) { settings.ImgPath = "Images"; }
        if (settings.MinutesInHour == 0) { settings.MinutesInHour = 60;}
        if (settings.HoursInDay == 0) { settings.HoursInDay = 24;}
        if (settings.DaysInWeek == 0) { settings.DaysInWeek = 7;}
        if (settings.DaysInYear == 0) { settings.DaysInYear = 360;}
        if (settings.MonthsInYear == 0) { settings.MonthsInYear = 12;}
        if (settings.Volume == 0) { settings.Volume = 10; } 
        if (settings.VolumeAmb == 0) { settings.VolumeAmb = 10; }
        if (settings.VolumeSnd == 0) { settings.VolumeSnd = 10; }
        if (settings.Language == null) { settings.Language = "en"; }
        using (var ldb = new LiteDatabase(settings.DataPath))
        {
            var Months = ldb.GetCollection<Months0>();
            var Weeks = ldb.GetCollection<Weeks0>();
            if (Months.Count() != settings.MonthsInYear)
            {
                Months.DeleteAll();
                for (int j = 0; j < settings.MonthsInYear; j++)
                {
                    Months.Insert(new Months0()
                    {
                        ID = j+1,
                        days = 30,
                        monthname = "Month " + (j+1).ToString()
                    });
                }
            }
            if (Weeks.Count() != settings.DaysInWeek)
            {
                Weeks.DeleteAll();
                for (int j = 0; j < settings.DaysInWeek; j++)
                {
                    Weeks.Insert(new Weeks0()
                    {
                        ID = j+1,
                        weekname = "Weekday " + (j+1).ToString()
                    });
                }
            }
        }
    }

    public void LanSet()
    {
        var translations = App.Current.Resources.MergedDictionaries.OfType<ResourceInclude>().FirstOrDefault(x => x.Source?.OriginalString?.Contains("/Locals/") ?? false);

        if (translations != null)
            App.Current.Resources.MergedDictionaries.Remove(translations);
        
        App.Current.Resources.MergedDictionaries.Add(
            new ResourceInclude(new Uri($"avares://dmtools/Locals/{settings.Language}.axaml"))
            {
                Source = new Uri($"avares://dmtools/Locals/{settings.Language}.axaml")
            });
    }

    //DASHBOARD
    
    //greeting
    public void GreetIni()
    {
        long AllMinutes = settings.DateAndTime;
        long AllHours = (long)Math.Floor((decimal)(AllMinutes / settings.MinutesInHour));
        int AllDays = (int)Math.Floor((decimal)(AllHours / settings.HoursInDay));
        int AllWeeks = AllDays % settings.DaysInWeek;
        int AllYears = (int)Math.Floor((decimal)(AllDays / settings.DaysInYear));
        int CurrentYear = AllYears;
        int CurrentYearDay = (AllDays - AllYears * settings.DaysInYear)+1;
        int CurrentHour = (int)(AllHours - AllDays * settings.HoursInDay);
        int CurrentMinute = (int)(AllMinutes - AllHours * settings.MinutesInHour);
        int halfday = settings.HoursInDay / 2;
        int ProperHour = CurrentHour;
        string TimeOfDay = "";
        switch (CurrentHour)
        {
            case 0:
                ProperHour = halfday;
                TimeOfDay = "am";
                break;
            case var hd when hd < halfday:
                TimeOfDay = "am";
                break;
            case var hd when hd == halfday:
                TimeOfDay = "pm";
                break;
            case var hd when hd > halfday:
                ProperHour -= halfday;
                TimeOfDay = "pm";
                break;
        }
        int CurrentMonth = 0;
        int CurrentMonthsDays = 0;
        int CurrentDay = CurrentYearDay;
        string MonthName = "";
        string WeekDayName = "";
        using (var ldb = new LiteDatabase(settings.DataPath))
        {
            var Months = ldb.GetCollection<Months0>();
            var Weeks = ldb.GetCollection<Weeks0>();
            foreach (var Month in Months.FindAll())
            {
                CurrentMonthsDays += Month.days;
                if (CurrentMonthsDays >= CurrentYearDay)
                {
                    break;
                }
                CurrentDay -= Month.days;
                CurrentMonth++;
            }
            MonthName = Months.FindById(CurrentMonth + 1).monthname;
            WeekDayName = Weeks.FindById(AllWeeks + 1).weekname;
        }
        if (CurrentDay != settings.PrevDay)
        {
            settings.Weather = Weather.ChangeWeather();
        }
        settings.PrevDay = CurrentDay;
        string ProperDay = "";
        if (CurrentDay.ToString().First() != '1' || CurrentDay.ToString().Length == 1)
        {
            switch (CurrentDay.ToString().Last())
            {
                case '1':
                    ProperDay = CurrentDay + Application.Current.FindResource("st").ToString();
                    break;
                case '2':
                    ProperDay = CurrentDay + Application.Current.FindResource("nd").ToString();
                    break;
                case '3':
                    ProperDay = CurrentDay + Application.Current.FindResource("rd").ToString();
                    break;
                default:
                    ProperDay = CurrentDay + Application.Current.FindResource("th").ToString();
                    break;
            }
        }
        else
        {
            ProperDay = CurrentDay + Application.Current.FindResource("th").ToString();
        }
        Greeting.Text = $"{Application.Current.FindResource("Hello")}, {settings.Player}, ";
        weather.Text = $"{Application.Current.FindResource("its")} {settings.Weather}{Application.Current.FindResource("itu")} ";
        Hours.Text = $"{ProperHour:00}";
        Minutes.Text = $"{CurrentMinute:00} ";
        timeod.Text = $"{TimeOfDay}{Application.Current.FindResource("the")}";
        day.Text = $"{ProperDay}";
        month.Text = $"{Application.Current.FindResource("of")}{MonthName}";
        monthn.Text = $"({(CurrentMonth + 1)}), ";
        year.Text = $"{CurrentYear:0000}";
        Week.Text = $", {WeekDayName}";
    }
    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
            switch ((sender as Button).Name)
            {
                case "minpl":
                    settings.DateAndTime++;
                    break;
                case "houpl":
                    settings.DateAndTime += settings.MinutesInHour;
                    break;
                case "daypl":
                    settings.DateAndTime += settings.HoursInDay * settings.MinutesInHour;
                    break;
                case "monpl":
                    settings.DateAndTime += settings.DaysInYear * settings.HoursInDay * settings.MinutesInHour;
                    break;
            }
            GreetIni();
        
    }
    private void Clockch_OnIsCheckedChanged(object? sender, RoutedEventArgs e)
    {
        bool state = (sender as ToggleButton).IsChecked.Value;
        foreach (var ch in GreetGrid.Children.OfType<Button>())
        {
            if (ch != sender)
            {
                ch.IsVisible = state;
            }
        }
    }
    private void Buttonmin_OnClick(object? sender, RoutedEventArgs e)
    {
            switch ((sender as Button).Name)
            {
                case "minmi":
                    if (settings.DateAndTime != 0)
                    {
                        settings.DateAndTime--;
                    }
                    break;
                case "houmi":
                    if (settings.DateAndTime > settings.MinutesInHour)
                    {
                        settings.DateAndTime -= settings.MinutesInHour;
                    }
                    break;
                case "daymi":
                    if (settings.DateAndTime > settings.HoursInDay * settings.MinutesInHour)
                    {
                        settings.DateAndTime -= settings.HoursInDay * settings.MinutesInHour;
                    }
                    break;
                case "monmi":
                    if (settings.DateAndTime > settings.DaysInYear * settings.HoursInDay * settings.MinutesInHour)
                    {
                        settings.DateAndTime -= settings.DaysInYear * settings.HoursInDay * settings.MinutesInHour;
                    }
                    break;
            }
            GreetIni();
    }
    

    
    
    //notes
    private void NoteInit()
    {
        var res = String.Empty;
        if (settings.MainNotes == null)
        {
            return;
        }
        foreach (var x in settings.MainNotes.Split(@"\r\n"))
        {
            res += $"{x}\r\n";
        }
        MainNotesT.Text = res;
    }
    private void SaveNotes_OnClick(object? sender, RoutedEventArgs e)
    {
        settings.MainNotes = MainNotesT.Text;
    }
    private void DeleteNotes_OnClick(object? sender, RoutedEventArgs e)
    {
        settings.MainNotes = "";
        MainNotesT.Text = "";
    }
    
    
    
    //fight
    public List<int> init = new List<int>();
    private List<SelNpcs> selnpc = new List<SelNpcs>();
    public List<FightDataNpc> npcsf = new List<FightDataNpc>(); 
    ObservableCollection<FightData> fin = new ObservableCollection<FightData>();
    public async void DashFightUp()
    {
        using (var LdbPC = new LiteDatabase(settings.DataPath))
        {
            fin.Clear();
            FightGrid.ItemsSource = null;
            var playChar = LdbPC.GetCollection<PlayerCharacter>();
            var npcChar = LdbPC.GetCollection<NPC>();
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
            List<int> listsn = new List<int>();
            foreach (var sn in selnpc)
            {
                listsn.Add(sn.id);
            }
            foreach (var nep in npcChar.FindAll())
            {
                if (listsn.Contains(nep.ID) && Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                {
                    var thisnpcindex = new int();
                    thisnpcindex = selnpc.FindIndex(r => r.id == nep.ID);
                    FightData fightList = new FightData()
                    {
                        Initiative = selnpc[thisnpcindex].init,
                        Name =  nep.FirstName,
                        Player = "NPC",
                        ArCl = Convert.ToInt32(nep.ArmorClass),
                        ID = nep.ID,
                        Health = Convert.ToInt32(nep.Health),
                    };
                    fin.Add(fightList);
                }
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
        if ((sender as ToggleButton).IsChecked == true && Avalonia.Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
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
            foreach (var m in MoodChooser.Items)
            {
                if (m.ToString() == "Battle" || m.ToString() == "Fight" || m.ToString() == "fight" || m.ToString() == "battle")
                {
                    MoodChooser.SelectedItem = m;
                }
            }
            int s = 1;
            using (var LdbPC = new LiteDatabase(settings.DataPath))
            {
                var playChar = LdbPC.GetCollection<PlayerCharacter>();
                int c = 0;
                foreach (var pc in playChar.FindAll())
                {
                    Init inn = new Init(pc.FirstName);
                    int dice = 0;
                    await inn.ShowDialog(desktop.MainWindow);
                    if (inn.result == -1)
                    { 
                        s = 0;
                        break;
                    }
                    if (inn.result < 21 && inn.result > 0)
                    { 
                        dice = inn.result;
                    }
                    else 
                    {
                        dice = rndm.Next(1, 21);
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
            foreach (var m in MoodChooser.Items)
            {
                if (m.ToString() == "Normal" || m.ToString() == "Neutral" || m.ToString() == "normal" || m.ToString() == "neutral")
                {
                    MoodChooser.SelectedItem = m;
                }
            }
            npcsf.Clear();
            AddCus.IsEnabled = false;
            AddNpc.IsEnabled = false;
            FightGrid.Columns[0].IsVisible = false;
            FightGrid.Columns[1].Sort();
            init.Clear();
            selnpc.Clear();
        }
        DashFightUp();
    }
    private async void AddCus_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Avalonia.Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            AddCustNpc acn = new AddCustNpc();
            await acn.ShowDialog(desktop.MainWindow);
            if (acn.res.initiative == -1)
            {
                return;
            }
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
        DashFightUp();
    }
    private async void AddNpc_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Avalonia.Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            AddNpc addNpc = new AddNpc();
            await addNpc.ShowDialog(desktop.MainWindow);
            if (addNpc.SelNpc == null)
            {
                return;
            }
            List<int> listsn = new List<int>();
            foreach (var sn in selnpc)
            {
                listsn.Add(sn.id);
            }
            if (listsn.Contains(addNpc.SelNpc.ID))
            {
                NO no = new NO("This characted is already in a grid or smth // i need to make a better error window");
                no.ShowDialog(desktop.MainWindow);
                return;
            }
            using (var LdbPC = new LiteDatabase(settings.DataPath))
            {
                var npcChar = LdbPC.GetCollection<NPC>();
                Init inn = new Init(npcChar.FindById(addNpc.SelNpc.ID).FirstName);
                await inn.ShowDialog(desktop.MainWindow);
                var dice = new int();
                if (inn.result == -1)
                { 
                    return;
                }
                if (inn.result < 21 && inn.result > 0)
                { 
                    dice = inn.result;
                }
                else 
                {
                    dice = rndm.Next(1, 21);
                }
                SelNpcs sdsdf = new SelNpcs()
                {
                    id = addNpc.SelNpc.ID,
                    init = (int)Math.Floor(Convert.ToDecimal((Convert.ToInt32(npcChar.FindById(addNpc.SelNpc.ID).Dexterity) - 10) / 2)) + dice
                };
                selnpc.Add(sdsdf);
            }
            DashFightUp();
        }
    }
    
    
    
    //todoo
    private void ToDoInit()
    {
        tdl.Items.Clear();
        using (var LdbPC = new LiteDatabase(settings.DataPath))
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
        using (var LdbPC = new LiteDatabase(settings.DataPath))
        {
            var todos = LdbPC.GetCollection<ToDoL>();
            var tt = todos.FindById(Convert.ToInt32((sender as CheckBox).Name));
            tt.state = (bool)(sender as CheckBox).IsChecked;
            todos.Update(tt);
        }
    }
    private void Deltd_OnClick(object? sender, RoutedEventArgs e)
    {
        if (tdl.SelectedItem == null)
        {
            return;
        }
        using (var LdbPC = new LiteDatabase(settings.DataPath))
        {
            CheckBox delth = (tdl.SelectedItem as CheckBox);
            var todos = LdbPC.GetCollection<ToDoL>(); 
            todos.Delete(Convert.ToInt32(delth.Name));
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
        using (var LdbPC = new LiteDatabase(settings.DataPath))
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
                Image image = new Image
                {
                    Source = bitmap,
                };
                ToggleButton button = new ToggleButton
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
        if (PictureWindow.win)
        {
            OpenWin.IsChecked = true;
        }
    }
    private void ImgChanger(object? sender, RoutedEventArgs e)
    {
        if (selimg == (sender as ToggleButton).Name)
        {
            (sender as ToggleButton).IsChecked = true;
            return;
        }
        if ((sender as ToggleButton).IsChecked != true)
        {
            return;
        }
        Bitmap mainimg = new Bitmap((sender as ToggleButton).Name);
        selimg = (sender as ToggleButton).Name;
        ImagePreview.Source = mainimg;
        PictureWindow.picwin.pichange(mainimg);
        foreach (var tgche in wpip.Children.OfType<ToggleButton>())
        { 
            if (tgche.Name != (sender as ToggleButton).Name)
            { 
                tgche.IsChecked = false;
            }
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
            i++;
        }
        if ((sender as Button).Name == "Right")
        {
            foreach (var but in wpip.Children.OfType<ToggleButton>())
            {
                if (i != pics.Count - 1 && but.Name == pics[i+1])
                {
                    but.IsChecked = true;
                    return;
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
                    return;
                }
            }
        }
    }
    private void OpenPictureWindow(object? sender, RoutedEventArgs e)
    {
        if (PictureWindow.win)
        {
            PictureWindow.picwin.Hide();
            PictureWindow.win = false;
            OpenWin.IsChecked = false;
        }
        else if (!PictureWindow.win)
        {
            PictureWindow.picwin.Show();
            PictureWindow.win = true;
            OpenWin.IsChecked = true;
        }
    }
    private void ClozedEv(object? o, EventArgs e)
    {
        PictureWindow.win = false;
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
    
    
    //dice
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
    
    
    
    
    //media
    
    private void Media_OnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
    }
    //music
    public WaveOutEvent mainPlayer = new WaveOutEvent();
    public Random rndm = new Random();
    public AudioFileReader auf;
    public DispatcherTimer mainTime { get; set; }
    public string NowPlaying { get; set; } = "0";
    public string CurrentSongPath { get; set; } = "0";
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
    public List<AudioFileReader> af = new List<AudioFileReader>();
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
                    Button tg0 = new Button()
                    {
                        Name = c.ToString(),
                        Content = snd.Name.Replace(".mp3", ""),
                        Width = 150,
                        Height = 50,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        FontSize = 24,
                        Margin = new Thickness(5)
                    };
                    var x = new AudioFileReader(snd.FullName);
                    x.Volume = (float)settings.VolumeSnd;
                    af.Add(x);
                    tg0.Click += boom;
                    Sounds.Children.Add(tg0);
                    c++;
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
        WaveOutEvent ap = new WaveOutEvent();
        ap.Init(af[Convert.ToInt32((sender as Button).Name)]);
        ap.Play();
    }
    private void VolumeSnd_OnValueChanged(object? sender, RangeBaseValueChangedEventArgs e)
    {
        foreach (var pl in af)
        {
            settings.VolumeSnd = (sender as Slider).Value;
            pl.Volume = (float)settings.VolumeSnd/10;
        }
    }
    
    
    
    //YT
    
    
    
    //npcs
    private void AddNpcP(object? sender, RoutedEventArgs e)
    {
        using (var LdbNPC = new LiteDatabase(settings.DataPath))
        {
            var npc = LdbNPC.GetCollection<NPC>();
            npc.Insert(new NPC
            {
                Level = "1", ArmorClass = "10", Health = "5", Strength = "10", Dexterity = "10",
                Constitution = "10", Experience = "0", Wisdom = "10", Intelligence = "10", Charisma = "10", IsCCharisma = false,
                IsCConstitution = false, IsCDexterity = false, IsCStrength = false, IsCIntelligence = false, IsCWisdom = false,
                Likes = string.Empty, Dislikes = string.Empty
            });
        }
        npcupdate(sender, e);
        SizeChange(sender, e);
    }
    public void npcupdate(object? sender, EventArgs e)
    {
        wpnpc.Children.Clear();
        using (var LdbPC = new LiteDatabase(settings.DataPath))
        {
            var npc = LdbPC.GetCollection<NPC>();
            foreach (var pc in npc.FindAll())
            {
                NpcForm charForm = new NpcForm();
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
                charForm.IsCheck = new List<bool>()
                {
                    pc.IsCCharisma, pc.IsCStrength, pc.IsCDexterity, pc.IsCConstitution, pc.IsCIntelligence, pc.IsCWisdom,
                };
                if (pc.Likes != null)
                {
                    charForm.Likes = pc.Likes.Split("/").ToList();
                }
                if (pc.Dislikes != null)
                {
                    charForm.Dis = pc.Dislikes.Split("/").ToList();
                }
                wpnpc.Children.Add(charForm);
            }
        }
    }
    private void NPCs_OnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        npcupdate(sender, e);
        SizeChange(sender, e);
    }
    
    
    
    
    //playercharacters
    public void Add(object sender, RoutedEventArgs args)
    {
        using (var LdbPC = new LiteDatabase(settings.DataPath))
        {
            var playChar = LdbPC.GetCollection<PlayerCharacter>();
            playChar.Insert(new PlayerCharacter
            {
                Level = "1", ArmorClass = "10", Health = "5", Strength = "10", Dexterity = "10",
                Constitution = "10", Experience = "0", Wisdom = "10", Intelligence = "10", Charisma = "10", IsCCharisma = false,
                IsCConstitution = false, IsCDexterity = false, IsCStrength = false, IsCIntelligence = false, IsCWisdom = false
            });
        }
        pcupdate(sender, args);
        SizeChange(sender, args);
    }
    public void pcupdate(object? sender, EventArgs e)
    {
        wppc.Children.Clear();
        using (var LdbPC = new LiteDatabase(settings.DataPath))
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
                charForm.IsCheck = new List<bool>()
                {
                    pc.IsCCharisma, pc.IsCStrength, pc.IsCDexterity, pc.IsCConstitution, pc.IsCIntelligence, pc.IsCWisdom,
                };
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
        double Size = GreetGrid.Bounds.Width * 0.02;
        Greeting.FontSize = Size;
        weather.FontSize = Size;
        Hours.FontSize = Size;
        ee.FontSize = Size;
        Minutes.FontSize = Size;
        timeod.FontSize = Size;
        day.FontSize = Size;
        month.FontSize = Size;
        monthn.FontSize = Size;
        year.FontSize = Size;
        Week.FontSize = Size;
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
        foreach (NpcForm cf in wpnpc.Children.OfType<NpcForm>())
        {
            if (svnpc.Bounds.Width > 1500)
            {
                cf.Width = svnpc.Bounds.Width / 3 - 10;
                cf.Height = cf.Width / 1.33333;
            }
            else if (svnpc.Bounds.Width > 1000)
            {
                cf.Width = svnpc.Bounds.Width / 2 - 10;
                cf.Height = cf.Width / 1.33333;
            }
            else
            {
                cf.Width = svnpc.Bounds.Width - 10;
                cf.Height = cf.Width / 1.33333;
            }
        }
    }
    private void TheTabControl_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        YTC(sender, e);
    }
}