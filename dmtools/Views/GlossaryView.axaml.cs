﻿using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml.Styling;
using AVFoundation;
using dmtools.PopUps;
using dmtools.GlossData;
using DynamicData;
using LiteDB;

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
public partial class GlossaryView : UserControl
{
    public List<Spells> spells { get; set; }
    public List<Bestiary> bestiary { get; set; }
    public GlossaryView()
    {
        InitializeComponent();
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
        SearchSpells.ItemsSource = spells;
        SpellsGrid.ItemsSource = spells;
        SearchBest.ItemsSource = bestiary;
        BestGrid.ItemsSource = bestiary;
    }


    private void List_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
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
        ThisSpell.Levels = lvlss;
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
        ThisSpell.classes = $"Classes: {thisspell.classes}";
        ThisSpell.subclasses = $"Subclasses: {thisspell.subclasses}";
        if (thisspell.dctype != null || thisspell.dcsuccess != null || thisspell.dcdesc != null)
        {
            ThisSpell.dctypensucess = $"Saving Throw: {thisspell.dctype} {thisspell.dcsuccess} {thisspell.dcdesc}";
        }
        if (thisspell.DamageType != null || thisspell.attack_type != null)
        {
            ThisSpell.damage = $"Damage: {thisspell.DamageType} {thisspell.attack_type}";
        }
        int? m = null;
        if (thisspell.AreaSize != -99)
        {
            m = thisspell.AreaSize;
        }
        if (thisspell.AreaType != null || m != null)
        {
            ThisSpell.Area = $"Area damage: {thisspell.AreaType} - {m}ft";
        }
    }

    private void BestGrid_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var srs = ((sender as DataGrid).SelectedItem as Bestiary);
        ThisBest.BName = $"Name: {srs.name}";
        ThisBest.Aligment = $"Aligment: {srs.alignment}";
        ThisBest.Languages = $"Languages: {srs.languages}";
        ThisBest.Type = $"Type: {srs.type}";
        ThisBest.Subtype = $"Subtype: {srs.subtype}";
        ThisBest.Size = $"Size: {srs.size}";
        ThisBest.Speed = srs.speed_walk;
        ThisBest.Swim = srs.speed_swim;
        ThisBest.Fly = srs.speed_fly;
        ThisBest.Burrow = srs.speed_burrow;
        ThisBest.Climb = srs.speed_climb;
        ThisBest.Immunities = $"Damage immunities: {srs.damage_immunities}\r\n\r\n" +
                              $"Damage resistances: {srs.damage_resistances}\r\n\r\n" +
                              $"Conditions: {srs.condition_immunities}";
        if (srs.damage_vulnerabilities != null)
        {
            ThisBest.Vul = $"{srs.damage_vulnerabilities}";
        }
        else
        {
            ThisBest.Vul = "None";
        }
        ThisBest.Senses = $"Darkvision: {srs.darkvision}\r\n\r\n" +
                          $"Passive perception: {srs.passive_perception}\r\n\r\n" +
                          $"Blindsight: {srs.blindsight}\r\n\r\n" +
                          $"Truesight: {srs.truesight}\r\n\r\n" +
                          $"Tremorsense: {srs.tremorsense}";
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
            attacks = "None";
        }
        if (srs.la_name != null)
        {
            var lanames = srs.la_name.Split("$");
            var ladescs = srs.la_desc.Split("$");
            attacks += $"Legendary attacks: \r\n";
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
            ThisBest.Abilities = "None";
        }
        if (srs.desc != null)
        {
            ThisBest.Desc = srs.desc;
        }
        else
        {
            ThisBest.Desc = "None";
        }
        
    }
}