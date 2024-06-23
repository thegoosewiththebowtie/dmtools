using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace dmtools.Templates;

public class EncForm : TemplatedControl
{
    public static readonly StyledProperty<string> HealthProperty = AvaloniaProperty.Register<BestDisplay, string>(
        "Health");

    public string Health
    {
        get => GetValue(HealthProperty);
        set => SetValue(HealthProperty, value);
    }

    public static readonly StyledProperty<string> HitPointsRollProperty = AvaloniaProperty.Register<BestDisplay, string>(
        "HitPointsRoll");

    public string HitPointsRoll
    {
        get => GetValue(HitPointsRollProperty);
        set => SetValue(HitPointsRollProperty, value);
    }

    public static readonly StyledProperty<string> ExpProperty = AvaloniaProperty.Register<BestDisplay, string>(
        "Exp");

    public string Exp
    {
        get => GetValue(ExpProperty);
        set => SetValue(ExpProperty, value);
    }

    public static readonly StyledProperty<string> MainACvalProperty = AvaloniaProperty.Register<BestDisplay, string>(
        "MainACval");

    public string MainACval
    {
        get => GetValue(MainACvalProperty);
        set => SetValue(MainACvalProperty, value);
    }

    public static readonly StyledProperty<string> ACTypeProperty = AvaloniaProperty.Register<BestDisplay, string>(
        "ACType");

    public string ACType
    {
        get => GetValue(ACTypeProperty);
        set => SetValue(ACTypeProperty, value);
    }
    
    public static readonly StyledProperty<string> BNameProperty = AvaloniaProperty.Register<BestDisplay, string>(
        "BName");

    public string BName
    {
        get => GetValue(BNameProperty);
        set => SetValue(BNameProperty, value);
    }

    public static readonly StyledProperty<string> SizeProperty = AvaloniaProperty.Register<BestDisplay, string>(
        "Size");

    public string Size
    {
        get => GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    public static readonly StyledProperty<string> TypeProperty = AvaloniaProperty.Register<BestDisplay, string>(
        "Type");

    public string Type
    {
        get => GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    public static readonly StyledProperty<string> SubtypeProperty = AvaloniaProperty.Register<BestDisplay, string>(
        "Subtype");

    public string Subtype
    {
        get => GetValue(SubtypeProperty);
        set => SetValue(SubtypeProperty, value);
    }
    
    public static readonly StyledProperty<string> AligmentProperty = AvaloniaProperty.Register<BestDisplay, string>(
        "Aligment");

    public string Aligment
    {
        get => GetValue(AligmentProperty);
        set => SetValue(AligmentProperty, value);
    }

    public static readonly StyledProperty<string> LanguagesProperty = AvaloniaProperty.Register<BestDisplay, string>(
        "Languages");

    public string Languages
    {
        get => GetValue(LanguagesProperty);
        set => SetValue(LanguagesProperty, value);
    }

    public static readonly StyledProperty<string> SpeedProperty = AvaloniaProperty.Register<BestDisplay, string>(
        "Speed");

    public string Speed
    {
        get => GetValue(SpeedProperty);
        set => SetValue(SpeedProperty, value);
    }

    public static readonly StyledProperty<string> SwimProperty = AvaloniaProperty.Register<BestDisplay, string>(
        "Swim");

    public string Swim
    {
        get => GetValue(SwimProperty);
        set => SetValue(SwimProperty, value);
    }

    public static readonly StyledProperty<string> FlyProperty = AvaloniaProperty.Register<BestDisplay, string>(
        "Fly");

    public string Fly
    {
        get => GetValue(FlyProperty);
        set => SetValue(FlyProperty, value);
    }

    public static readonly StyledProperty<string> BurrowProperty = AvaloniaProperty.Register<BestDisplay, string>(
        "Burrow");

    public string Burrow
    {
        get => GetValue(BurrowProperty);
        set => SetValue(BurrowProperty, value);
    }

    public static readonly StyledProperty<string> ClimbProperty = AvaloniaProperty.Register<BestDisplay, string>(
        "Climb");

    public string Climb
    {
        get => GetValue(ClimbProperty);
        set => SetValue(ClimbProperty, value);
    }

    public static readonly StyledProperty<string> ImmunitiesProperty = AvaloniaProperty.Register<BestDisplay, string>(
        "Immunities");

    public string Immunities
    {
        get => GetValue(ImmunitiesProperty);
        set => SetValue(ImmunitiesProperty, value);
    }

    public static readonly StyledProperty<string> SensesProperty = AvaloniaProperty.Register<BestDisplay, string>(
        "Senses");

    public string Senses
    {
        get => GetValue(SensesProperty);
        set => SetValue(SensesProperty, value);
    }

    public static readonly StyledProperty<string> ArmorProperty = AvaloniaProperty.Register<BestDisplay, string>(
        "Armor");

    public string Armor
    {
        get => GetValue(ArmorProperty);
        set => SetValue(ArmorProperty, value);
    }

    public static readonly StyledProperty<string> DescProperty = AvaloniaProperty.Register<BestDisplay, string>(
        "Desc");

    public string Desc
    {
        get => GetValue(DescProperty);
        set => SetValue(DescProperty, value);
    }

    public static readonly StyledProperty<string> AttacksProperty = AvaloniaProperty.Register<BestDisplay, string>(
        "Attacks");

    public string Attacks
    {
        get => GetValue(AttacksProperty);
        set => SetValue(AttacksProperty, value);
    }
    public static readonly StyledProperty<string> StrValProperty = AvaloniaProperty.Register<CharForm, string>(
        "StrVal", defaultValue:"10");

    public string StrVal
    {
        get => GetValue(StrValProperty);
        set => SetValue(StrValProperty, value);
    }

    public static readonly StyledProperty<string> DexValProperty = AvaloniaProperty.Register<CharForm, string>(
        "DexVal", defaultValue:"10");

    public string DexVal
    {
        get => GetValue(DexValProperty);
        set => SetValue(DexValProperty, value);
    }

    public static readonly StyledProperty<string> ConValProperty = AvaloniaProperty.Register<CharForm, string>(
        "ConVal", defaultValue:"10");

    public string ConVal
    {
        get => GetValue(ConValProperty);
        set => SetValue(ConValProperty, value);
    }

    public static readonly StyledProperty<string> IntValProperty = AvaloniaProperty.Register<CharForm, string>(
        "IntVal", defaultValue:"10");

    public string IntVal
    {
        get => GetValue(IntValProperty);
        set => SetValue(IntValProperty, value);
    }

    public static readonly StyledProperty<string> WisValProperty = AvaloniaProperty.Register<CharForm, string>(
        "WisVal", defaultValue:"10");

    public string WisVal
    {
        get => GetValue(WisValProperty);
        set => SetValue(WisValProperty, value);
    }

    public static readonly StyledProperty<string> ChaValProperty = AvaloniaProperty.Register<CharForm, string>(
        "ChaVal", defaultValue:"10");

    public string ChaVal
    {
        get => GetValue(ChaValProperty);
        set => SetValue(ChaValProperty, value);
    }

    public static readonly StyledProperty<string> VulProperty = AvaloniaProperty.Register<BestDisplay, string>(
        "Vul");

    public string Vul
    {
        get => GetValue(VulProperty);
        set => SetValue(VulProperty, value);
    }

    public static readonly StyledProperty<string> AbilitiesProperty = AvaloniaProperty.Register<BestDisplay, string>(
        "Abilities");

    public string Abilities
    {
        get => GetValue(AbilitiesProperty);
        set => SetValue(AbilitiesProperty, value);
    }
}