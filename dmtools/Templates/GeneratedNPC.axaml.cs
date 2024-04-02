using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using GLib;

namespace dmtools.Templates;

public class GeneratedNPC : TemplatedControl
{
    public static readonly StyledProperty<string> desProperty = AvaloniaProperty.Register<GeneratedNPC, string>(
        "des");

    public string des
    {
        get => GetValue(desProperty);
        set => SetValue(desProperty, value);
    }
    
    public static readonly StyledProperty<List<string>> likesProperty = AvaloniaProperty.Register<GeneratedNPC, List<string>>(
        "likes");

    public List<string> likes
    {
        get => GetValue(likesProperty);
        set => SetValue(likesProperty, value);
    }
    
    public static readonly StyledProperty<List<string>> dislikesProperty = AvaloniaProperty.Register<GeneratedNPC, List<string>>(
        "dislikes");

    public List<string> dislikes
    {
        get => GetValue(dislikesProperty);
        set => SetValue(dislikesProperty, value);
    }

    public static readonly StyledProperty<string> strProperty = AvaloniaProperty.Register<GeneratedNPC, string>(
        "str");

    public string str
    {
        get => GetValue(strProperty);
        set => SetValue(strProperty, value);
    }

    public static readonly StyledProperty<string> dexProperty = AvaloniaProperty.Register<GeneratedNPC, string>(
        "dex");

    public string dex
    {
        get => GetValue(dexProperty);
        set => SetValue(dexProperty, value);
    }

    public static readonly StyledProperty<string> conProperty = AvaloniaProperty.Register<GeneratedNPC, string>(
        "con");

    public string con
    {
        get => GetValue(conProperty);
        set => SetValue(conProperty, value);
    }

    public static readonly StyledProperty<string> wisProperty = AvaloniaProperty.Register<GeneratedNPC, string>(
        "wis");

    public string wis
    {
        get => GetValue(wisProperty);
        set => SetValue(wisProperty, value);
    }

    public static readonly StyledProperty<string> inteProperty = AvaloniaProperty.Register<GeneratedNPC, string>(
        "inte");

    public string inte
    {
        get => GetValue(inteProperty);
        set => SetValue(inteProperty, value);
    }

    public static readonly StyledProperty<string> chaProperty = AvaloniaProperty.Register<GeneratedNPC, string>(
        "cha");

    public string cha
    {
        get => GetValue(chaProperty);
        set => SetValue(chaProperty, value);
    }
}