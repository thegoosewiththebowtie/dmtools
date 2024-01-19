using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Config.Net;
using dmtools.Views;
using LiteDB;

namespace dmtools.Resources;

public partial class Sure : Window
{
    public static event EventHandler Delete;
    public int id;
    public Sure(int ID)
    {
        InitializeComponent();
        id = ID;
    }
    ISettings settings = new ConfigurationBuilder<ISettings>().UseIniFile("Settings.ini").Build();

    private void Button_Delete(object? sender, RoutedEventArgs e)
    {
        using (var ldbpc = new LiteDatabase(settings.Profile))
        {
            var chars = ldbpc.GetCollection<PlayerCharacter>();
            chars.Delete(id);
            foreach (var pc in chars.FindAll())
            {
                int i = 1;
                pc.ID = i;
                i++;
            }
        }
        EventHandler del = Delete;
        del(sender, e);
        this.Close();
    }

    private void ButtonCancel_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }
}