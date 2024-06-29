﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Config.Net;
using dmtools.PopUps;
using dmtools.PopUps.MapPU;
using dmtools.Views;
using DynamicData;
using Gdk;
using LiteDB;
using Brushes = Avalonia.Media.Brushes;
using Color = Avalonia.Media.Color;

namespace dmtools.Templates;

public class ItemCoordinates
{
    public int vertical { get; set; }
    public int horizontal { get; set; }
}
public partial class MapEditor : UserControl
{
    public static event EventHandler IveBeenDeletedAAAA;
    public IMap MapInfo { get; set; } 
    char[] alpha = " ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
    Profile profile = new ConfigurationBuilder<Profile>().UseIniFile("Profile.ini").Build();
    public int profid { get; set; }

    public ISettings settings { get; set; }
    public MapEditor(ConfigurationBuilder<IMap> thismap)
    {
        InitializeComponent();
        profid = profile.ProfileID;
        settings = new ConfigurationBuilder<ISettings>().UseIniFile("Settings/q0" + profid +".ini").Build();
        ini(thismap.Build());
        MapInfo = thismap.Build();
    }

    public void ini(IMap tm)
    {
        Map.Children.Clear();
        var wi = tm.width;
        var he = tm.height;
        GridLength acvi;
        GridLength achi;
        if (tm.height > tm.width)
        {
            achi = new GridLength(1, GridUnitType.Star);
            var f = (double)(tm.height/tm.width);
            acvi = new GridLength(f, GridUnitType.Star);
            Map.VerticalAlignment = VerticalAlignment.Top;
            Map.HorizontalAlignment = HorizontalAlignment.Left;
        }
        else if (tm.height < tm.width)
        {
            var f = (double)(tm.width / tm.height);
            achi = new GridLength(f, GridUnitType.Star);
            acvi = new GridLength(1, GridUnitType.Star);
            Map.VerticalAlignment = VerticalAlignment.Top;
            Map.HorizontalAlignment = HorizontalAlignment.Left;
        }
        else
        {
            acvi = new GridLength(1, GridUnitType.Star);
            achi = new GridLength(1, GridUnitType.Star);
        }
        ColumnDefinitions cds = new ColumnDefinitions();
        for (int w = 0; w < wi; w++)
        {
            ColumnDefinition cd = new ColumnDefinition()
            {
                Width = acvi
            };
            cds.Add(cd);
        }
        RowDefinitions rds = new RowDefinitions();
        for (int h = 0; h < he; h++)
        {
            RowDefinition rd = new RowDefinition()
            {
                Height = achi
            }; 
            rds.Add(rd);
        }
        Map.ColumnDefinitions = cds;
        Map.RowDefinitions = rds;
        var wic = 0;
        var hec = 0;
        var letr1 = 0;
        var letr2 = 1;
        for (int i = 0; i < wi*he; i++)
        {
            var e = $"{alpha[letr1].ToString()}{alpha[letr2].ToString()}-{(hec + 1).ToString()}";
            e = e.Trim();
            Border notbutton = new Border()
            {
                Width = 50,
                Height = 50,
                BorderThickness = new Thickness(1),
                BorderBrush = Brushes.DimGray,
                [Grid.RowProperty] = hec,
                [Grid.ColumnProperty] = wic,
                ZIndex = 10
            };
            Button button = new Button()
            {
                Name = e,
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0),
                Padding = new Thickness(0),
                Background = Brushes.Transparent,
            };
            button.PointerEntered += coordinates;
            button.Click += SelectPlace;
            notbutton.Child = button;
            if (letr2 >= 26)
            {
                letr1++;
                letr2 = 1;
            }
            else
            {
                letr2++;
            }
            if (wic == wi-1)
            {
                wic = 0;
                letr1 = 0;
                letr2 = 1;
                hec++;
            }
            else
            {
                wic++;
            }
            Map.Children.Add(notbutton);
        }

        if (!string.IsNullOrEmpty(tm.icons))
        {
            foreach (var ics in tm.icons.Split("$"))
            {
                if (string.IsNullOrEmpty(ics))
                {
                    break;
                }
                var d = Application.Current.FindResource(ics.Split("|")[0]) as StreamGeometry;
                var pi = new PathIcon()
                {
                    Data = d,
                    ZIndex = -1,
                    [Grid.RowProperty] = Convert.ToInt32(ics.Split("|")[2]),
                    [Grid.ColumnProperty] = Convert.ToInt32(ics.Split("|")[1])
                };
                Map.Children.Add(pi);
            }
        }
        if (!string.IsNullOrEmpty(tm.text))
        {
            foreach (var ics in tm.text.Split("$"))
            {
                if (string.IsNullOrEmpty(ics))
                {
                    break;
                }
                var d = ics.Split("|")[0];
                var pi = new TextBlock()
                {
                    FontSize = 25,
                    TextWrapping = TextWrapping.Wrap,
                    Text = d,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    ZIndex = -2,
                    [Grid.ColumnProperty] = Convert.ToInt32(ics.Split("|")[1]),
                    [Grid.RowProperty] = Convert.ToInt32(ics.Split("|")[2]),
                    [Grid.ColumnSpanProperty] = Convert.ToInt32(ics.Split("|")[3]),
                };
                Map.Children.Add(pi);
            }
        }
        if (!string.IsNullOrEmpty(tm.colors))
        {
            foreach (var ics in tm.colors.Split("$"))
            {
                if (string.IsNullOrEmpty(ics))
                {
                    break;
                }
                var d = ics.Split("|")[0];
                var f = new SolidColorBrush(Color.Parse(d));
                if (tm.images.Contains($"{ics.Split("|")[1]}|{Convert.ToInt32(ics.Split("|")[2])}"))
                {
                    f.Opacity = 50;
                }
                var pi = new Border()
                {
                    Background = f,
                    ZIndex = -3,
                    [Grid.ColumnProperty] = Convert.ToInt32(ics.Split("|")[1]),
                    [Grid.RowProperty] = Convert.ToInt32(ics.Split("|")[2]),
                    [Grid.ColumnSpanProperty] = Convert.ToInt32(ics.Split("|")[3]),
                    [Grid.RowSpanProperty] = Convert.ToInt32(ics.Split("|")[4]),
                };
                Map.Children.Add(pi);
            }
        }
        if (!string.IsNullOrEmpty(tm.images))
        {
            foreach (var ics in tm.images.Split("$"))
            {
                if (string.IsNullOrEmpty(ics))
                {
                    break;
                }
                var d = Application.Current.FindResource(ics.Split("|")[0]) as StreamGeometry;
                var pi = new PathIcon()
                {
                    Data = d,
                    ZIndex = -4,
                    [Grid.ColumnProperty] = Convert.ToInt32(ics.Split("|")[1]),
                    [Grid.RowProperty] = Convert.ToInt32(ics.Split("|")[2]),
                    [Grid.ColumnSpanProperty] = Convert.ToInt32(ics.Split("|")[3]),
                    [Grid.RowSpanProperty] = Convert.ToInt32(ics.Split("|")[4]),
                };
                Map.Children.Add(pi);
            }
        }
        if (!string.IsNullOrEmpty(tm.urhere))
        {
            string ress = "";
            string last = "";
            foreach (var ics in tm.urhere.Split("$"))
            {
                if (string.IsNullOrEmpty(ics))
                {
                    break;
                }
                if (!ress.Contains($"{ics.Split("|")[1]}|{ics.Split("|")[2]}"))
                {
                    ress += $"{ics.Split("|")[0]}>|{ics.Split("|")[1]}|{ics.Split("|")[2]}$";
                }
                else
                {
                    ress = ress.Replace(last, $"{last}/{ics.Split("|")[0]}>");
                }
                last = $"{ics.Split("|")[0]}>";
            }
            foreach (var ics in ress.Split("$"))
            {
                if (string.IsNullOrEmpty(ics))
                {
                    break;
                }
                var pi = new Border()
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    ZIndex = 1,
                    Width = 40,
                    Height = 40,
                    [Grid.ColumnProperty] = Convert.ToInt32(ics.Split("|")[1]),
                    [Grid.RowProperty] = Convert.ToInt32(ics.Split("|")[2]),
                    Background = Brushes.LightGreen,
                    CornerRadius = new CornerRadius(5),
                    BorderThickness = new Thickness(1),
                    BorderBrush = Brushes.Black,
                };
                if (ics.Split("|")[0].Split("/").Length == 1)
                {
                    TextBlock tb = new TextBlock()
                    {
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Text = $"{ics.Split("|")[0].Replace(">","")}",
                        FontSize = 25,
                        Margin = new Thickness(1),
                        Padding = new Thickness(0),
                        TextWrapping = TextWrapping.Wrap,
                    };
                    pi.Child = tb;
                }
                else if (ress.Split("$").Length - 1 == 1)
                {
                    TextBlock tb = new TextBlock()
                    {
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        FontSize = 20,
                        Text = $"All",
                        Margin = new Thickness(1),
                        Padding = new Thickness(0),
                        TextWrapping = TextWrapping.Wrap,
                    };
                    pi.Child = tb;
                }
                else
                {
                    WrapPanel wrapPanel = new WrapPanel()
                    {
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                    };
                    pi.Child = wrapPanel;
                    foreach (var ltrs in ics.Split("|")[0].Split("/"))
                    {
                        if (string.IsNullOrEmpty(ltrs))
                        {
                            break;
                        }
                        TextBlock tb = new TextBlock()
                        {
                            VerticalAlignment = VerticalAlignment.Center,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            Text = $"{ltrs.Replace(">","")}",
                            Margin = new Thickness(0),
                            Padding = new Thickness(1),
                            TextWrapping = TextWrapping.Wrap,
                        };
                        wrapPanel.Children.Add(tb);
                    }
                }
                
                Map.Children.Add(pi);
            }
        }
    }
    private void coordinates(object? sender, PointerEventArgs e)
    {
        crdns.Text = (sender as Button).Name;
    }
    private void SelectPlace(object? sender, RoutedEventArgs e)
    {
        crd.Text = (sender as Button).Name;
    }
    private async void Ico_OnClick(object? sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(crd.Text))
        {
            return;
        }
        ClrType(2);
        var selcor = GetCoordinates();
        if (Avalonia.Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            IconSelector iconSelector = new IconSelector();
            await iconSelector.ShowDialog(desktop.MainWindow);
            MapInfo.icons += $"{iconSelector.sel}|{selcor.vertical}|{selcor.horizontal}$";
        }
        ini(MapInfo);
    }
    private async void Col_OnClick(object? sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(crd.Text))
        {
            return;
        }
        var selcor = GetCoordinates();
        ItemCoordinates max = new ItemCoordinates()
        {
            horizontal = MapInfo.width,
            vertical = MapInfo.height
        };
        if (Avalonia.Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            ColorSelector colorSelector = new ColorSelector(max, selcor);
            await colorSelector.ShowDialog(desktop.MainWindow);
            if (string.IsNullOrWhiteSpace(colorSelector.coloret))
            {
                return;
            }
            MapInfo.colors +=
                $"{colorSelector.coloret}|{selcor.vertical}|{selcor.horizontal}|{colorSelector.width0}|{colorSelector.height0}$";
        }
        ini(MapInfo);
    }
    private async void Txt_OnClick(object? sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(crd.Text))
        {
            return;
        }
        var selcor = GetCoordinates();
        ItemCoordinates max = new ItemCoordinates()
        {
            horizontal = MapInfo.width,
            vertical = MapInfo.height
        };
        if (Avalonia.Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            TextSelector colorSelector = new TextSelector(max, selcor);
            await colorSelector.ShowDialog(desktop.MainWindow);
            if (string.IsNullOrWhiteSpace(colorSelector.coloret))
            {
                return;
            }
            MapInfo.text +=
                $"{colorSelector.coloret}|{selcor.vertical}|{selcor.horizontal}|{colorSelector.width0}$";
        }
        ini(MapInfo);
    }
    private void Pth_OnClick(object? sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(crd.Text))
        {
            return;
        }
        var selcor = GetCoordinates();
    }
    private void Loc_OnClick(object? sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(crd.Text))
        {
            return;
        }
        var selcor = GetCoordinates();
        var MI = MapInfo.urhere.Split("$").ToList();
        MapInfo.urhere = "";
        foreach (var player in MI)
        {
            if (string.IsNullOrEmpty(player))
            {
                break;
            }
            MapInfo.urhere += $"{player.Split("|")[0]}|{selcor.vertical}|{selcor.horizontal}|{player.Split("|")[3]}$";
        }
        ini(MapInfo);
    }
    private void Del_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
                File.Delete($"Maps/{MapInfo.name}");
        }
        catch (Exception exception)
        {
                NO no = new NO(exception.Message);
                no.Show();
        }
        IveBeenDeletedAAAA(sender, e);
    }
    public void ClrType(int id)
    {
        if (string.IsNullOrWhiteSpace(crd.Text))
        {
            return;
        }
        var selcor = GetCoordinates();
        switch (id)
        {
            case 1:
                var images = MapInfo.images.Split("$");
                foreach (var a in images)
                {
                    if (string.IsNullOrEmpty(a))
                    {
                        break;
                    }
                    if (a.Contains($"{selcor.vertical}|{selcor.horizontal}"))
                    {
                        MapInfo.images = MapInfo.images.Replace($"{a}$", "");
                    }
                }
                break;
            case 2:
                var icons = MapInfo.icons.Split("$");
                foreach (var a in icons )
                {
                    if (string.IsNullOrEmpty(a))
                    {
                        break;
                    }
                    if (a.Contains($"{selcor.vertical}|{selcor.horizontal}"))
                    {
                        MapInfo.icons = MapInfo.icons.Replace($"{a}$", "");
                    }
                }
                break;
            case 3:
                var colors = MapInfo.colors.Split("$");
                Array.Reverse(colors);
                foreach (var a in colors)
                {
                    if (!string.IsNullOrEmpty(a))
                    {
                        if (a.Contains($"{selcor.vertical}|{selcor.horizontal}"))
                        {
                            MapInfo.colors = MapInfo.colors.Replace($"{a}$", "");
                            break;
                        }
                    }
                }
                break;
            case 4:
                var text = MapInfo.text.Split("$");
                foreach (var a in text)
                {
                    if (string.IsNullOrEmpty(a))
                    {
                        break;
                    }
                    if (a.Contains($"{selcor.vertical}|{selcor.horizontal}"))
                    {
                        MapInfo.text = MapInfo.text.Replace($"{a}$", "");
                    }
                }
                break;
            case 5:
                var roads = MapInfo.roads.Split("$");
                foreach (var a in roads)
                {
                    if (string.IsNullOrEmpty(a))
                    {
                        break;
                    }
                    if (a.Contains($"{selcor.vertical}|{selcor.horizontal}"))
                    {
                        MapInfo.roads = MapInfo.roads.Replace($"{a}$", "");
                    }
                }
                break;

        }
        ini(MapInfo);
    }
    private void Clr_OnClick(object? sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(crd.Text))
        {
            return;
        }
        var selcor = GetCoordinates();

        var icons = MapInfo.icons.Split("$");
        foreach (var a in icons )
        {
            if (string.IsNullOrEmpty(a))
            {
                break;
            }
            if (a.Contains($"{selcor.vertical}|{selcor.horizontal}"))
            {
                MapInfo.icons = MapInfo.icons.Replace($"{a}$", "");
            }
        }

        var images = MapInfo.images.Split("$");
        foreach (var a in images)
        {
            if (string.IsNullOrEmpty(a))
            {
                break;
            }
            if (a.Contains($"{selcor.vertical}|{selcor.horizontal}"))
            {
                MapInfo.images = MapInfo.images.Replace($"{a}$", "");
            }
        }

        var colors = MapInfo.colors.Split("$");
        foreach (var a in colors)
        {
            if (string.IsNullOrEmpty(a))
            {
                break;
            }
            if (a.Contains($"{selcor.vertical}|{selcor.horizontal}"))
            {
                MapInfo.colors = MapInfo.colors.Replace($"{a}$", "");
            }
        }

        var text = MapInfo.text.Split("$");
        foreach (var a in text)
        {
            if (string.IsNullOrEmpty(a))
            {
                break;
            }
            if (a.Contains($"{selcor.vertical}|{selcor.horizontal}"))
            {
                MapInfo.text = MapInfo.text.Replace($"{a}$", "");
            }
        }

        var roads = MapInfo.roads.Split("$");
        foreach (var a in roads)
        {
            if (string.IsNullOrEmpty(a))
            {
                break;
            }
            if (a.Contains($"{selcor.vertical}|{selcor.horizontal}"))
            {
                MapInfo.roads = MapInfo.roads.Replace($"{a}$", "");
            }
        }
        ini(MapInfo);
    }
    public ItemCoordinates GetCoordinates()
    {
        var prev = crd.Text.Split("-")[0].ToCharArray();
        Array.Reverse(prev);
        var h = crd.Text.Split("-")[1];
        var s2 = 0;
        var s1 = alpha.IndexOf(prev[0])-1;
        if (prev.Count() > 1)
        {
            s2 = alpha.IndexOf(prev[1]) * 26;
        }
        var v = s2 + s1;
        var ret = new ItemCoordinates()
        {
            horizontal = Convert.ToInt32(h)-1,
            vertical = Convert.ToInt32(v)
        };
        return ret;
    }
    private void clrsel_OnClick(object? sender, RoutedEventArgs e)
    {
        ClrType(Convert.ToInt32((sender as Button).Name.Replace("id", "")));
    }
    private void FlyoutBase_OnOpened(object? sender, EventArgs e)
    {
        btnpnl.Children.Clear();
        using (LiteDatabase ldb = new LiteDatabase(settings.DataPath))
        {
            var pls = ldb.GetCollection<PlayerCharacter>();
            foreach (var pc in pls.FindAll())
            {
                Button btn = new Button()
                {
                    Content = pc.FirstName,
                    Name = pc.FirstName,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center
                };
                btn.Click += BtnOnClick;
                btnpnl.Children.Add(btn);
            }
        }
    }
    private void BtnOnClick(object? sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(crd.Text))
        {
            return;
        }
        var selcor = GetCoordinates();
        List<string> bas = MapInfo.urhere.Split("$").ToList();
        if (MapInfo.urhere.Contains(((sender as Button).Name)))
        {
            foreach (var pc in bas)
            {
                if (string.IsNullOrEmpty(pc))
                {
                    break;
                }
                if (pc.Contains((sender as Button).Name.Remove(1).ToUpper()))
                {
                    if (pc.Contains((sender as Button).Name) && pc.Split("|")[0].Length > 1)
                    {
                        MapInfo.urhere = MapInfo.urhere.Replace( $"{pc}$", "");
                        MapInfo.urhere += $"{(sender as Button).Name.Remove(1).ToUpper()}{(sender as Button).Name.Remove(2).Remove(0,1).ToLower()}|{selcor.vertical}|{selcor.horizontal}|{(sender as Button).Name}$";
                    }
                    else if (pc.Contains((sender as Button).Name) && pc.Split("|")[0].Length == 1)
                    {
                        MapInfo.urhere = MapInfo.urhere.Replace( $"{pc}$", "");
                        MapInfo.urhere += $"{(sender as Button).Name.Remove(1).ToUpper()}|{selcor.vertical}|{selcor.horizontal}|{(sender as Button).Name}$";
                    }
                }
            }
        }
        else
        {
            if (MapInfo.urhere.Contains($"{(sender as Button).Name.Remove(1).ToUpper()}|"))
            {
                MapInfo.urhere += $"{(sender as Button).Name.Remove(1).ToUpper()}{(sender as Button).Name.Remove(2).Remove(0,1).ToLower()}|{selcor.vertical}|{selcor.horizontal}|{(sender as Button).Name}$";
            }
            else
            {
                MapInfo.urhere += $"{(sender as Button).Name.Remove(1).ToUpper()}|{selcor.vertical}|{selcor.horizontal}|{(sender as Button).Name}$";
            }
        }
        
        ini(MapInfo);
    }
}