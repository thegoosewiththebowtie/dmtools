using System;
using System.Diagnostics;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;
using Config.Net;

namespace dmtools.Views;

public partial class SettingsView : UserControl
{
    public SettingsView()
    {
        InitializeComponent();
        ButIni();
    }
    ISettings settings = new ConfigurationBuilder<ISettings>().UseIniFile("Settings.ini").Build();

    public void ButIni()
    {
        MusPa.Text = settings.MusPath;
        AmbPa.Text = settings.AmbPath;
        SndPa.Text = settings.SndPath;
        if (settings.MusPath == "Music/Mus")
        {
            Music.IsChecked = true;
        }
        else
        {
            Music.IsChecked = false;
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
            Sound.IsChecked = true;
        }
        else
        {
            Sound.IsChecked = false;
        }
    }
}