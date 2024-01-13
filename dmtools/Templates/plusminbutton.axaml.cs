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
    
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        var pbtn = e.NameScope.Find<Button>("plus");
        modmodd = ((Convert.ToInt32(Value0) - 10) / 2).ToString("0");
        pbtn.Click += (s, e) =>
        {
            if (Convert.ToInt32(Value0) < 20)
            {
                Value0 = (Convert.ToInt32(Value0) + 1).ToString();
                modmodd = ((Convert.ToInt32(Value0) - 10) / 2).ToString("0");
            }
    
        };
        var mbtn = e.NameScope.Find<Button>("minus");
        mbtn.Click += (s, e) =>
        {
            if (Convert.ToInt32(Value0) > 0)
            {
                Value0 = (Convert.ToInt32(Value0) - 1).ToString();
                modmodd = ((Convert.ToInt32(Value0) - 10) / 2).ToString("0");
            }
        };
    }
}
