﻿using System;
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
            if (Convert.ToInt32(theval) < 20)
            {
                theval = (Convert.ToInt32(theval) + 1).ToString("0");
            }
        };
        var dsbtn = e.NameScope.Find<Button>("lvlmn1");
        dsbtn.Click += (sender, args) =>
        {
            if (Convert.ToInt32(theval) > 0)
            {
                theval = (Convert.ToInt32(theval) - 1).ToString("0");
            }
        };
    }
}