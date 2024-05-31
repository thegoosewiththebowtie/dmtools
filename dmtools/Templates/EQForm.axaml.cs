using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Pango;

namespace dmtools.Templates;

public class EQForm : TemplatedControl
{
    public static readonly StyledProperty<string> Name0Property = AvaloniaProperty.Register<MIForm, string>(
        "Name0");

    public string Name0
    {
        get => GetValue(Name0Property);
        set => SetValue(Name0Property, value);
    }


    public static readonly StyledProperty<bool> WeaponProperty = AvaloniaProperty.Register<EQForm, bool>(
        "Weapon", false);

    public bool Weapon
    {
        get => GetValue(WeaponProperty);
        set => SetValue(WeaponProperty, value);
    }

    public static readonly StyledProperty<bool> ArmorProperty = AvaloniaProperty.Register<EQForm, bool>(
        "Armor", false);

    public bool Armor
    {
        get => GetValue(ArmorProperty);
        set => SetValue(ArmorProperty, value);
    }

    public static readonly StyledProperty<bool> VehicleProperty = AvaloniaProperty.Register<EQForm, bool>(
        "Vehicle",false);

    public bool Vehicle
    {
        get => GetValue(VehicleProperty);
        set => SetValue(VehicleProperty, value);
    }

    public static readonly StyledProperty<bool> GearProperty = AvaloniaProperty.Register<EQForm, bool>(
        "Gear",false);

    public bool Gear
    {
        get => GetValue(GearProperty);
        set => SetValue(GearProperty, value);
    }

    public static readonly StyledProperty<bool> ToolProperty = AvaloniaProperty.Register<EQForm, bool>(
        "Tool",false);

    public bool Tool
    {
        get => GetValue(ToolProperty);
        set => SetValue(ToolProperty, value);
    }

    public static readonly StyledProperty<string> CategoryProperty = AvaloniaProperty.Register<EQForm, string>(
        "Category");

    public string Category
    {
        get => GetValue(CategoryProperty);
        set => SetValue(CategoryProperty, value);
    }

    public static readonly StyledProperty<string> SubCategoryProperty = AvaloniaProperty.Register<EQForm, string>(
        "SubCategory");

    public string SubCategory
    {
        get => GetValue(SubCategoryProperty);
        set => SetValue(SubCategoryProperty, value);
    }

    public static readonly StyledProperty<string> DescProperty = AvaloniaProperty.Register<EQForm, string>(
        "Desc");

    public string Desc
    {
        get => GetValue(DescProperty);
        set => SetValue(DescProperty, value);
    }

    //wep
    
    public static readonly StyledProperty<string> DamDpTProperty = AvaloniaProperty.Register<EQForm, string>(
        "DamDpT");

    public string DamDpT
    {
        get => GetValue(DamDpTProperty);
        set => SetValue(DamDpTProperty, value);
    }

    public static readonly StyledProperty<string> THdamProperty = AvaloniaProperty.Register<EQForm, string>(
        "THdam");

    public string THdam
    {
        get => GetValue(THdamProperty);
        set => SetValue(THdamProperty, value);
    }

    public static readonly StyledProperty<string> NormalRangeProperty = AvaloniaProperty.Register<EQForm, string>(
        "NormalRange");

    public string NormalRange
    {
        get => GetValue(NormalRangeProperty);
        set => SetValue(NormalRangeProperty, value);
    }

    public static readonly StyledProperty<string> LongRangeProperty = AvaloniaProperty.Register<EQForm, string>(
        "LongRange");

    public string LongRange
    {
        get => GetValue(LongRangeProperty);
        set => SetValue(LongRangeProperty, value);
    }

    public static readonly StyledProperty<string> ThrowingRangeProperty = AvaloniaProperty.Register<EQForm, string>(
        "ThrowingRange");

    public string ThrowingRange
    {
        get => GetValue(ThrowingRangeProperty);
        set => SetValue(ThrowingRangeProperty, value);
    }

    public static readonly StyledProperty<string> LongThrowingRangeProperty = AvaloniaProperty.Register<EQForm, string>(
        "LongThrowingRange");

    public string LongThrowingRange
    {
        get => GetValue(LongThrowingRangeProperty);
        set => SetValue(LongThrowingRangeProperty, value);
    }

    public static readonly StyledProperty<string> TagsProperty = AvaloniaProperty.Register<EQForm, string>(
        "Tags");

    public string Tags
    {
        get => GetValue(TagsProperty);
        set => SetValue(TagsProperty, value);
    }
    
    //arm
    public static readonly StyledProperty<string> BaseACProperty = AvaloniaProperty.Register<EQForm, string>(
        "BaseAC");

    public string BaseAC
    {
        get => GetValue(BaseACProperty);
        set => SetValue(BaseACProperty, value);
    }

    public static readonly StyledProperty<bool> huhProperty = AvaloniaProperty.Register<EQForm, bool>(
        "huh");

    public bool huh
    {
        get => GetValue(huhProperty);
        set => SetValue(huhProperty, value);
    }
    
    
    public static readonly StyledProperty<string> DexBonProperty = AvaloniaProperty.Register<EQForm, string>(
        "DexBon");

    public string DexBon
    {
        get => GetValue(DexBonProperty);
        set => SetValue(DexBonProperty, value);
    }

    public static readonly StyledProperty<string> MinStrProperty = AvaloniaProperty.Register<EQForm, string>(
        "MinStr");

    public string MinStr
    {
        get => GetValue(MinStrProperty);
        set => SetValue(MinStrProperty, value);
    }

    public static readonly StyledProperty<string> StDisProperty = AvaloniaProperty.Register<EQForm, string>(
        "StDis");

    public string StDis
    {
        get => GetValue(StDisProperty);
        set => SetValue(StDisProperty, value);
    }
    //gr
    public static readonly StyledProperty<string> ContentsProperty = AvaloniaProperty.Register<EQForm, string>(
        "Contents");

    public string Contents
    {
        get => GetValue(ContentsProperty);
        set => SetValue(ContentsProperty, value);
    }

    public static readonly StyledProperty<string> quantityProperty = AvaloniaProperty.Register<EQForm, string>(
        "quantity");

    public string quantity
    {
        get => GetValue(quantityProperty);
        set => SetValue(quantityProperty, value);
    }

    //veh
    
    public static readonly StyledProperty<string> CapacityProperty = AvaloniaProperty.Register<EQForm, string>(
        "Capacity");

    public string Capacity
    {
        get => GetValue(CapacityProperty);
        set => SetValue(CapacityProperty, value);
    }

    public static readonly StyledProperty<string> SpeedProperty = AvaloniaProperty.Register<EQForm, string>(
        "Speed");

    public string Speed
    {
        get => GetValue(SpeedProperty);
        set => SetValue(SpeedProperty, value);
    }
    //btm
    public static readonly StyledProperty<string> PriceProperty = AvaloniaProperty.Register<EQForm, string>(
        "Price");

    public string Price
    {
        get => GetValue(PriceProperty);
        set => SetValue(PriceProperty, value);
    }

    public static readonly StyledProperty<string> WeightProperty = AvaloniaProperty.Register<EQForm, string>(
        "Weight");

    public string Weight
    {
        get => GetValue(WeightProperty);
        set => SetValue(WeightProperty, value);
    }
}