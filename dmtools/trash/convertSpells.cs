using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using dmtools.Views;
using DryIoc.ImTools;
using GLib;
using LiteDB;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pango;
using JsonSerializer = LiteDB.JsonSerializer;

namespace dmtools.GlossData;

/* Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
    public class AreaOfEffect
    {
        public string type { get; set; }
        public int size { get; set; }
    }

    public class Class
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Damage
    {
        public DamageType damage_type { get; set; }
        public DamageAtSlotLevel damage_at_slot_level { get; set; }
        public DamageAtCharacterLevel damage_at_character_level { get; set; }
    }

    public class DamageAtCharacterLevel
    {
        [JsonProperty("1")]
        public string _1 { get; set; }

        [JsonProperty("5")]
        public string _5 { get; set; }

        [JsonProperty("11")]
        public string _11 { get; set; }

        [JsonProperty("17")]
        public string _17 { get; set; }
    }

    public class DamageAtSlotLevel
    {
        [JsonProperty("2")]
        public string _2 { get; set; }

        [JsonProperty("3")]
        public string _3 { get; set; }

        [JsonProperty("4")]
        public string _4 { get; set; }

        [JsonProperty("5")]
        public string _5 { get; set; }

        [JsonProperty("6")]
        public string _6 { get; set; }

        [JsonProperty("7")]
        public string _7 { get; set; }

        [JsonProperty("8")]
        public string _8 { get; set; }

        [JsonProperty("9")]
        public string _9 { get; set; }

        [JsonProperty("1")]
        public string _1 { get; set; }
    }

    public class DamageType
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Dc
    {
        public DcType dc_type { get; set; }
        public string dc_success { get; set; }
        public string desc { get; set; }
    }

    public class DcType
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class HealAtSlotLevel
    {
        [JsonProperty("2")]
        public string _2 { get; set; }

        [JsonProperty("3")]
        public string _3 { get; set; }

        [JsonProperty("4")]
        public string _4 { get; set; }

        [JsonProperty("5")]
        public string _5 { get; set; }

        [JsonProperty("6")]
        public string _6 { get; set; }

        [JsonProperty("7")]
        public string _7 { get; set; }

        [JsonProperty("8")]
        public string _8 { get; set; }

        [JsonProperty("9")]
        public string _9 { get; set; }

        [JsonProperty("1")]
        public string _1 { get; set; }
    }

    public class Root
    {
        public string index { get; set; }
        public string name { get; set; }
        public List<string> desc { get; set; }
        public List<string> higher_level { get; set; }
        public string range { get; set; }
        public List<string> components { get; set; }
        public string material { get; set; }
        public bool ritual { get; set; }
        public string duration { get; set; }
        public bool concentration { get; set; }
        public string casting_time { get; set; }
        public int level { get; set; }
        public string attack_type { get; set; }
        public Damage damage { get; set; }
        public School school { get; set; }
        public List<Class> classes { get; set; }
        public List<Subclass> subclasses { get; set; }
        public string url { get; set; }
        public Dc dc { get; set; }
        public HealAtSlotLevel heal_at_slot_level { get; set; }
        public AreaOfEffect area_of_effect { get; set; }
    }

    public class School
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Subclass
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class All
    {
        public List<Root> all { get; set; }
    }

public class convertSpells
{
    public static void ConvertStartTest()
    {
        using StreamReader reader = new("a/s.json");
        var json = reader.ReadToEnd();
        All myDeserializedClass = JsonConvert.DeserializeObject<All>(json);
        using (var ldb = new LiteDatabase("GlossData/Glossary.db"))
        {
            var sp = ldb.GetCollection<Spells>();
            foreach (var spell0 in myDeserializedClass.all)
            {
                if (spell0.area_of_effect == null)
                {
                    spell0.area_of_effect = new AreaOfEffect() {size = -99, type = ""};
                }
                if (spell0.dc == null)
                {
                    spell0.dc = new Dc() { desc = "", dc_success = "", dc_type = new DcType(){index = "", url = "", name = ""} };
                }
                if (spell0.damage == null)
                {
                    spell0.damage = new Damage()
                    {
                        damage_type = new DamageType() {index = "", name = "", url = "",}, damage_at_character_level = new DamageAtCharacterLevel(){_1 = "", _5 = "", _11 = "", _17 = ""},
                        damage_at_slot_level = new DamageAtSlotLevel(){_1 = "", _2 = "", _3 = "", _4 = "", _5 = "", _6 = "", _7 = "", _8 = "", _9 = ""}
                    };
                }
                if (spell0.damage.damage_at_character_level == null)
                {
                    spell0.damage.damage_at_character_level = new DamageAtCharacterLevel()
                        { _1 = "", _5 = "", _11 = "", _17 = "" };
                }
                if (spell0.damage.damage_at_character_level._1 == null)
                {
                    spell0.damage.damage_at_character_level._1 = "";
                }
                if (spell0.damage.damage_at_character_level._5 == null)
                {
                    spell0.damage.damage_at_character_level._5 = "";
                }
                if (spell0.damage.damage_at_character_level._11 == null)
                {
                    spell0.damage.damage_at_character_level._11 = "";
                }
                if (spell0.damage.damage_at_character_level._17 == null)
                {
                    spell0.damage.damage_at_character_level._17 = "";
                }

                if (spell0.damage.damage_at_slot_level == null)
                {
                    spell0.damage.damage_at_slot_level = new DamageAtSlotLevel()
                        { _1 = "", _2 = "", _3 = "", _4 = "", _5 = "", _6 = "", _7 = "", _8 = "", _9 = "" };
                }
                if (spell0.damage.damage_at_slot_level._1 == null)
                {
                    spell0.damage.damage_at_slot_level._1 = "";
                }
                if (spell0.damage.damage_at_slot_level._2 == null)
                {
                    spell0.damage.damage_at_slot_level._2 = "";
                }
                if (spell0.damage.damage_at_slot_level._3 == null)
                {
                    spell0.damage.damage_at_slot_level._3 = "";
                }
                if (spell0.damage.damage_at_slot_level._4 == null)
                {
                    spell0.damage.damage_at_slot_level._4 = "";
                }
                if (spell0.damage.damage_at_slot_level._5 == null)
                {
                    spell0.damage.damage_at_slot_level._5 = "";
                }
                if (spell0.damage.damage_at_slot_level._6 == null)
                {
                    spell0.damage.damage_at_slot_level._6 = "";
                }
                if (spell0.damage.damage_at_slot_level._7 == null)
                {
                    spell0.damage.damage_at_slot_level._7 = "";
                }
                if (spell0.damage.damage_at_slot_level._8 == null)
                {
                    spell0.damage.damage_at_slot_level._8 = "";
                }
                if (spell0.damage.damage_at_slot_level._9 == null)
                {
                    spell0.damage.damage_at_slot_level._9 = "";
                }
                if (spell0.heal_at_slot_level == null)
                {
                    spell0.heal_at_slot_level = new HealAtSlotLevel()
                        { _1 = "", _2 = "", _3 = "", _4 = "", _6 = "", _7 = "", _8 = "", _9 = "", _5 = "" };
                }
                if (spell0.heal_at_slot_level._1 == null)
                {
                    spell0.heal_at_slot_level._1 = "";
                }
                if (spell0.heal_at_slot_level._2 == null)
                {
                    spell0.heal_at_slot_level._2 = "";
                }
                if (spell0.heal_at_slot_level._3 == null)
                {
                    spell0.heal_at_slot_level._3 = "";
                }
                if (spell0.heal_at_slot_level._4 == null)
                {
                    spell0.heal_at_slot_level._4 = "";
                }
                if (spell0.heal_at_slot_level._5 == null)
                {
                    spell0.heal_at_slot_level._5 = "";
                }
                if (spell0.heal_at_slot_level._6 == null)
                {
                    spell0.heal_at_slot_level._6 = "";
                }
                if (spell0.heal_at_slot_level._7 == null)
                {
                    spell0.heal_at_slot_level._7 = "";
                }
                if (spell0.heal_at_slot_level._8 == null)
                {
                    spell0.heal_at_slot_level._8 = "";
                }
                if (spell0.heal_at_slot_level._9 == null)
                {
                    spell0.heal_at_slot_level._9 = "";
                }
                if (spell0.name == null)
                {
                    spell0.name = "";
                }
                if (spell0.attack_type == null)
                {
                    spell0.attack_type = "";
                }
                if (spell0.casting_time == null)
                {
                    spell0.casting_time = "";
                }
                if (spell0.classes == null)
                {
                    spell0.classes = new List<Class>() {new Class(){name = ""} };
                }
                List<string> classsssses = new List<string>();
                foreach (var cc in spell0.classes)
                {
                    classsssses.Add(cc.name);
                }
                if (spell0.higher_level == null)
                {
                    spell0.higher_level = new List<string>() { "" };
                }
                if (spell0.material == null)
                {
                    spell0.material = "";
                }

                if (spell0.subclasses == null)
                {
                    spell0.subclasses = new List<Subclass>(){new Subclass(){name = ""}};
                }
                List<string> subbbs = new List<string>();
                foreach (var sss in spell0.subclasses)
                {
                    subbbs.Add(sss.name);
                }
                if (spell0.damage.damage_type == null)
                {
                    spell0.damage.damage_type = new DamageType(){name = ""};
                }

                if (spell0.school == null)
                {
                    spell0.school = new School() { name = "" };
                }
                sp.Insert(new Spells()
                {
                    name = spell0.name, AreaType = spell0.area_of_effect.type, AreaSize = spell0.area_of_effect.size,
                    attack_type = spell0.attack_type, casting_time = spell0.casting_time,
                    classes = string.Join(", ", classsssses), concentration = spell0.concentration, dcdesc = spell0.dc.desc,
                    dcsuccess = spell0.dc.dc_success, dctype = spell0.dc.dc_type.name, DamageType = spell0.damage.damage_type.name,
                    SL1 = spell0.damage.damage_at_slot_level._1, SL2 = spell0.damage.damage_at_slot_level._2, SL3 = spell0.damage.damage_at_slot_level._3,
                    SL4 = spell0.damage.damage_at_slot_level._4, SL5 = spell0.damage.damage_at_slot_level._5,SL6 = spell0.damage.damage_at_slot_level._6,
                    SL7 = spell0.damage.damage_at_slot_level._7,SL8 = spell0.damage.damage_at_slot_level._8, SL9 = spell0.damage.damage_at_slot_level._9,
                    CL1 = spell0.damage.damage_at_character_level._1, CL5 = spell0.damage.damage_at_character_level._5,
                    CL11 = spell0.damage.damage_at_character_level._11, CL17 = spell0.damage.damage_at_character_level._17,
                    HASL1 = spell0.heal_at_slot_level._1, HASL2 = spell0.heal_at_slot_level._2,HASL3 = spell0.heal_at_slot_level._3,
                    HASL4 = spell0.heal_at_slot_level._4,HASL5 = spell0.heal_at_slot_level._5, HASL6 = spell0.heal_at_slot_level._6,
                    HASL7 = spell0.heal_at_slot_level._7, HASL8 = spell0.heal_at_slot_level._8, HASL9 = spell0.heal_at_slot_level._9,
                    desc = string.Join("\r\n", spell0.desc),
                    duration = spell0.duration, range = spell0.range, ritual = spell0.ritual, components = string.Join("$", spell0.components),
                    subclasses = string.Join(", ",subbbs), level = spell0.level, higher_level = string.Join("\r\n", spell0.higher_level),
                    material = spell0.material, school = spell0.school.name
                });
            }
        }
    }
}
*/