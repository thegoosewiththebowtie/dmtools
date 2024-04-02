using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Config.Net;
using dmtools.Views;
using LiteDB;

namespace dmtools.PopUps;

public partial class AddNpc : Window
{
    public NPC SelNpc { get; set; }
    public ISettings settings { get; set; }
    public int profileid { get; set; } 
    public AddNpc()
    {
        InitializeComponent();
        Profile profile = new ConfigurationBuilder<Profile>().UseIniFile("Profile.ini").Build();
        profileid = profile.ProfileID;
        InitData();
    }

    public void InitData()
    {
        settings = new ConfigurationBuilder<ISettings>().UseIniFile("Settings/q0" + profileid +".ini").Build();
        using (var ldb = new LiteDatabase(settings.DataPath))
        {
            var npcs = ldb.GetCollection<NPC>();
            List<NPC> npclist =  new List<NPC>();
            foreach (var fffuckit in npcs.FindAll())
            {
                npclist.Add(fffuckit);
            }
            NpcBox.ItemsSource = npclist;
        }
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void Add_OnClick(object? sender, RoutedEventArgs e)
    {
        SelNpc = (NpcBox.SelectedItem as NPC);
        this.Close();
    }

    private void NpcBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (NpcBox.SelectedItem != null)
        {
            Add.IsEnabled = true;
        }
        else
        {
            Add.IsEnabled = false;
        }
    }
}