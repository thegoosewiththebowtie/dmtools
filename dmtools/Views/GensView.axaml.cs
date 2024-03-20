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
    public string str { get; set; }
    public string dex { get; set; }
    public string con { get; set; }
    public string inte { get; set; }
    public string wis { get; set; }
    public string cha { get; set; }
    public List<string> DisList { get; set; }
    public List<string>LikesList { get; set; }
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
        CurrentNpc.des = "";
        string MinMaxAge = "a0a1001";
        if (Age.SelectedItem != null)
        {
            MinMaxAge = (Age.SelectedItem as TextBlock).Name;
        }
        var res = Generators.NPC.CreateNPC(MinMaxAge);
        HistNpcs.Insert(0,new GenNpc()
        {
            Name = res[0][0], Name2 = res[0][1], Race = res[1][0], Age = res[1][1], Hair = res[1][2], Height = res[1][3],
            BodyType = res[1][4], EyeColor= res[1][5], str = res[1][6], dex = res[1][7], con = res[1][8], inte = res[1][9], wis = res[1][10], cha = res[1][11], 
            DisList = res[2], LikesList = res[3]
        });
        History.ItemsSource = HistNpcs;
    }

    private void History_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if ((sender as DataGrid).SelectedItem == null)
        {
            CurrentNpc.des = "";
            CurrentNpc.str = "";
            CurrentNpc.dex = "";
            CurrentNpc.con = "";
            CurrentNpc.inte = "";
            CurrentNpc.wis = "";
            CurrentNpc.cha = "";
            CurrentNpc.dislikes = new List<string>();
            CurrentNpc.likes = new List<string>();
            return;
        }
        CurrentNpc.des =
            $"Age: {(History.SelectedItem as GenNpc).Age}\r\n" +
            $"Hair: {(History.SelectedItem as GenNpc).Hair}\r\nHeight: {(History.SelectedItem as GenNpc).Height}\r\n" +
            $"BodyType: {(History.SelectedItem as GenNpc).BodyType}\r\nEyeColor: {(History.SelectedItem as GenNpc).EyeColor}\r\n";
        CurrentNpc.str = (History.SelectedItem as GenNpc).str;
        CurrentNpc.dex = (History.SelectedItem as GenNpc).dex;
        CurrentNpc.con = (History.SelectedItem as GenNpc).con;
        CurrentNpc.inte = (History.SelectedItem as GenNpc).inte;
        CurrentNpc.wis = (History.SelectedItem as GenNpc).wis;
        CurrentNpc.cha = (History.SelectedItem as GenNpc).cha;
        CurrentNpc.dislikes = (History.SelectedItem as GenNpc).DisList;
        CurrentNpc.likes = (History.SelectedItem as GenNpc).LikesList;
    }
}