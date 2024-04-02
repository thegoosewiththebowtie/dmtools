using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace dmtools.Templates;

public class SpellDisplay : TemplatedControl
{
    public static readonly StyledProperty<string> LevelProperty = AvaloniaProperty.Register<SpellDisplay, string>(
        "Level");

    public string Level
    {
        get => GetValue(LevelProperty);
        set => SetValue(LevelProperty, value);
    }

    public static readonly StyledProperty<string> SpellNameProperty = AvaloniaProperty.Register<SpellDisplay, string>(
        "SpellName");

    public string SpellName
    {
        get => GetValue(SpellNameProperty);
        set => SetValue(SpellNameProperty, value);
    }

    public static readonly StyledProperty<string> SchoolProperty = AvaloniaProperty.Register<SpellDisplay, string>(
        "School");

    public string School
    {
        get => GetValue(SchoolProperty);
        set => SetValue(SchoolProperty, value);
    }

    public static readonly StyledProperty<string> RangeProperty = AvaloniaProperty.Register<SpellDisplay, string>(
        "Range");

    public string Range
    {
        get => GetValue(RangeProperty);
        set => SetValue(RangeProperty, value);
    }

    public static readonly StyledProperty<string> DurationProperty = AvaloniaProperty.Register<SpellDisplay, string>(
        "Duration");

    public string Duration
    {
        get => GetValue(DurationProperty);
        set => SetValue(DurationProperty, value);
    }

    public static readonly StyledProperty<string> CastingTimeProperty = AvaloniaProperty.Register<SpellDisplay, string>(
        "CastingTime");

    public string CastingTime
    {
        get => GetValue(CastingTimeProperty);
        set => SetValue(CastingTimeProperty, value);
    }

    public static readonly StyledProperty<string> AreaProperty = AvaloniaProperty.Register<SpellDisplay, string>(
        "Area");

    public string Area
    {
        get => GetValue(AreaProperty);
        set => SetValue(AreaProperty, value);
    }

    public static readonly StyledProperty<string> DescProperty = AvaloniaProperty.Register<SpellDisplay, string>(
        "Desc");

    public string Desc
    {
        get => GetValue(DescProperty);
        set => SetValue(DescProperty, value);
    }

    public static readonly StyledProperty<string> classesProperty = AvaloniaProperty.Register<SpellDisplay, string>(
        "classes");

    public string classes
    {
        get => GetValue(classesProperty);
        set => SetValue(classesProperty, value);
    }

    public static readonly StyledProperty<string> subclassesProperty = AvaloniaProperty.Register<SpellDisplay, string>(
        "subclasses");

    public string subclasses
    {
        get => GetValue(subclassesProperty);
        set => SetValue(subclassesProperty, value);
    }

    public static readonly StyledProperty<string> dctypensucessProperty = AvaloniaProperty.Register<SpellDisplay, string>(
        "dctypensucess");

    public string dctypensucess
    {
        get => GetValue(dctypensucessProperty);
        set => SetValue(dctypensucessProperty, value);
    }

    public static readonly StyledProperty<string> damageProperty = AvaloniaProperty.Register<SpellDisplay, string>(
        "damage");

    public string damage
    {
        get => GetValue(damageProperty);
        set => SetValue(damageProperty, value);
    }

    public static readonly StyledProperty<List<string>> LevelsProperty = AvaloniaProperty.Register<SpellDisplay, List<string>>(
        "Levels");

    public List<string> Levels
    {
        get => GetValue(LevelsProperty);
        set => SetValue(LevelsProperty, value);
    }

    public static readonly StyledProperty<bool> RitualProperty = AvaloniaProperty.Register<SpellDisplay, bool>(
        "Ritual");

    public bool Ritual
    {
        get => GetValue(RitualProperty);
        set => SetValue(RitualProperty, value);
    }

    public static readonly StyledProperty<bool> ConcProperty = AvaloniaProperty.Register<SpellDisplay, bool>(
        "Conc");

    public bool Conc
    {
        get => GetValue(ConcProperty);
        set => SetValue(ConcProperty, value);
    }
    
    public static readonly StyledProperty<bool> VerbalProperty = AvaloniaProperty.Register<SpellDisplay, bool>(
        "Verbal");

    public bool Verbal
    {
        get => GetValue(VerbalProperty);
        set => SetValue(VerbalProperty, value);
    }

    public static readonly StyledProperty<bool> SomaticProperty = AvaloniaProperty.Register<SpellDisplay, bool>(
        "Somatic");

    public bool Somatic
    {
        get => GetValue(SomaticProperty);
        set => SetValue(SomaticProperty, value);
    }

    public static readonly StyledProperty<bool> MaterialProperty = AvaloniaProperty.Register<SpellDisplay, bool>(
        "Material");

    public bool Material
    {
        get => GetValue(MaterialProperty);
        set => SetValue(MaterialProperty, value);
    }

    public static readonly StyledProperty<string> MaterialsProperty = AvaloniaProperty.Register<SpellDisplay, string>(
        "Materials", "none");

    public string Materials
    {
        get => GetValue(MaterialsProperty);
        set => SetValue(MaterialsProperty, value);
    }
    
}