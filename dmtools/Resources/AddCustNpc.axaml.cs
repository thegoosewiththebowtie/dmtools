using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace dmtools.Resources;

public class Res
{
    public int initiative { get; set; }
    public string name { get; set; }
    public string playe { get; set; }
    public int ac { get; set; }
    public int hp { get; set; }
}

public partial class AddCustNpc : Window
{
    public Res res { get; set; }
    public AddCustNpc()
    {
        InitializeComponent();
    }
    
    private void TestInt(object? sender, KeyEventArgs e)
    {
        if (!Int32.TryParse(e.KeySymbol, out int i))
        {
            e.Handled = true;
        }
    }

    private void OK(object? sender, RoutedEventArgs e)
    {
        int ien = 0;
        if (Initiative.Text != null)
        {
            ien = Convert.ToInt32(Initiative.Text);
        }
        else
        {
            ien = new Random().Next(1, 20);
        }
        res = new Res()
        {
            initiative = ien,
            name = Name.Text,
            playe = NPC.Text,
            ac = Convert.ToInt32(AC.Text),
            hp = Convert.ToInt32(HP.Text)
        };
        this.Close();
    }

    private void Cancel(object? sender, RoutedEventArgs e)
    {
        res = new Res()
        {
            initiative = -1,
        };
        this.Close();
    }
}