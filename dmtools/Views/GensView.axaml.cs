using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using dmtools.Generators;
using Avalonia.Markup.Xaml;
using Config.Net;
using dmtools.PopUps;
using LiteDB;
using Microsoft.VisualBasic;
using OpenGL;

namespace dmtools.Views;

public class GenNpc
{
    public int ID { get; set; }
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

public class LootParameters
{
    public int type0 { get; set; }
    public string type { get; set; }
}
public partial class GensView : UserControl
{
    public List<GenNpc> HistNpcs = new List<GenNpc>();
    Profile profile = new ConfigurationBuilder<Profile>().UseIniFile("Profile.ini").Build();
    public int profid { get; set; }
    private ISettings settings { get; set; }
    public GensView()
    {
        InitializeComponent();
        profid = profile.ProfileID;
        settings = new ConfigurationBuilder<ISettings>().UseIniFile("Settings/q0" + profid +".ini").Build();

        using (LiteDatabase ldb = new LiteDatabase(settings.DataPath))
        {
            var ldbhistnp = ldb.GetCollection<GenNpc>();
            HistNpcs = ldbhistnp.FindAll().ToList();
            HistNpcs.Reverse();
        }

        History.ItemsSource = HistNpcs;
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
        NpcParams npcParams = new NpcParams();
        npcParams.MinMaxAge = MinMaxAge;
        npcParams.Race = 0;
        if (Racech.SelectedItem != null)
        {
            npcParams.Race = Racech.SelectedIndex;
        }
        var res = Generators.NPC.CreateNPC(npcParams);
        var genned = new GenNpc()
        {
            Name = res[0][0], Name2 = res[0][1], Race = res[1][0], Age = res[1][1], Hair = res[1][2],
            Height = res[1][3],
            BodyType = res[1][4], EyeColor = res[1][5], str = res[1][6], dex = res[1][7], con = res[1][8],
            inte = res[1][9], wis = res[1][10], cha = res[1][11],
            DisList = res[2], LikesList = res[3]
        };
        HistNpcs.Insert(0,genned);
        if (HistNpcs.Count() > 50)
        {
            var c = HistNpcs.Count();
           HistNpcs.RemoveRange(50, c-50);
        }
        using (LiteDatabase ldb = new LiteDatabase(settings.DataPath))
        {
            var ldbhistnp = ldb.GetCollection<GenNpc>();
            ldbhistnp.Insert(genned);
            if (ldbhistnp.Count() > 50)
            {
                var c2 = ldbhistnp.Count();
                for (int i = 51; i <= c2; i++)
                {
                    ldbhistnp.Delete(51);
                }
            }
        }
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

    private void SaveNPC_OnClick(object? sender, RoutedEventArgs e)
    {
        if (History.SelectedItem == null)
        {
            return;
        }
        using (var LdbNPC = new LiteDatabase(settings.DataPath))
        {
            var npc = LdbNPC.GetCollection<NPC>();
            npc.Insert(new NPC
            {
                Level = "1", ArmorClass = "10", Health = "5", Strength = CurrentNpc.str, Dexterity = CurrentNpc.dex,
                Constitution = CurrentNpc.con, Experience = "0", Wisdom = CurrentNpc.wis, Intelligence = CurrentNpc.inte, Charisma = CurrentNpc.cha, IsCCharisma = false,
                IsCConstitution = false, IsCDexterity = false, IsCStrength = false, IsCIntelligence = false, IsCWisdom = false,
                Likes = String.Join("/", CurrentNpc.likes), Dislikes = string.Join("/", CurrentNpc.dislikes), Notes = CurrentNpc.des,
                FirstName = (History.SelectedItem as GenNpc).Name, OtherName = (History.SelectedItem as GenNpc).Name2,
                Race = (History.SelectedItem as GenNpc).Race,
            });
        }
    }

    private void LootHis_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        Debug.WriteLine("Nah, it doesnt work yet");
    }

    private void GenL_OnClick(object? sender, RoutedEventArgs e)
    {
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        if (NpcType.SelectedItem == null)
        {
            return;
        }
        var lp = new LootParameters ()
        {
            type0 = 1, type = (NpcType.SelectedItem as ComboBoxItem).Name
        };
        var res = dmtools.Generators.Loot.GenerateLoot(lp);
        Gendloot.ItemsSource = null;
        Gendloot.ItemsSource = res;
    }

    private void ShopGen_OnClick(object? sender, RoutedEventArgs e)
    {
        NO no = new NO("It's not implemented yet cause im gonna transfer everything from this pdf (it should've been opened)");
        no.Show();
        Process.Start("explorer.exe", $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/GlossData/dndsh.pdf");
    }

    private void BossGen_OnClick(object? sender, RoutedEventArgs e)
    {
        if (BossType.SelectedItem == null)
        {
            return;
        }
        var lp = new LootParameters ()
        {
            type0 = 2, type = (BossType.SelectedItem as ComboBoxItem).Name
        };
        var res = dmtools.Generators.Loot.GenerateLoot(lp);
        Gendbossloot.ItemsSource = null;
        Gendbossloot.ItemsSource = res;
    }

    private void EnvGen_OnClick(object? sender, RoutedEventArgs e)
    {
        if (EnvType.SelectedItem == null)
        {
            return;
        }
        var lp = new LootParameters ()
        {
            type0 = 4, type = (EnvType.SelectedItem as ComboBoxItem).Name
        };
        var res = dmtools.Generators.Loot.GenerateLoot(lp);
        EnvRes.ItemsSource = null;
        EnvRes.ItemsSource = res;    }
}