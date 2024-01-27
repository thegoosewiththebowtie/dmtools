using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using dmtools.Generators;
using Avalonia.Markup.Xaml;

namespace dmtools.Views;

public class GenNpc
{
    public string Name { get; set; }
    public string Name2 { get; set; }
    public string Race { get; set; }
    public string Age { get; set; }
    public string Hair { get; set; }
    public string Height { get; set; }
    public string BodyType { get; set; }
    public string EyeColor { get; set; }
    public List<string> stats { get; set; }
    
}
public partial class GensView : UserControl
{
    public List<GenNpc> HistNpcs = new List<GenNpc>();
    public GensView()
    {
        InitializeComponent();
    }

    private void GenerateNPC_OnClick(object? sender, RoutedEventArgs e)
    {
        History.SelectedItem = null;
        History.ItemsSource = null;
        CurrentNpc.Text = "";
        string MinMaxAge = "a0a1001";
        if (Age.SelectedItem != null)
        {
            MinMaxAge = (Age.SelectedItem as TextBlock).Name;
        }
        List<string> res = Generators.NPC.CreateNPC(MinMaxAge);
        HistNpcs.Insert(0,new GenNpc()
        {
            Name = res[0], Name2 = res[1], Race = res[2], Age = res[3], Hair = res[4], Height = res[5],
            BodyType = res[6], EyeColor= res[7], stats = res.GetRange(8, 6)
        });
        History.ItemsSource = HistNpcs;
    }

    private void History_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if ((sender as DataGrid).SelectedItem == null)
        {
            return;
        }
        CurrentNpc.Text =
            $"Age: {(History.SelectedItem as GenNpc).Age}\r\n" +
            $"Hair: {(History.SelectedItem as GenNpc).Hair}\r\nHeight: {(History.SelectedItem as GenNpc).Height}\r\n" +
            $"BodyType: {(History.SelectedItem as GenNpc).BodyType}\r\nEyeColor: {(History.SelectedItem as GenNpc).EyeColor}\r\n" +
            $"Stats:\r\n{string.Join("\r\n", (History.SelectedItem as GenNpc).stats)}";
    }
}