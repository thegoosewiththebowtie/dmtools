using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Config.Net;
using dmtools.Views;
using LiteDB;


namespace dmtools.PopUps;

public partial class Sure : Window
{
    public static event EventHandler Delete;
    public int id;
    public int tyid;
    public Sure(int ID, int TyID)
    {
        InitializeComponent();
        id = ID;
        tyid = TyID;
    }

    private void Button_Delete(object? sender, RoutedEventArgs e)
    {
        Profile profile = new ConfigurationBuilder<Profile>().UseIniFile("Profile.ini").Build();
        ISettings settings = new ConfigurationBuilder<ISettings>().UseIniFile("Settings/q0" + profile.ProfileID +".ini").Build();
        using (var ldbpc = new LiteDatabase(settings.DataPath))
        {
            if (tyid == 0)
            {
                var chars = ldbpc.GetCollection<PlayerCharacter>();
                chars.Delete(id);
                int i = 1;
                foreach (var pc in chars.FindAll())
                {
                    pc.ID = i;
                    i++;
                }
            }
            else if (tyid == 99)
            {
                var chars = ldbpc.GetCollection<NPC>();
                chars.Delete(id);
                int i = 1;
                foreach (var pc in chars.FindAll())
                {
                    pc.ID = i;
                    i++;
                }
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