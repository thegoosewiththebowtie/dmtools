using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Primitives;
using Config.Net;
using dmtools.PopUps;
using dmtools.Views;
using LiteDB;

namespace dmtools.Templates;

public class NpcForm : TemplatedControl
{
    
    public int ID { get; set; }
    public static readonly StyledProperty<List<bool>> IsCheckProperty = AvaloniaProperty.Register<NpcForm, List<bool>>(
        "IsCheck");

    public List<bool> IsCheck
    {
        get => GetValue(IsCheckProperty);
        set => SetValue(IsCheckProperty, value);
    }
    
    public static readonly StyledProperty<string> FirstNameProperty = AvaloniaProperty.Register<NpcForm, string>(
        "FirstName");
    
    public string FirstName
    {
        get => GetValue(FirstNameProperty);
        set => SetValue(FirstNameProperty, value);
    }

    public static readonly StyledProperty<string> OtherNamesProperty = AvaloniaProperty.Register<NpcForm, string>(
        "OtherNames");

    public string OtherNames
    {
        get => GetValue(OtherNamesProperty);
        set => SetValue(OtherNamesProperty, value);
    }

    public static readonly StyledProperty<string> PlayerProperty = AvaloniaProperty.Register<NpcForm, string>(
        "Player");

    public string Player
    {
        get => GetValue(PlayerProperty);
        set => SetValue(PlayerProperty, value);
    }

    public static readonly StyledProperty<string> RaceProperty = AvaloniaProperty.Register<NpcForm, string>(
        "Race");

    public string Race
    {
        get => GetValue(RaceProperty);
        set => SetValue(RaceProperty, value);
    }

    public static readonly StyledProperty<string> ClassProperty = AvaloniaProperty.Register<NpcForm, string>(
        "Class");

    public string Class
    {
        get => GetValue(ClassProperty);
        set => SetValue(ClassProperty, value);
    }

    public static readonly StyledProperty<string> BackgrounddProperty = AvaloniaProperty.Register<NpcForm, string>(
        "Backgroundd");

    public string Backgroundd
    {
        get => GetValue(BackgrounddProperty);
        set => SetValue(BackgrounddProperty, value);
    }

    public static readonly StyledProperty<string> AlligmentProperty = AvaloniaProperty.Register<NpcForm, string>(
        "Alligment");

    public string Alligment
    {
        get => GetValue(AlligmentProperty);
        set => SetValue(AlligmentProperty, value);
    }

    public static readonly StyledProperty<string> NotesProperty = AvaloniaProperty.Register<NpcForm, string>(
        "Notes");

    public string Notes
    {
        get => GetValue(NotesProperty);
        set => SetValue(NotesProperty, value);
    }

    public static readonly StyledProperty<string> StrValProperty = AvaloniaProperty.Register<NpcForm, string>(
        "StrVal", defaultValue:"10");

    public string StrVal
    {
        get => GetValue(StrValProperty);
        set => SetValue(StrValProperty, value);
    }

    public static readonly StyledProperty<string> DexValProperty = AvaloniaProperty.Register<NpcForm, string>(
        "DexVal", defaultValue:"10");

    public string DexVal
    {
        get => GetValue(DexValProperty);
        set => SetValue(DexValProperty, value);
    }

    public static readonly StyledProperty<string> ConValProperty = AvaloniaProperty.Register<NpcForm, string>(
        "ConVal", defaultValue:"10");

    public string ConVal
    {
        get => GetValue(ConValProperty);
        set => SetValue(ConValProperty, value);
    }

    public static readonly StyledProperty<string> IntValProperty = AvaloniaProperty.Register<NpcForm, string>(
        "IntVal", defaultValue:"10");

    public string IntVal
    {
        get => GetValue(IntValProperty);
        set => SetValue(IntValProperty, value);
    }

    public static readonly StyledProperty<string> WisValProperty = AvaloniaProperty.Register<NpcForm, string>(
        "WisVal", defaultValue:"10");

    public string WisVal
    {
        get => GetValue(WisValProperty);
        set => SetValue(WisValProperty, value);
    }

    public static readonly StyledProperty<string> ChaValProperty = AvaloniaProperty.Register<NpcForm, string>(
        "ChaVal", defaultValue:"10");

    public string ChaVal
    {
        get => GetValue(ChaValProperty);
        set => SetValue(ChaValProperty, value);
    }

    public static readonly StyledProperty<string> LevelProperty = AvaloniaProperty.Register<NpcForm, string>(
        "Level");

    public string Level
    {
        get => GetValue(LevelProperty);
        set => SetValue(LevelProperty, value);
    }

    public static readonly StyledProperty<string> ExpProperty = AvaloniaProperty.Register<NpcForm, string>(
        "Exp");

    public string Exp
    {
        get => GetValue(ExpProperty);
        set => SetValue(ExpProperty, value);
    }

    public static readonly StyledProperty<string> HealthProperty = AvaloniaProperty.Register<NpcForm, string>(
        "Health");

    public string Health
    {
        get => GetValue(HealthProperty);
        set => SetValue(HealthProperty, value);
    }

    public static readonly StyledProperty<string> AcProperty = AvaloniaProperty.Register<NpcForm, string>(
        "Ac");

    public string Ac
    {
        get => GetValue(AcProperty);
        set => SetValue(AcProperty, value);
    }

    public static readonly StyledProperty<string> profbonProperty = AvaloniaProperty.Register<NpcForm, string>(
        "profbon");

    public string profbon
    {
        get => GetValue(profbonProperty);
        set => SetValue(profbonProperty, value);
    }
    
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    { 
        base.OnApplyTemplate(e);
        Profile profile = new ConfigurationBuilder<Profile>().UseIniFile("Profile.ini").Build();
        ISettings settings = new ConfigurationBuilder<ISettings>().UseIniFile("Settings/q0" + profile.ProfileID +".ini").Build();
        profbon ="+" + (Math.Ceiling(Convert.ToDecimal(Level) / 4) + 1).ToString();
        var dbtn = e.NameScope.Find<Button>("Delete");
        dbtn.Click += (sender, args) =>
        {
            Sure huh = new Sure(ID, 99);
            if (Avalonia.Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                huh.ShowDialog(desktop.MainWindow);
            }
        };
        var sbtn = e.NameScope.Find<Button>("Save");
        sbtn.Click += (sender, args) =>
        {
            using (var LdbPC = new LiteDatabase(settings.DataPath))
            {
                var pccol = LdbPC.GetCollection<NPC>();
                pccol.Delete(ID );
                pccol.Insert(new NPC()
                {
                    ID = ID, FirstName = FirstName, OtherName = OtherNames, Player = Player, Race = Race, Class = Class,
                    Backgroundd = Backgroundd, Alligment = Alligment, Notes = Notes, Strength = StrVal, Dexterity = DexVal,
                    Constitution = ConVal, Intelligence = IntVal, Wisdom = WisVal, Charisma = ChaVal, Level = Level,
                    Experience = Exp, Health = Health, ArmorClass = Ac, IsCCharisma = IsCheck[0], IsCWisdom = IsCheck[5],
                    IsCIntelligence = IsCheck[4], IsCConstitution = IsCheck[3], IsCDexterity = IsCheck[2], IsCStrength = IsCheck[1]
                });
            }
        };
        var lvl = e.NameScope.Find<plusminbuttonless>("LevelN");
        lvl.PropertyChanged += (sender, args) =>
        {
            profbon ="+" + (Math.Ceiling(Convert.ToDecimal(Level) / 4) + 1).ToString();
        };
    }
}