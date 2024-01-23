using System;
using System.Diagnostics;
using System.IO;
using Avalonia.Controls.ApplicationLifetimes;
using Config.Net;
using dmtools.Resources;
using dmtools.Views;
using ReactiveUI;

namespace dmtools.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    ViewModelBase _content;

    public MainWindowViewModel()
    {
        Profile profile = new ConfigurationBuilder<Profile>().UseIniFile("Profile.ini").Build();
        DirectoryInfo prof = new DirectoryInfo("Settings"); 
        if (prof.GetFiles().Length == 0)
        {
           Content = new AddProfileViewModel();
        }
        else
        {
            Content = new HomeViewModel();
        }
    }
    

    public ViewModelBase Content
    {
        get => _content;
        private set => this.RaiseAndSetIfChanged(ref _content, value);
    }

    //HomeViewModel List { get; }

    public void Settings()
    {
        var vm = new SettingsViewModel();
        Content = vm;
    }
    public void Home()
    {
        var vm = new HomeViewModel();
        Content = vm;
    }

    public void About()
    {
        var vm = new AboutViewModel();
        Content = vm;
    }
    public void NewProfile()
    {
        var vm = new AddProfileViewModel();
        Content = vm;
    }
}