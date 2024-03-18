using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Platform.Storage;
using Avalonia.Styling;
using Config.Net;
using dmtools.PopUps;
using LiteDB;
using dmtools.Templates;
namespace dmtools.Views;

public partial class SettingsView : UserControl
{
    Profile profile = new ConfigurationBuilder<Profile>().UseIniFile("Profile.ini").Build();
    public ISettings settings { get; set; }
    public int profid { get; set; }
    public SettingsView()
    {
        InitializeComponent();
        profid = profile.ProfileID;
        ButIni();
    }
    public void ButIni()
    {
        settings = new ConfigurationBuilder<ISettings>().UseIniFile("Settings/q0" + profid +".ini").Build();
        MusPa.Text = settings.MusPath;
        AmbPa.Text = settings.AmbPath;
        SndPa.Text = settings.SndPath;
        if (settings.MusPath == "Music/Mus")
        {
            Mus.IsChecked = true;
        }
        else
        {
            Mus.IsChecked = false;
        }

        if (settings.AmbPath == "Music/Amb")
        {
            Amb.IsChecked = true;
        }
        else
        {
            Amb.IsChecked = false;
        }

        if (settings.SndPath == "Music/Snd")
        {
            Snd.IsChecked = true;
        }
        else
        {
            Snd.IsChecked = false;
        }
        if (settings.ImgPath == "Images")
        {
            Img.IsChecked = true;
        }
        else
        {
            Img.IsChecked = false;
        }
        MiHs.Value = settings.MonthsInYear;
        HiDs.Value = settings.HoursInDay;
        DiWs.Value = settings.DaysInWeek;
        MiYs.Value = settings.MonthsInYear;
        if (File.Exists($"Data/q0{settings.ID}.bmp"))
        {
            PfPSett.Source = new Avalonia.Media.Imaging.Bitmap($"Data/q0{settings.ID}.bmp");
        }
        ProfName.Text = settings.Profile;
        DmName.Text = settings.Player;
        foreach (var lng in LanguageChanger.Items)
        {
            if ((lng as ComboBoxItem).Content.ToString().Contains(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(settings.Language.ToLower())))
            {
                LanguageChanger.SelectedItem = lng;
            }
        }
    }
    private void Default(object? sender, RoutedEventArgs e)
    {
        if ((sender as ToggleButton).IsChecked == true)
        {
            switch ((sender as ToggleButton).Name)
            {
                case "Mus": settings.MusPath = "Music/Mus"; break; 
                case "Amb": settings.AmbPath = "Music/Amb"; break;
                case "Snd": settings.SndPath = "Music/Snd"; break;
                case "Img": settings.ImgPath = "Images"; break;
            }
            MusPa.Text = settings.MusPath;
            AmbPa.Text = settings.AmbPath;
            SndPa.Text = settings.SndPath;
            ImgPa.Text = settings.ImgPath;
        }
    }
    private async void Browse(object? sender, RoutedEventArgs e)
    {
        var topLevel = TopLevel.GetTopLevel(this);
        var result = await topLevel.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions(){ Title = "Choose directory"});
        if (result.Count != 0)
        {
            if (sender is ToggleButton)
            {
                switch ((sender as ToggleButton).Name)
                {
                    case "Mus":
                        settings.MusPath = result[0].Path.ToString().Replace("file:///", "");
                        break;
                    case "Amb":
                        settings.AmbPath = result[0].Path.ToString().Replace("file:///", "");
                        break;
                    case "Snd":
                        settings.SndPath = result[0].Path.ToString().Replace("file:///", "");
                        break;
                    case "Img":
                        settings.ImgPath = result[0].Path.ToString().Replace("file:///", "");
                        break;
                }
            }
            else
            {
                switch ((sender as Button).Name)
                {
                    case "MusBrs":
                        settings.MusPath = result[0].Path.ToString().Replace("file:///", "");
                        break;
                    case "AmbBrs":
                        settings.AmbPath = result[0].Path.ToString().Replace("file:///", "");
                        break;
                    case "SndBrs":
                        settings.SndPath = result[0].Path.ToString().Replace("file:///", "");
                        break;
                    case "ImgBrs":
                        settings.ImgPath = result[0].Path.ToString().Replace("file:///", "");
                        break;
                }
            }
        }
        ButIni();
    }
    private void SaveAms_OnClick(object? sender, RoutedEventArgs e)
    {
        settings.MinutesInHour = (int)MiHs.Value;
        settings.HoursInDay = (int)HiDs.Value;
        settings.DaysInWeek = (int)DiWs.Value;
        settings.MonthsInYear = (int)MiYs.Value;
    }

    private void Months_OnExpanded(object? sender, RoutedEventArgs e)
    {
        using (var Ldb = new LiteDatabase(settings.DataPath))
        {
            var m = Ldb.GetCollection<Months0>();
            Months.Children.Clear();
            for (int i = 0; i < settings.MonthsInYear; i++)
            {
                Months.Children.Add(new WeekMonthEdit()
                {
                    Name = (i + 1).ToString(),
                    MonthName = m.FindById(i+1).monthname,
                    Days = m.FindById(i+1).days,
                    MainWatermark = "Month " + (i + 1),
                });
            }
        }

        Button savemonth = new Button()
        {
            Content = "Save",
            Margin = new Thickness(5)
        };
        savemonth.Click += SavemonthOnClick;
        Months.Children.Add(savemonth);
    }

    private void SavemonthOnClick(object? sender, RoutedEventArgs e)
    {
        using (var LdbPC = new LiteDatabase(settings.DataPath))
        {
            var months = LdbPC.GetCollection<Months0>();
            months.DeleteAll();
            int diy = 0;
            foreach (var month in Months.Children.OfType<WeekMonthEdit>())
            {
                months.Insert(new Months0()
                {
                    ID = Convert.ToInt32(month.Name),
                    days = (int)month.Days,
                    monthname = month.MonthName,
                });
                diy += (int)month.Days;
            }
            settings.DaysInYear = diy;
        }
    }
    

    private void Weeks_OnExpanded(object? sender, RoutedEventArgs e)
    {
        using (var Ldb = new LiteDatabase(settings.DataPath))
        {
            var w = Ldb.GetCollection<Weeks0>();
            Weeks.Children.Clear();
            for (int i = 0; i < settings.DaysInWeek; i++)
            {
                Weeks.Children.Add(new TextBox()
                {
                    Name = (i + 1).ToString(),
                    Watermark = "Day " + (i + 1),
                    Text = w.FindById(i+1).weekname,
                    Margin = new Thickness(5)
                });
            }
        }

        Button saveweek = new Button()
        {
            Content = "Save",
            Margin = new Thickness(5)
        };
        saveweek.Click += SaveweekOnClick;
        Weeks.Children.Add(saveweek);
    }

    private void SaveweekOnClick(object? sender, RoutedEventArgs e)
    {
        using (var LdbPC = new LiteDatabase(settings.DataPath))
        {
            var weeks = LdbPC.GetCollection<Weeks0>();
            weeks.DeleteAll();
            foreach (var week in Weeks.Children.OfType<TextBox>())
            {
                weeks.Insert(new Weeks0()
                {
                    ID = Convert.ToInt32(week.Name),
                    weekname = week.Text,
                });

            }
        }
    }

    private void DelPro_OnClick(object? sender, RoutedEventArgs e)
    {
        var topLevel = TopLevel.GetTopLevel(this);
        List<int> profs = new List<int>();
        DirectoryInfo profilesset = new DirectoryInfo("Settings/");
        foreach (var profilen in profilesset.GetFiles())
        {
            profs.Add(Convert.ToInt32(profilen.Name.Replace("q0", "").Replace(".ini", "")));
        }
        profs.Remove(profid);
        File.Delete($"Settings/q0{profid}.ini");
        File.Delete($"Data/q0{profid}.db");
        File.Delete($"Data/q0{profid}.bmp");
        if (profs.Count != 0)
        {
            profile.ProfileID = profs[0];
        }
        else
        {
            File.Delete("Profile.ini");
        }
        Process p = Process.GetCurrentProcess();    
        Process.Start(p.ProcessName + ".exe");
        if (Avalonia.Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            Thread.Sleep(1000);
            desktop.Shutdown();
        }
    }

    private async void Pfpb_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        var topLevel = TopLevel.GetTopLevel(this);
        var result = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions(){ Title = "Choose profile picture", 
            FileTypeFilter = new[] { FilePickerFileTypes.ImageAll }});
        if (result.Count == 0)
        {
            return;
        }
        Bitmap pfp = new Bitmap(result[0].Path.ToString().Replace("file:///", ""));
        pfp.Save($"Data/q0{settings.ID}.bmp");
        PfPSett.Source = new Avalonia.Media.Imaging.Bitmap($"Data/q0{settings.ID}.bmp");
        if (Avalonia.Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            (desktop.MainWindow as MainWindow).pfp.Source = new Avalonia.Media.Imaging.Bitmap($"Data/q0{settings.ID}.bmp");
        }
    }

    private void LanguageChanger_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        settings.Language = ((sender as ComboBox).SelectedItem as ComboBoxItem).Content.ToString().ToLower().Remove(2);
    }

    private async void ProfName_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        settings.Profile = (sender as TextBox).Text;
    }

    private void DmName_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        settings.Player = (sender as TextBox).Text;
    }
}