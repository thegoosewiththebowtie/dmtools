﻿using System.IO;
using Config.Net;
using dmtools.Views;
using ReactiveUI;

namespace dmtools.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    ViewModelBase _content;
    ViewModelBase _media0;
    ViewModelBase _home0;
    public MainWindowViewModel()
    {
        Profile profile = new ConfigurationBuilder<Profile>().UseIniFile("Profile.ini").Build();
        DirectoryInfo prof = new DirectoryInfo("Settings"); 
        if (prof.GetFiles().Length == 0 || profile.ProfileID == 0)
        {
           Content = new AddProfileViewModel();
        }
        else
        {
            Media0 = new MediaViewModel();
            Home0 = new HomeViewModel();
        }
    }
    

    public ViewModelBase Content
    {
        get => _content;
        private set => this.RaiseAndSetIfChanged(ref _content, value);
    }
    public ViewModelBase Home0
    {
        get => _home0;
        private set => this.RaiseAndSetIfChanged(ref _home0, value);
    }
    public ViewModelBase Media0
    {
        get => _media0;
        private set => this.RaiseAndSetIfChanged(ref _media0, value);
    }

    //HomeViewModel List { get; }


    public void Home()
    {
        Content = null;
        if (Media0 == null)
        {
            Media0 = new MediaViewModel();
        }
        if (Home0 == null)
        {
            Home0 = new HomeViewModel();
        }
    }

    public void Gens()
    {
        var vm = new GensViewModel();
        Content = vm;
    }
    public void Glossary()
    {
        var vm = new GlossaryViewModel();
        Content = vm;
    }
    
    public void Bot()
    {
        var vm = new BotTestViewModel();
        Content = vm;
    }
    public void Settings()
    {
        var vm = new SettingsViewModel();
        Content = vm;
        Home0 = null;
        Media0 = null;
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