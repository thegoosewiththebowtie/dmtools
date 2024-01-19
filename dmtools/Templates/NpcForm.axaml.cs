using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Primitives;
using Config.Net;
using dmtools.Resources;
using dmtools.Views;
using LiteDB;

namespace dmtools.Templates;

public class NpcForm : TemplatedControl
{
    ISettings settings = new ConfigurationBuilder<ISettings>().UseIniFile("Settings.ini").Build();
    
    public int ID { get; set; }
    public static readonly StyledProperty<string> FirstNameProperty = AvaloniaProperty.Register<CharForm, string>(
        "FirstName");

    public string FirstName
    {
        get => GetValue(FirstNameProperty);
        set => SetValue(FirstNameProperty, value);
    }

    public static readonly StyledProperty<string> OtherNamesProperty = AvaloniaProperty.Register<CharForm, string>(
        "OtherNames");

    public string OtherNames
    {
        get => GetValue(OtherNamesProperty);
        set => SetValue(OtherNamesProperty, value);
    }

    public static readonly StyledProperty<string> PlayerProperty = AvaloniaProperty.Register<CharForm, string>(
        "Player");

    public string Player
    {
        get => GetValue(PlayerProperty);
        set => SetValue(PlayerProperty, value);
    }

    public static readonly StyledProperty<string> RaceProperty = AvaloniaProperty.Register<CharForm, string>(
        "Race");

    public string Race
    {
        get => GetValue(RaceProperty);
        set => SetValue(RaceProperty, value);
    }

    public static readonly StyledProperty<string> ClassProperty = AvaloniaProperty.Register<CharForm, string>(
        "Class");

    public string Class
    {
        get => GetValue(ClassProperty);
        set => SetValue(ClassProperty, value);
    }

    public static readonly StyledProperty<string> BackgrounddProperty = AvaloniaProperty.Register<CharForm, string>(
        "Backgroundd");

    public string Backgroundd
    {
        get => GetValue(BackgrounddProperty);
        set => SetValue(BackgrounddProperty, value);
    }

    public static readonly StyledProperty<string> AlligmentProperty = AvaloniaProperty.Register<CharForm, string>(
        "Alligment");

    public string Alligment
    {
        get => GetValue(AlligmentProperty);
        set => SetValue(AlligmentProperty, value);
    }

    public static readonly StyledProperty<string> NotesProperty = AvaloniaProperty.Register<CharForm, string>(
        "Notes");

    public string Notes
    {
        get => GetValue(NotesProperty);
        set => SetValue(NotesProperty, value);
    }

    public static readonly StyledProperty<string> StrValProperty = AvaloniaProperty.Register<CharForm, string>(
        "StrVal", defaultValue:"10");

    public string StrVal
    {
        get => GetValue(StrValProperty);
        set => SetValue(StrValProperty, value);
    }

    public static readonly StyledProperty<string> DexValProperty = AvaloniaProperty.Register<CharForm, string>(
        "DexVal", defaultValue:"10");

    public string DexVal
    {
        get => GetValue(DexValProperty);
        set => SetValue(DexValProperty, value);
    }

    public static readonly StyledProperty<string> ConValProperty = AvaloniaProperty.Register<CharForm, string>(
        "ConVal", defaultValue:"10");

    public string ConVal
    {
        get => GetValue(ConValProperty);
        set => SetValue(ConValProperty, value);
    }

    public static readonly StyledProperty<string> IntValProperty = AvaloniaProperty.Register<CharForm, string>(
        "IntVal", defaultValue:"10");

    public string IntVal
    {
        get => GetValue(IntValProperty);
        set => SetValue(IntValProperty, value);
    }

    public static readonly StyledProperty<string> WisValProperty = AvaloniaProperty.Register<CharForm, string>(
        "WisVal", defaultValue:"10");

    public string WisVal
    {
        get => GetValue(WisValProperty);
        set => SetValue(WisValProperty, value);
    }

    public static readonly StyledProperty<string> ChaValProperty = AvaloniaProperty.Register<CharForm, string>(
        "ChaVal", defaultValue:"10");

    public string ChaVal
    {
        get => GetValue(ChaValProperty);
        set => SetValue(ChaValProperty, value);
    }

    public static readonly StyledProperty<string> LevelProperty = AvaloniaProperty.Register<CharForm, string>(
        "Level");

    public string Level
    {
        get => GetValue(LevelProperty);
        set => SetValue(LevelProperty, value);
    }

    public static readonly StyledProperty<string> ExpProperty = AvaloniaProperty.Register<CharForm, string>(
        "Exp");

    public string Exp
    {
        get => GetValue(ExpProperty);
        set => SetValue(ExpProperty, value);
    }

    public static readonly StyledProperty<string> HealthProperty = AvaloniaProperty.Register<CharForm, string>(
        "Health");

    public string Health
    {
        get => GetValue(HealthProperty);
        set => SetValue(HealthProperty, value);
    }

    public static readonly StyledProperty<string> AcProperty = AvaloniaProperty.Register<CharForm, string>(
        "Ac");

    public string Ac
    {
        get => GetValue(AcProperty);
        set => SetValue(AcProperty, value);
    }

    public static readonly StyledProperty<string> profbonProperty = AvaloniaProperty.Register<CharForm, string>(
        "profbon");

    public string profbon
    {
        get => GetValue(profbonProperty);
        set => SetValue(profbonProperty, value);
    }
    
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        var dbtn = e.NameScope.Find<Button>("Delete");
        profbon ="+" + (Math.Floor(Convert.ToDecimal(Level) / 4) + 1).ToString();
        dbtn.Click += (sender, args) =>
        {
            Sure huh = new Sure(ID);
            if (Avalonia.Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                huh.ShowDialog(desktop.MainWindow);
            }
        };
        var sbtn = e.NameScope.Find<Button>("Save");
        sbtn.Click += (sender, args) =>
        {
            using (var LdbPC = new LiteDatabase(settings.Profile))
            {
                var pccol = LdbPC.GetCollection<PlayerCharacter>();
                pccol.Delete(ID );
                pccol.Insert(new PlayerCharacter
                {
                    ID = ID, FirstName = FirstName, OtherName = OtherNames, Player = Player, Race = Race, Class = Class,
                    Backgroundd = Backgroundd, Alligment = Alligment, Notes = Notes, Strength = StrVal, Dexterity = DexVal,
                    Constitution = ConVal, Intelligence = IntVal, Wisdom = WisVal, Charisma = ChaVal, Level = Level,
                    Experience = Exp, Health = Health, ArmorClass = Ac
                });
            }
        };
    }

}