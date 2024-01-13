using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace dmtools.Templates;

public class Longplusminmore : TemplatedControl
{
    public static readonly StyledProperty<string> labellssProperty = AvaloniaProperty.Register<Longplusminmore, string>(
        "labellss", defaultValue:"Level");

    public string labellss
    {
        get => GetValue(labellssProperty);
        set => SetValue(labellssProperty, value);
    }

    public static readonly StyledProperty<string> thevalProperty = AvaloniaProperty.Register<Longplusminmore, string>(
        "theval", defaultValue:"0");

    public string theval
    {
        get => GetValue(thevalProperty);
        set => SetValue(thevalProperty, value);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        var dbtn = e.NameScope.Find<Button>("lvlpl1");
        dbtn.Click += (sender, args) =>
        {
                theval = (Convert.ToInt32(theval) + 1).ToString("0");
        };
        var dsbtn = e.NameScope.Find<Button>("lvlmn1");
        dsbtn.Click += (sender, args) =>
        {
                theval = (Convert.ToInt32(theval) - 1).ToString("0");
        };
        base.OnApplyTemplate(e);
        var dbtn1 = e.NameScope.Find<Button>("lvlpl10");
        dbtn1.Click += (sender, args) =>
        {
                theval = (Convert.ToInt32(theval) + 10).ToString("0");
        };
        var dsbtn1 = e.NameScope.Find<Button>("lvlmn10");
        dsbtn1.Click += (sender, args) =>
        {
                theval = (Convert.ToInt32(theval) - 10).ToString("0");
        };
        base.OnApplyTemplate(e);
        var dbtn11 = e.NameScope.Find<Button>("lvlpl100");
        dbtn11.Click += (sender, args) =>
        {
                theval = (Convert.ToInt32(theval) + 100).ToString("0");
        };
        var dsbtn11 = e.NameScope.Find<Button>("lvlmn100");
        dsbtn11.Click += (sender, args) =>
        {
                theval = (Convert.ToInt32(theval) - 100).ToString("0");
        };
    }
}