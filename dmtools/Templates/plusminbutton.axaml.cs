using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace dmtools.Templates;

public class plusminbutton : TemplatedControl
{
    
    public static readonly StyledProperty<string> Value0Property = AvaloniaProperty.Register<plusminbutton, string>(
        nameof(Value0), defaultValue:"10");

    public string Value0
    {
        get => GetValue(Value0Property);
        set => SetValue(Value0Property, value);
    }

    public static readonly StyledProperty<bool> ischeckedProperty = AvaloniaProperty.Register<plusminbutton, bool>(
        "ischecked", defaultValue:false);

    public bool ischecked
    {
        get => GetValue(ischeckedProperty);
        set => SetValue(ischeckedProperty, value);
    }

    public static readonly StyledProperty<string> LabllProperty = AvaloniaProperty.Register<plusminbutton, string>(
        nameof(Labll), defaultValue:"Strength");

    public string Labll
    {
        get => GetValue(LabllProperty);
        set => SetValue(LabllProperty, value);
    }

    public static readonly StyledProperty<string> modmoddProperty = AvaloniaProperty.Register<plusminbutton, string>(
        "modmodd", defaultValue:"0");

    public string modmodd
    {
        get => GetValue(modmoddProperty);
        set => SetValue(modmoddProperty, value);
    }

    public static readonly StyledProperty<string> profProperty = AvaloniaProperty.Register<plusminbutton, string>(
        "prof", defaultValue:"2");

    public string prof
    {
        get => GetValue(profProperty);
        set => SetValue(profProperty, value);
    }
    
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        modmodd = ((int)Math.Floor(Convert.ToDecimal((Convert.ToInt32(Value0) - 10) / 2))).ToString();
        if (e.NameScope.Find<ToggleButton>("pro").IsChecked == true)
        {
            modmodd = (Convert.ToInt32(modmodd) + Convert.ToInt32(prof)).ToString();
        }
        var pbtn = e.NameScope.Find<Button>("plus");
        pbtn.Click += (s, e) =>
        {
                Value0 = (Convert.ToInt32(Value0) + 1).ToString();
                modmodd = (Math.Floor((Convert.ToDecimal(Value0) - 10) / 2)).ToString();
    
        };
        var mbtn = e.NameScope.Find<Button>("minus");
        mbtn.Click += (s, e) =>
        {
            if (Convert.ToInt32(Value0) > 1)
            {
                Value0 = (Convert.ToInt32(Value0) - 1).ToString();
                modmodd = ((int)Math.Floor(Convert.ToDecimal((Convert.ToInt32(Value0) - 10) / 2))).ToString();
            }
        };
        var probtn = e.NameScope.Find<ToggleButton>("pro");
        probtn.IsCheckedChanged += (s, e) =>
        {
            if ((s as ToggleButton).IsChecked == true)
            {
                modmodd = (Convert.ToInt32(modmodd) + Convert.ToInt32(prof)).ToString();
                ischecked = true;
            }
            if ((s as ToggleButton).IsChecked == false)
            {
                modmodd = (Convert.ToInt32(modmodd) - Convert.ToInt32(prof)).ToString();
                ischecked = false;
            }
        };
    }
}
