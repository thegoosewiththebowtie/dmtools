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

public class HealthGrid : TemplatedControl
{
    ISettings settings = new ConfigurationBuilder<ISettings>().UseIniFile("Settings.ini").Build();
    public static readonly StyledProperty<string> HealthValProperty = AvaloniaProperty.Register<HealthGrid, string>(
        "HealthVal", defaultValue:"10");
    public string HealthVal
    {
        get => GetValue(HealthValProperty);
        set => SetValue(HealthValProperty, value);
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
        base.OnApplyTemplate(e);
        var pbtn = e.NameScope.Find<Button>("bPlus");
        pbtn.Click += (sender, args) =>
        {
            HealthVal = (Convert.ToInt32(HealthVal) + 1).ToString();
            using (var LdbPC = new LiteDatabase(settings.Profile))
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
            using (var LdbPC = new LiteDatabase(settings.Profile))
            {
                var pc = LdbPC.GetCollection<PlayerCharacter>();
                var p = pc.FindById(ID);
                p.Health = HealthVal;
                pc.Update(p);
            }
        };
    }
}