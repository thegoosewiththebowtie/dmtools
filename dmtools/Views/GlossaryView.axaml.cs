using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml.Styling;
using dmtools.PopUps;
using dmtools.GlossData;
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
    /*public List<SpellData0> spells { get; set; }
    public GlossaryView()
    {
        InitializeComponent();
        spells = new List<SpellData0>();
        using (var ldb = new LiteDatabase("GlossData/Glossary.db"))
        {
            var spellss = ldb.GetCollection<SpellData0>();
            foreach (var spl in spellss.FindAll())
            {
                spells.Add(spl);
            }
        }
        SearchSpells.ItemsSource = spells;
        SpelsGrid.ItemsSource = spells;
    }


    private void List_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        using (var ldb = new LiteDatabase("GlossData/Glossary.db"))
        {
            var spellss = ldb.GetCollection<SpellData0>();
            var selspl = spellss.FindById(((sender as DataGrid).SelectedItem as SpellData0).ID);
        }
    }*/
}