using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace dmtools.Templates;

public class SpellDisplay : TemplatedControl
{
    public static readonly StyledProperty<int> LevelProperty = AvaloniaProperty.Register<SpellDisplay, int>(
        "Level");

    public int Level
    {
        get => GetValue(LevelProperty);
        set => SetValue(LevelProperty, value);
    }

    public static readonly StyledProperty<string> TimeProperty = AvaloniaProperty.Register<SpellDisplay, string>(
        "Time");

    public string Time
    {
        get => GetValue(TimeProperty);
        set => SetValue(TimeProperty, value);
    }

    public static readonly StyledProperty<string> RangeProperty = AvaloniaProperty.Register<SpellDisplay, string>(
        "Range");

    public string Range
    {
        get => GetValue(RangeProperty);
        set => SetValue(RangeProperty, value);
    }
    
    
    
    
}