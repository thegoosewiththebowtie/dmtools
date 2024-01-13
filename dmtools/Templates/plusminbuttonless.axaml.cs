using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace dmtools.Templates;

public class plusminbuttonless : TemplatedControl
{
    
    public static readonly StyledProperty<string> Value0Property = AvaloniaProperty.Register<plusminbuttonless, string>(
        nameof(Value0), defaultValue:"10");

    public string Value0
    {
        get => GetValue(Value0Property);
        set => SetValue(Value0Property, value);
    }

    public static readonly StyledProperty<string> LabllProperty = AvaloniaProperty.Register<plusminbuttonless, string>(
        nameof(Labll));

    public string Labll
    {
        get => GetValue(LabllProperty);
        set => SetValue(LabllProperty, value);
    }
    
    
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        var pbtn = e.NameScope.Find<Button>("plus");
        pbtn.Click += (s, e) =>
        {
                Value0 = (Convert.ToInt32(Value0) + 1).ToString();
        };
        var mbtn = e.NameScope.Find<Button>("minus");
        mbtn.Click += (s, e) =>
        {
                Value0 = (Convert.ToInt32(Value0) - 1).ToString();
        };
    }
}
