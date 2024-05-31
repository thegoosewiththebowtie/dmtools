using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace dmtools.Templates;

public class MIForm : TemplatedControl
{
    public static readonly StyledProperty<string> Name0Property = AvaloniaProperty.Register<MIForm, string>(
        "Name0");

    public string Name0
    {
        get => GetValue(Name0Property);
        set => SetValue(Name0Property, value);
    }

    public static readonly StyledProperty<string> eqcProperty = AvaloniaProperty.Register<MIForm, string>(
        "eqc");

    public string eqc
    {
        get => GetValue(eqcProperty);
        set => SetValue(eqcProperty, value);
    }

    public static readonly StyledProperty<string> rarityProperty = AvaloniaProperty.Register<MIForm, string>(
        "rarity");

    public string rarity
    {
        get => GetValue(rarityProperty);
        set => SetValue(rarityProperty, value);
    }

    public static readonly StyledProperty<string> descProperty = AvaloniaProperty.Register<MIForm, string>(
        "desc");

    public string desc
    {
        get => GetValue(descProperty);
        set => SetValue(descProperty, value);
    }
    
}