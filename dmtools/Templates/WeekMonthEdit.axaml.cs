using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace dmtools.Templates;

public class WeekMonthEdit : TemplatedControl
{
    public static readonly StyledProperty<string> MainWatermarkProperty = AvaloniaProperty.Register<WeekMonthEdit, string>(
        "MainWatermark", defaultValue:"MonthName");

    public string MainWatermark
    {
        get => GetValue(MainWatermarkProperty);
        set => SetValue(MainWatermarkProperty, value);
    }

    public static readonly StyledProperty<string> MonthNameProperty = AvaloniaProperty.Register<WeekMonthEdit, string>(
        "MonthName", defaultValue:"");

    public string MonthName
    {
        get => GetValue(MonthNameProperty);
        set => SetValue(MonthNameProperty, value);
    }

    public static readonly StyledProperty<decimal> DaysProperty = AvaloniaProperty.Register<WeekMonthEdit, decimal>(
        "Days", defaultValue: 30);

    public decimal Days
    {
        get => GetValue(DaysProperty);
        set => SetValue(DaysProperty, value);
    }
}