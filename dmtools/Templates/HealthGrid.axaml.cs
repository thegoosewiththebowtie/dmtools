using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Primitives;
using Config.Net;
using dmtools.PopUps;
using dmtools.Views;
using LiteDB;

namespace dmtools.Templates;

public class HealthGrid : TemplatedControl
{
    public static readonly StyledProperty<string> HealthValProperty = AvaloniaProperty.Register<HealthGrid, string>(
        "HealthVal", defaultValue:"10");
    public string HealthVal
    {
        get => GetValue(HealthValProperty);
        set => SetValue(HealthValProperty, value);
    }

    public static readonly StyledProperty<string> PlayerNameProperty = AvaloniaProperty.Register<HealthGrid, string>(
        "PlayerName");

    public string PlayerName
    {
        get => GetValue(PlayerNameProperty);
        set => SetValue(PlayerNameProperty, value);
    }

    public static readonly StyledProperty<int> IDProperty = AvaloniaProperty.Register<HealthGrid, int>(
        "ID");

    public int ID
    {
        get => GetValue(IDProperty);
        set => SetValue(IDProperty, value);
    }
    

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        Profile profile = new ConfigurationBuilder<Profile>().UseIniFile("Profile.ini").Build();
        var profid = profile.ProfileID;
        ISettings settings = new ConfigurationBuilder<ISettings>().UseIniFile("Settings/q0" + profid +".ini").Build();
        base.OnApplyTemplate(e);
        var pbtn = e.NameScope.Find<Button>("bPlus");
        pbtn.Click += (sender, args) =>
        {
            HealthVal = (Convert.ToInt32(HealthVal) + 1).ToString();
            if (ID == 999)
            {
                return;
            }
            if (PlayerName == "NPC")
            {
                using (var ldb = new LiteDatabase(settings.DataPath))
                {
                    var np = ldb.GetCollection<NPC>();
                    var npp = np.FindById(ID);
                    npp.Health = HealthVal;
                    np.Update(npp);
                }
                return;
            }
            using (var LdbPC = new LiteDatabase(settings.DataPath))
            {
                    var pc = LdbPC.GetCollection<PlayerCharacter>();
                    var p = pc.FindById(ID);
                    p.Health = HealthVal;
                    pc.Update(p);
            }
        };
        var mbtn = e.NameScope.Find<Button>("bMinus");
        mbtn.Click += (sender, args) =>
        {
            HealthVal = (Convert.ToInt32(HealthVal) - 1).ToString();
            if (ID == 999)
            {
                return;
            }
            if (PlayerName == "NPC")
            {
                using (var ldb = new LiteDatabase(settings.DataPath))
                {
                    var np = ldb.GetCollection<NPC>();
                    var npp = np.FindById(ID);
                    npp.Health = HealthVal;
                    np.Update(npp);
                }
                return;
            }
            using (var LdbPC = new LiteDatabase(settings.DataPath))
            {
                    var pc = LdbPC.GetCollection<PlayerCharacter>();
                    var p = pc.FindById(ID);
                    p.Health = HealthVal;
                    pc.Update(p);
            }
        };
    }
}