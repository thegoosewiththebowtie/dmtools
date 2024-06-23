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
using dmtools.Templates;
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
        ini();
    }

    public void ini()
    {
        profid = profile.ProfileID;
        settings = new ConfigurationBuilder<ISettings>().UseIniFile("Settings/q0" + profid +".ini").Build();
        using (LiteDatabase ldb = new LiteDatabase(settings.DataPath))
        {
            var ldbhistnp = ldb.GetCollection<GenNpc>();
            HistNpcs = ldbhistnp.FindAll().ToList();
            HistNpcs.Reverse();
            var ldbchars = ldb.GetCollection<PlayerCharacter>();
            foreach (var pc in ldbchars.FindAll())
            {
                EncChForm charForm = new EncChForm();
                charForm.ID = pc.ID;
                charForm.FirstName = pc.FirstName;
                charForm.OtherNames = pc.OtherName;
                charForm.Player = pc.Player;
                charForm.Class = pc.Class;
                charForm.Race = pc.Race;
                charForm.Alligment = pc.Alligment;
                charForm.Notes = pc.Notes;
                charForm.Backgroundd = pc.Backgroundd;
                charForm.StrVal = pc.Strength;
                charForm.DexVal = pc.Dexterity;
                charForm.ConVal = pc.Constitution;
                charForm.IntVal = pc.Intelligence;
                charForm.WisVal = pc.Wisdom;
                charForm.ChaVal = pc.Charisma;
                charForm.Level = pc.Level;
                charForm.Exp = pc.Experience;
                charForm.Health = pc.Health;
                charForm.Ac = pc.ArmorClass;
                charForm.IsCheck = new List<bool>()
                {
                    pc.IsCCharisma, pc.IsCStrength, pc.IsCDexterity, pc.IsCConstitution, pc.IsCIntelligence, pc.IsCWisdom,
                };
                PCCarousel.Items.Add(charForm);
            }
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

    private void PrevPC_OnClick(object? sender, RoutedEventArgs e)
    {
        PCCarousel.Previous();
    }

    private void NxtPC_OnClick(object? sender, RoutedEventArgs e)
    {
        PCCarousel.Next();
    }

    private void PrevEn_OnClick(object? sender, RoutedEventArgs e)
    {
        EnemyCarousel.Previous();
    }

    private void NxtEn_OnClick(object? sender, RoutedEventArgs e)
    {
        EnemyCarousel.Next();
    }

    private void GenEnc_OnClick(object? sender, RoutedEventArgs e)
    {
        List<Bestiary> bc;
        switch (enctype.SelectedIndex)
        {
            case 0:
                return;
            case 1:
                bc = Generators.Encounter.GenerateEncounter(com.LowerSelectedValue, com.UpperSelectedValue, (int)ams.Value);
                break;
            default:
                return;
        }
        EnemyCarousel.Items.Clear();
        foreach (var srs in bc)
        {
            EncForm ThisBest = new EncForm();
            ThisBest.BName = srs.name;
            ThisBest.Aligment = $"{Application.Current.FindResource("Aligment")}: {srs.alignment}";
            ThisBest.Languages = $"{Application.Current.FindResource("Languages")}: {srs.languages}";
            ThisBest.Type = $"{Application.Current.FindResource("Type")}: {srs.type}";
            ThisBest.Subtype = $"{Application.Current.FindResource("Subtype")}: {srs.subtype}";
            ThisBest.Size = $"{Application.Current.FindResource("Size")}: {srs.size}";
            ThisBest.Speed = srs.speed_walk;
        ThisBest.Swim = srs.speed_swim;
        ThisBest.Fly = srs.speed_fly;
        ThisBest.Burrow = srs.speed_burrow;
        ThisBest.Climb = srs.speed_climb;
        ThisBest.Immunities = $"{Application.Current.FindResource("Damageimmunities")}: {srs.damage_immunities}\r\n\r\n" +
                              $"{Application.Current.FindResource("Damageresistances")}: {srs.damage_resistances}\r\n\r\n" +
                              $"{Application.Current.FindResource("Conditions")}: {srs.condition_immunities}";
        if (srs.damage_vulnerabilities != null)
        {
            ThisBest.Vul = $"{srs.damage_vulnerabilities}";
        }
        else
        {
            ThisBest.Vul = $"{Application.Current.FindResource("None")}";
        }
        ThisBest.Senses = $"{Application.Current.FindResource("Darkvision")}: {srs.darkvision}\r\n\r\n" +
                          $"{Application.Current.FindResource("Passiveperception")}: {srs.passive_perception}\r\n\r\n" +
                          $"{Application.Current.FindResource("Blindsight")}: {srs.blindsight}\r\n\r\n" +
                          $"{Application.Current.FindResource("Truesight")}: {srs.truesight}\r\n\r\n" +
                          $"{Application.Current.FindResource("Tremorsense")}: {srs.tremorsense}";
        ThisBest.Armor = $"{srs.ac_armor}\r\n" +
                         $"{srs.ac_condition}\r\n" +
                         $"{srs.ac_desc}\r\n" +
                         $"{srs.ac_spell}\r\n" +
                         $"{srs.ac_type}\r\n" +
                         $"{srs.ac_type}";
        ThisBest.MainACval = srs.ac_value;
        ThisBest.ACType = srs.ac_type;
        ThisBest.Health = srs.hit_points.ToString();
        ThisBest.HitPointsRoll = srs.hit_points_roll;
        ThisBest.Exp = srs.xp.ToString();
        ThisBest.StrVal = srs.strength.ToString();
        ThisBest.DexVal = srs.dexterity.ToString();
        ThisBest.ConVal = srs.constitution.ToString();
        ThisBest.IntVal = srs.intelligence.ToString();
        ThisBest.WisVal = srs.wisdom.ToString();
        ThisBest.ChaVal = srs.charisma.ToString();
        string attacks = String.Empty;
        if (srs.a_names != null)
        {
            var anames = srs.a_names.Split("$");
            var adescs = srs.a_desc.Split("$");
            for (int i = 0; i < anames.Length; i++)
            {
                attacks += $"{anames[i]}\r\n{adescs[i]}\r\n\r\n";
            }
        }
        else
        {
            attacks = $"{Application.Current.FindResource("None")}";
        }
        if (srs.la_name != null)
        {
            var lanames = srs.la_name.Split("$");
            var ladescs = srs.la_desc.Split("$");
            attacks += $"{Application.Current.FindResource("Legendaryattacks")}: \r\n";
            for (int i = 0; i < lanames.Length; i++)
            {
                attacks += $"{lanames[i]}\r\n{ladescs[i]}\r\n\r\n";
            }
        }
        ThisBest.Attacks = attacks;
        string abilities = String.Empty;
        if (srs.sa_name != null)
        {
            var sanames = srs.sa_name.Split("$");
            var sadesc = srs.sa_desc.Split("$");
            for (int i = 0; i < sanames.Length; i++)
            {
                abilities += $"{sanames[i]}\r\n{sadesc[i]}\r\n\r\n";
            }
            ThisBest.Abilities = abilities;
        }
        else
        {
            ThisBest.Abilities = $"{Application.Current.FindResource("None")}";;
        }
        if (srs.desc != null)
        {
            ThisBest.Desc = srs.desc;
        }
        else
        {
            ThisBest.Desc = $"{Application.Current.FindResource("None")}";;
        }

        EnemyCarousel.Items.Add(ThisBest);
        }
    }
}