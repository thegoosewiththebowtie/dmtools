﻿using System;
using System.Diagnostics;
using System.IO;
using System.Security.Permissions;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
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
            }
            MusPa.Text = settings.MusPath;
            AmbPa.Text = settings.AmbPath;
            SndPa.Text = settings.SndPath;
        }
        else
        {

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
                }
            }
        }
        ButIni();
    }
}