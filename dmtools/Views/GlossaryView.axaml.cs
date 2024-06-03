using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml.Styling;
using AVFoundation;
using dmtools.PopUps;
using DynamicData;
using LiteDB;
using ReactiveUI;
using Avalonia.Data;
using System.Windows;
using Avalonia;
using Avalonia.Controls.Primitives;
using Config.Net;

namespace dmtools.Views;
public class Spells
{
    public int ID { get; set; }
    public string name { get; set; }
    public string desc { get; set; }
    public string higher_level { get; set; }
    public string range { get; set; }
    public string components { get; set; }
    public string material { get; set; }
    public bool ritual { get; set; }
    public string duration { get; set; }
    public bool concentration { get; set; }
    public string casting_time { get; set; }
    public int level { get; set; }
    public string attack_type { get; set; }
    public string DamageType { get; set; }
    public string SL1 { get; set; }
    public string SL2 { get; set; }
    public string SL3 { get; set; }
    public string SL4 { get; set; }
    public string SL5 { get; set; }
    public string SL6 { get; set; }
    public string SL7 { get; set; }
    public string SL8 { get; set; }
    public string SL9 { get; set; }
    public string CL1 { get; set; }
    public string CL5 { get; set; }
    public string CL11 { get; set; }
    public string CL17 { get; set; }
    public string school { get; set; }
    public string classes { get; set; }
    public string subclasses { get; set; }
    public string dctype { get; set; }
    public string dcsuccess { get; set; }
    public string dcdesc { get; set; }
    public string HASL1 { get; set; }
    public string HASL2 { get; set; }
    public string HASL3 { get; set; }
    public string HASL4 { get; set; }
    public string HASL5 { get; set; }
    public string HASL6 { get; set; }
    public string HASL7 { get; set; }
    public string HASL8 { get; set; }
    public string HASL9 { get; set; }
    public string AreaType { get; set; }
    public int AreaSize { get; set; }
}
public class Bestiary
{
    public int ID { get; set; }
    public string name { get; set; }
    public string size { get; set; }
    public string type { get; set; }
    public string alignment { get; set; }
    public string ac_type { get; set; }
    public string ac_value { get; set; }
    public string ac_condition { get; set; }
    public string ac_spell { get; set; }
    public string ac_armor { get; set; }
    public string ac_desc { get; set; }
    public int hit_points { get; set; }
    public string hit_dice { get; set; }
    public string hit_points_roll { get; set; }
    public string speed_walk { get; set; }
    public string speed_swim { get; set; }
    public string speed_fly { get; set; }
    public string speed_burrow { get; set; }
    public string speed_climb { get; set; }
    public bool? speed_hover { get; set; }
    public int strength { get; set; }
    public int dexterity { get; set; }
    public int constitution { get; set; }
    public int intelligence { get; set; }
    public int wisdom { get; set; }
    public int charisma { get; set; }
    public string prof_name { get; set; }
    public string prof_val { get; set; }
    public string damage_vulnerabilities { get; set; }
    public string damage_resistances { get; set; }
    public string damage_immunities { get; set; }
    public string condition_immunities { get; set; }
    public string darkvision { get; set; }
    public int passive_perception { get; set; }
    public string blindsight { get; set; }
    public string truesight { get; set; }
    public string tremorsense { get; set; }
    public string languages { get; set; }
    public double challenge_rating { get; set; }
    public int proficiency_bonus { get; set; }
    public int xp { get; set; }
    public string sa_name { get; set; }
    public string sa_desc { get; set; } 
    public string u_type { get; set; }
    public string u_times { get; set; }
    public string u_dice { get; set; }
    public string u_min_value { get; set; }
    public string attack_bonus { get; set; }
    public string a_names { get; set; }
    public string a_desc { get; set; }
    public string a_u_type { get; set; }
    public string a_u_times { get; set; }
    public string a_u_dice { get; set; }
    public string a_u_min_value { get; set; }
    public string la_name { get; set; }
    public string la_desc { get; set; }
    public string desc { get; set; }
    public string subtype { get; set; }
    public string r_names { get; set; }
    public string r_descs { get; set; }
    public string forms { get; set; }
}
public class MagicItems
{
    public int ID { get; set; }
    public string name { get; set; }
    public string equipmentcategory { get; set; }
    public string rarity { get; set; }
    public string variants { get; set; }
    public bool variant { get; set; }
    public string desc { get; set; }
}
public class Equipment
{
    public int ID { get; set; }
    //ALL
    public string name { get; set; }//+
    public string equipment_category { get; set; }//+
    public string cost_amount { get; set; }
    public string cost_coin { get; set; }
    public double weight { get; set; }

    //OnlyWeapons
    public string weapon_category { get; set; }//+
    public string weapon_range { get; set; }//+
    public string category_range { get; set; }//+
    public string damage_dice { get; set; }//+
    public string damage_type { get; set; }//+
    public int range_normal{ get; set; }//+
    public int? range_long{ get; set; }//+
    public int ThrRannormal { get; set; }//+
    public int ThrRanlong { get; set; }//+
    public string TwoHandedDamagedamage_dice { get; set; }//+
    public string TwoHandedDamagedamage_type { get; set; }//+
    public string special { get; set; } //+

    //OnlyArmor
    public string armor_category { get; set; }//+
    public int ArmorClassbase { get; set; } //+
    public bool ArmorClassdex_bonus { get; set; }//+
    public int? ArmorClassmax_bonus { get; set; }//+
    public int? str_minimum { get; set; }
    public bool? stealth_disadvantage { get; set; }

    //OnlyTools
    public string tool_category { get; set; }//+

    //OnlyMounts
    public string vehicle_category { get; set; }//+
    public string speedquantity { get; set; }//+
    public string speedunit { get; set; }//+
    public string capacity { get; set; }//+


    //OnlyGear
    public string gear_category { get; set; }//+
    public string contents_amountss { get; set; }
    public int? quantity { get; set; }//+

    //Rand
    public string properties { get; set; }//+
    public string desc { get; set; }//+

    //unsorted
    public string contents_items { get; set; } //+

}

public class LootList
{
    public int ID { get; set; }
    public string Item { get; set; }
    public string Description { get; set; }
    public string EstValue { get; set; }
    public string Rarity { get; set; }
    public string Weight { get; set; }
    public string Category { get; set; }
    public string Properties { get; set; }
    public string Requirements { get; set; }
    public string Author { get; set; }
    public object RarityNumber { get; set; }
}
public partial class GlossaryView : UserControl
{
    public List<Spells> spells { get; set; } = new List<Spells>();
    public List<Bestiary> bestiary { get; set; } = new List<Bestiary>();
    public List<MagicItems> magicitems { get; set; } = new List<MagicItems>();
    public List<Equipment> EQ { get; set; } = new List<Equipment>();
    public ISettings settings { get; set; }
    Profile profile = new ConfigurationBuilder<Profile>().UseIniFile("Profile.ini").Build();
    public int profid { get; set; }
    public List<string> StSp { get; set; }
    public List<string> StBe { get; set; }
    public List<string> StMi { get; set; }
    public List<string> StEq { get; set; }
    public GlossaryView()
    
    {
        InitializeComponent();
        profid = profile.ProfileID;
        settings = new ConfigurationBuilder<ISettings>().UseIniFile("Settings/q0" + profid +".ini").Build();
        ini();
    }
    public void ini()
    {
        spells = new List<Spells>();
        bestiary = new List<Bestiary>();
        using (var ldb = new LiteDatabase("GlossData/Spells.db"))
        {
            var spellss = ldb.GetCollection<Spells>();
            foreach (var spl in spellss.FindAll())
            {
                spells.Add(spl);
            }
        }
        using (var ldb = new LiteDatabase("GlossData/Bestiary.db"))
        {
            var besti = ldb.GetCollection<Bestiary>();
            foreach (var best in besti.FindAll())
            {
                bestiary.Add(best);
            }
        }
        using (var ldb = new LiteDatabase("GlossData/MagicItems.db"))
        {
            var mi = ldb.GetCollection<MagicItems>();
            foreach (var best in mi.FindAll())
            {
                if (best.variants == null)
                {
                    magicitems.Add(best);
                }
            }
        }
        using (var ldb = new LiteDatabase("GlossData/Equipment.db"))
        {
            var eq = ldb.GetCollection<Equipment>();
            foreach (var best in eq.FindAll())
            {
                EQ.Add(best);
            }
        }
        List<string> SpellACSearch = new List<string>();
        foreach (var f in spells)
        {
            if (!SpellACSearch.Contains(f.name))
            {
                SpellACSearch.Add(f.name); 
            }

            foreach (var cl in f.classes.Split(","))
            {
                if (!SpellACSearch.Contains(cl))
                {
                    SpellACSearch.Add(cl);
                }   
            }
            if (!SpellACSearch.Contains(f.school))
            {
                SpellACSearch.Add(f.school);
            }
        }
        List<string> BestACSearch = new List<string>();
        foreach (var f in bestiary)
        {
            if (!BestACSearch.Contains(f.name))
            {
                BestACSearch.Add(f.name); 
            }

            if (!BestACSearch.Contains(f.type))
            {
                BestACSearch.Add(f.type);
            }
            if (!BestACSearch.Contains(f.size))
            {
                BestACSearch.Add(f.size);
            }
            if (!BestACSearch.Contains(f.alignment))
            {
                BestACSearch.Add(f.alignment);
            }
        }
        List<string> MISearch = new List<string>();
        foreach (var f in magicitems)
        {
            if (!MISearch.Contains(f.name))
            {
                MISearch.Add(f.name); 
            }
        }
        List<string> EQSearch = new List<string>();
        foreach (var f in EQ)
        {
            if (!EQSearch.Contains(f.name))
            {
                EQSearch.Add(f.name); 
            }
        }
        SpellsGrid.ItemsSource = spells;
        BestGrid.ItemsSource = bestiary;
        MIGrid.ItemsSource = magicitems;
        EQGrid.ItemsSource = EQ;
        SearchSpells.ItemsSource = SpellACSearch;
        SearchBest.ItemsSource = BestACSearch;
        SearchMI.ItemsSource = MISearch;
        SearchEQ.ItemsSource = EQSearch;
        StSp = settings.StarredSpells.Split("$").ToList();
        StarSP.ItemsSource = StSp;
        StBe = settings.StarredBest.Split("$").ToList();
        StarBest.ItemsSource = StBe;
        StMi = settings.StarredMI.Split("$").ToList();
        StarMI.ItemsSource = StMi;
        StEq = settings.StarredEQ.Split("$").ToList();
        StarEQ.ItemsSource = StEq;
    }
    private void List_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (SpellsGrid.SelectedItem == null)
        {
            return;
        }
        var thisspell = (SpellsGrid.SelectedItem as Spells);
        List<string> lvlss = new List<string>()
        {
            "Spell Slot Level 1 - " + thisspell.SL1,
            "Spell Slot Level 2 - " + thisspell.SL2,
            "Spell Slot Level 3 - " + thisspell.SL3,
            "Spell Slot Level 4 - " + thisspell.SL4,
            "Spell Slot Level 5 - " + thisspell.SL5,
            "Spell Slot Level 6 - " + thisspell.SL6,
            "Spell Slot Level 7 - " +   thisspell.SL7,
            "Spell Slot Level 8 - " +   thisspell.SL8,
            "Spell Slot Level 9 - " +   thisspell.SL9,
            "Character Level 1 - " +   thisspell.CL1,
            "Character Level 5 - " +    thisspell.CL5,
            "Character Level 11 - " +   thisspell.CL11,
            "Character Level 17 - " +   thisspell.CL17,
            "Spell Slot Level 1 - " +   thisspell.HASL1,
            "Spell Slot Level 2 - " +   thisspell.HASL2,
            "Spell Slot Level 3 - " +   thisspell.HASL3,
            "Spell Slot Level 4 - " +   thisspell.HASL4,
            "Spell Slot Level 5 - " +  thisspell.HASL5,
            "Spell Slot Level 6 - " +   thisspell.HASL6,
            "Spell Slot Level 7 - " +   thisspell.HASL7,
            "Spell Slot Level 8 - " +  thisspell.HASL8,
            "Spell Slot Level 9 - " + thisspell.HASL9
        };
        for (int i = 0; i < lvlss.Count; i++)
        {
            if (lvlss[i].Length < 22)
            {
                lvlss[i] = "";
            }
        }
        lvlss.RemoveAll(string.IsNullOrWhiteSpace);
        var ssl = Application.Current.FindResource("SpellSlotLevel").ToString();
        var ccl = Application.Current.FindResource("CharacterLevel").ToString();
        for (int i = 0; i < lvlss.Count; i++)
        {
            lvlss[i].Replace("Spell Slot Level", ssl).Replace("Character Level", ccl);
        }
        ThisSpell.Desc = $"{thisspell.desc}\r\n\r\n{thisspell.higher_level}";
        ThisSpell.Level = thisspell.level.ToString();
        ThisSpell.SpellName = thisspell.name;
        if (thisspell.components.Contains("V")) {
            ThisSpell.Verbal = true;
        }else 
        {
            ThisSpell.Verbal = false; 
        }

        if (thisspell.components.Contains("S"))
        {
            ThisSpell.Somatic = true;
        }else
        {
            ThisSpell.Somatic = false;
        }
        if (thisspell.material != null)
        {
            ThisSpell.Material = true;
            ThisSpell.Materials = thisspell.material;
        }else
        {
            ThisSpell.Material = false;
            ThisSpell.Materials = "none";
        }
        if (thisspell.concentration)
        {
            ThisSpell.Conc = true;
        }else
        {
            ThisSpell.Conc = false;
        }
        if (thisspell.ritual)
        {
            ThisSpell.Ritual = true;
        }else
        {
            ThisSpell.Ritual = false;
        }
        ThisSpell.School = thisspell.school;
        ThisSpell.Duration = thisspell.duration;
        ThisSpell.CastingTime = thisspell.casting_time;
        ThisSpell.Range = thisspell.range;
        string addesc = String.Empty;
        addesc += $"{Application.Current.FindResource("Classes")}: {thisspell.classes}\r\n\r\n";
        if (!string.IsNullOrEmpty(thisspell.subclasses))
        {
            addesc += $"{Application.Current.FindResource("Subclasses")}: {thisspell.subclasses}\r\n\r\n";
        }
        if (thisspell.dctype != null || thisspell.dcsuccess != null || thisspell.dcdesc != null)
        {
            addesc += $"{Application.Current.FindResource("SavingThrow")}: {thisspell.dctype}\r\nSuccess: {thisspell.dcsuccess.Replace("none", "No effect").Replace("half", "Half")} {thisspell.dcdesc}\r\n\r\n";
        }
        if (thisspell.DamageType != null || thisspell.attack_type != null)
        {
            addesc += $"{Application.Current.FindResource("Damage")}: {thisspell.DamageType} {thisspell.attack_type}\r\n\r\n";
        }
        int? m = null;
        if (thisspell.AreaSize != -99)
        {
            m = thisspell.AreaSize;
        }
        if (thisspell.AreaType != null || m != null)
        {
            addesc += $"{Application.Current.FindResource("Areadamage")}: {thisspell.AreaType} - {m}ft\r\n\r\n";
        }
        foreach (var lv in lvlss)
        {
            addesc += $"{lv}\r\n";
        }
        ThisSpell.classes = addesc;
        if (StSp.Contains(thisspell.name))
        {
            StarBtnSP.IsChecked = true;
            var c = 0;
            foreach (var v in StarSP.Items)
            {
                var sln = ((sender as DataGrid).SelectedItem as Spells).name;
                if (v.ToString() == sln)
                {
                    StarSP.SelectedIndex = c;
                }
                c++;
            }
        }
        else
        {
            StarBtnSP.IsChecked = false;
            StarSP.SelectedItem = null;
        }
    }
    private void BestGrid_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (BestGrid.SelectedItem == null)
        {
            return;
        }
        var srs = ((sender as DataGrid).SelectedItem as Bestiary);
        ThisBest.BName = $"{Application.Current.FindResource("Name")}: {srs.name}";
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
        if (StBe.Contains(((sender as DataGrid).SelectedItem as Bestiary).name))
        {
            StarBtnBest.IsChecked = true;
            var c = 0;
            foreach (var v in StarBest.Items)
            {
                var sln = ((sender as DataGrid).SelectedItem as Bestiary).name;
                if (v.ToString() == sln)
                {
                    StarBest.SelectedIndex = c;
                }
                c++;
            }
        }
        else
        {
            StarBtnBest.IsChecked = false;
            StarBest.SelectedItem = null;
        }
        
    }
    private void MIGrid_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (MIGrid.SelectedItem == null)
        {
            return;
        }
        var thisMI = (MIGrid.SelectedItem as MagicItems);
        ThisMI.Name0 = thisMI.name;
        ThisMI.rarity = thisMI.rarity;
        ThisMI.desc = thisMI.desc;
        ThisMI.eqc = thisMI.equipmentcategory;
        if (StMi.Contains(thisMI.name))
        {
            StarBtnMI.IsChecked = true;
            var c = 0;
            foreach (var v in StarMI.Items)
            {
                var sln = ((sender as DataGrid).SelectedItem as MagicItems).name;
                if (v.ToString() == sln)
                {
                    StarMI.SelectedIndex = c;
                }
                c++;
            }
        }
        else
        {
            StarBtnMI.IsChecked = false;
            StarMI.SelectedItem = null;
        }
    }
    private void EQGrid_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (EQGrid.SelectedItem == null)
        {
            return;
        }
        var opnd = (EQGrid.SelectedItem as Equipment);
        switch (opnd.equipment_category)
        {
            case "Weapon":
                ThisEQ.Weapon = true;
                ThisEQ.Armor = false;
                ThisEQ.Vehicle = false;
                ThisEQ.Gear = false;
                ThisEQ.Tool = false;
                ThisEQ.SubCategory = opnd.category_range;
                break;
            case "Armor":
                ThisEQ.Weapon = false;
                ThisEQ.Armor = true;
                ThisEQ.Vehicle = false;
                ThisEQ.Gear = false;
                ThisEQ.Tool = false;
                ThisEQ.SubCategory = opnd.armor_category;
                break;
            case "Adventuring Gear":
                ThisEQ.Weapon = false;
                ThisEQ.Armor = false;
                ThisEQ.Vehicle = false;
                ThisEQ.Gear = true;
                ThisEQ.Tool = false;
                ThisEQ.SubCategory = opnd.gear_category;
                break;
            case "Tools":
                ThisEQ.Weapon = false;
                ThisEQ.Armor = false;
                ThisEQ.Vehicle = false;
                ThisEQ.Gear = false;
                ThisEQ.Tool = true;
                ThisEQ.SubCategory = opnd.tool_category;
                break;
            case "Mounts and Vehicles":
                ThisEQ.Weapon = false;
                ThisEQ.Armor = false;
                ThisEQ.Vehicle = true;
                ThisEQ.Gear = false;
                ThisEQ.Tool = false;
                ThisEQ.SubCategory = opnd.vehicle_category;
                break;
        }
        ThisEQ.Name0 = opnd.name;
        ThisEQ.Category = opnd.equipment_category;
        ThisEQ.Desc = $"{opnd.special}\r\n";
        ThisEQ.Desc += opnd.desc;
        ThisEQ.Price = $"{opnd.cost_amount} {opnd.cost_coin}";
        ThisEQ.Weight = $"{opnd.weight}";
        //Weapons
        ThisEQ.NormalRange = $"{opnd.range_normal} ft.";
        if (opnd.range_long == null || opnd.range_long == 0)
        {
            ThisEQ.LongRange = "None";
        }
        else
        {
            ThisEQ.LongRange = $"{opnd.range_long} ft.";
        }
        if (opnd.ThrRannormal == 0)
        {
            ThisEQ.ThrowingRange = $"None";
        }
        else
        {
            ThisEQ.ThrowingRange = $"{opnd.ThrRannormal} ft.";
        }
        if (opnd.ThrRanlong == 0)
        {
            ThisEQ.LongThrowingRange = $"None";
        }
        else
        {
            ThisEQ.LongThrowingRange = $"{opnd.ThrRanlong} ft.";
        }
        if (opnd.damage_dice == null && opnd.damage_type == null)
        {
            ThisEQ.DamDpT = $"None";
        }
        else
        {
            ThisEQ.DamDpT = $"{opnd.damage_type} {opnd.damage_dice}";
        }

        if (opnd.TwoHandedDamagedamage_type == null && opnd.TwoHandedDamagedamage_dice == null)
        {
            ThisEQ.THdam = "None";
        }
        else
        {
            ThisEQ.THdam = $"{opnd.TwoHandedDamagedamage_type} {opnd.TwoHandedDamagedamage_dice}";
        }
        if (opnd.properties == null)
        {
            ThisEQ.huh = false;
        }
        else
        {
            ThisEQ.huh = true;
            ThisEQ.Tags = opnd.properties.Replace("$", ", ");
        }
        //Armor
        ThisEQ.BaseAC = $"{opnd.ArmorClassbase}";
        if (opnd.ArmorClassdex_bonus)
        {
            ThisEQ.DexBon = $"+DEX, max +{opnd.ArmorClassmax_bonus}";
        }
        ThisEQ.MinStr = $"Minimal strength: {opnd.str_minimum}";
        ThisEQ.StDis = $"Stealth disadvantage: No";
        if (opnd.stealth_disadvantage == true)
        {
            ThisEQ.StDis.Replace("No", "Yes");
        }
        //Mounts
        if (opnd.speedquantity == "0" && opnd.speedunit == null)
        {
            ThisEQ.Speed = $"None";
        }
        else
        {
            ThisEQ.Speed = $"{opnd.speedquantity} {opnd.speedunit}";
        }
        if (opnd.capacity == null)
        {
            ThisEQ.Capacity = "None";
        }
        else
        {
            ThisEQ.Capacity = $"{opnd.capacity}";
        }
        //Gear
        string f = String.Empty;
        if (opnd.contents_amountss != "0" && opnd.properties != null)
        {
            var kjdd = opnd.properties.Split("$").ToList();
            kjdd.Remove(kjdd[0]);
            var ghtnhk = opnd.contents_amountss.Split("$").ToList();
            for (int i = 0; i < ghtnhk.Count; i++)
            {
                f += $"{kjdd[i]} {ghtnhk[i]}\r\n";
            }
        }
        else
        {
            f = "None";
        }
        ThisEQ.Contents = f;
        if (opnd.quantity == null)
        {
            ThisEQ.quantity = "1";
        }
        else
        {
            ThisEQ.quantity = opnd.quantity.ToString();
        }
        if (StEq.Contains(ThisEQ.Name0))
        {
            StarBtnEQ.IsChecked = true;
            var c = 0;
            foreach (var v in StarEQ.Items)
            {
                var sln = ((sender as DataGrid).SelectedItem as Equipment).name;
                if (v.ToString() == sln)
                {
                    StarEQ.SelectedIndex = c;
                }
                c++;
            }
        }
        else
        {
            StarBtnEQ.IsChecked = false;
            StarEQ.SelectedItem = null;
        }
    }
    private void SearchSpells_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        var filteredResults = from spell in spells
            where spell.name.Contains((sender as AutoCompleteBox).Text, StringComparison.OrdinalIgnoreCase) ||
                  spell.classes.Contains((sender as AutoCompleteBox).Text, StringComparison.OrdinalIgnoreCase)||
                  spell.school.Contains((sender as AutoCompleteBox).Text, StringComparison.OrdinalIgnoreCase)
            select spell;
        SpellsGrid.ItemsSource = filteredResults;
    }
    private void SearchBest_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        var filteredResults = from best in bestiary
            where best.name.Contains((sender as AutoCompleteBox).Text, StringComparison.OrdinalIgnoreCase)||
                  best.size.Contains((sender as AutoCompleteBox).Text, StringComparison.OrdinalIgnoreCase)||
                  best.type.Contains((sender as AutoCompleteBox).Text, StringComparison.OrdinalIgnoreCase)||
                  best.alignment.Contains((sender as AutoCompleteBox).Text, StringComparison.OrdinalIgnoreCase)

            select best;
        BestGrid.ItemsSource = filteredResults;    
    }
    private void SearchMI_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        var filteredResults = from mi in magicitems
            where mi.name.Contains((sender as AutoCompleteBox).Text, StringComparison.OrdinalIgnoreCase)
            select mi;
        MIGrid.ItemsSource = filteredResults;        
    }
    private void SearchEQ_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        var filteredResults = from eq in EQ
            where eq.name.Contains((sender as AutoCompleteBox).Text, StringComparison.OrdinalIgnoreCase)
            select eq;
        EQGrid.ItemsSource = filteredResults;    
    }

    private void StarBtnSP_OnIsCheckedChanged(object? sender, RoutedEventArgs e)
    {
        if (SpellsGrid.SelectedItem == null)
        {
            (sender as ToggleButton).IsChecked = false;
            return;
        }
        switch ((sender as ToggleButton).IsChecked)
        {
            case true:
                StSp.Insert(0, (SpellsGrid.SelectedItem as Spells).name);
                break;
            case false:
                StSp.Remove((SpellsGrid.SelectedItem as Spells).name);
                break;
        }
        StarSP.ItemsSource = null;
        StarSP.ItemsSource = StSp;
        settings.StarredSpells = string.Join('$', StSp);
    }

    private void StarSP_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if ((sender as ListBox).SelectedItem == null)
        {
            return;
        }
        if ((sender as ListBox).SelectedItem == (sender as ListBox).Items.Last())
        {
            (sender as ListBox).SelectedItem = null;
            return;
        }
        var sln = (sender as ListBox).SelectedItem.ToString();
        var c = 0;
        foreach (var v in SpellsGrid.ItemsSource)
        {
            if ((v as Spells).name == sln)
            {
                SpellsGrid.SelectedIndex = c;
                return;
            }
            c++;
        }
    }

    private void StarBtnBest_OnIsCheckedChanged(object? sender, RoutedEventArgs e)
    {
        if (BestGrid.SelectedItem == null)
        {
            (sender as ToggleButton).IsChecked = false;
            return;
        }
        switch ((sender as ToggleButton).IsChecked)
        {
            case true:
                StBe.Insert(0, (BestGrid.SelectedItem as Bestiary).name);
                break;
            case false:
                StBe.Remove((BestGrid.SelectedItem as Bestiary).name);
                break;
        }
        StarBest.ItemsSource = null;
        StarBest.ItemsSource = StBe;
        settings.StarredBest = string.Join('$', StBe);
    }

    private void StarBest_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if ((sender as ListBox).SelectedItem == null)
        {
            return;
        }
        if ((sender as ListBox).SelectedItem == (sender as ListBox).Items.Last())
        {
            (sender as ListBox).SelectedItem = null;
            return;
        }
        var sln = (sender as ListBox).SelectedItem.ToString();
        var c = 0;
        foreach (var v in BestGrid.ItemsSource)
        {
            if ((v as Bestiary).name == sln)
            {
                BestGrid.SelectedIndex = c;
                return;
            }
            c++;
        }
    }

    private void StarBtnMI_OnIsCheckedChanged(object? sender, RoutedEventArgs e)
    {
        if (MIGrid.SelectedItem == null)
        {
            (sender as ToggleButton).IsChecked = false;
            return;
        }
        switch ((sender as ToggleButton).IsChecked)
        {
            case true:
                StMi.Insert(0, (MIGrid.SelectedItem as MagicItems).name);
                break;
            case false:
                StMi.Remove((MIGrid.SelectedItem as MagicItems).name);
                break;
        }
        StarMI.ItemsSource = null;
        StarMI.ItemsSource = StMi;
        settings.StarredMI = string.Join('$', StMi);
    }

    private void StarMI_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if ((sender as ListBox).SelectedItem == null)
        {
            return;
        }
        if ((sender as ListBox).SelectedItem == (sender as ListBox).Items.Last())
        {
            (sender as ListBox).SelectedItem = null;
            return;
        }
        var sln = (sender as ListBox).SelectedItem.ToString();
        var c = 0;
        foreach (var v in MIGrid.ItemsSource)
        {
            if ((v as MagicItems).name == sln)
            {
                MIGrid.SelectedIndex = c;
                return;
            }
            c++;
        }
    }

    private void StarBtnEQ_OnIsCheckedChanged(object? sender, RoutedEventArgs e)
    {
        if (EQGrid.SelectedItem == null)
        {
            (sender as ToggleButton).IsChecked = false;
            return;
        }
        switch ((sender as ToggleButton).IsChecked)
        {
            case true:
                StEq.Insert(0, (EQGrid.SelectedItem as Equipment).name);
                break;
            case false:
                StEq.Remove((EQGrid.SelectedItem as Equipment).name);
                break;
        }
        StarEQ.ItemsSource = null;
        StarEQ.ItemsSource = StEq;
        settings.StarredEQ = string.Join('$', StEq);
    }

    private void StarEQ_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if ((sender as ListBox).SelectedItem == null)
        {
            return;
        }
        if ((sender as ListBox).SelectedItem == (sender as ListBox).Items.Last())
        {
            (sender as ListBox).SelectedItem = null;
            return;
        }
        var sln = (sender as ListBox).SelectedItem.ToString();
        var c = 0;
        foreach (var v in EQGrid.ItemsSource)
        {
            if ((v as Equipment).name == sln)
            {
                EQGrid.SelectedIndex = c;
                return;
            }
            c++;
        }
    }
}