using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using DryIoc.ImTools;
using LiteDB;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pango;
using JsonSerializer = LiteDB.JsonSerializer;

namespace dmtools.GlossData;

public class Components
    {
        public bool v { get; set; }
        public bool s { get; set; }
        public object m { get; set; }
    }

    public class Distance
    {
        public string type { get; set; }
        public int amount { get; set; }
    }

    public class Duration
    {
        public string type { get; set; }
        public Duration duration { get; set; }
        public bool? concentration { get; set; }
        public List<string> ends { get; set; }
        public int amount { get; set; }
    }

    public class EntriesHigherLevel
    {
        public string type { get; set; }
        public string name { get; set; }
        public List<string> entries { get; set; }
    }

    public class Meta
    {
        public bool ritual { get; set; }
    }

    public class OtherSource
    {
        public string source { get; set; }
        public int page { get; set; }
    }

    public class Range
    {
        public string type { get; set; }
        public Distance distance { get; set; }
    }

    public class Root
    {
        public List<Spell> spell { get; set; }
    }

    public class Scaling
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

    public class ScalingLevelDice
    {
        public string label { get; set; }
        public Scaling scaling { get; set; }
    }

    public class Spell
    {
        public string name { get; set; }
        public string source { get; set; }
        public int page { get; set; }
        public object srd { get; set; }
        public bool basicRules { get; set; }
        public int level { get; set; }
        public string school { get; set; }
        public List<Time> time { get; set; }
        public Range range { get; set; }
        public Components components { get; set; }
        public List<Duration> duration { get; set; }
        public List<object> entries { get; set; }
        public ScalingLevelDice scalingLevelDice { get; set; }
        public List<string> damageInflict { get; set; }
        public List<string> savingThrow { get; set; }
        public List<string> miscTags { get; set; }
        public List<string> areaTags { get; set; }
        public List<OtherSource> otherSources { get; set; }
        public List<EntriesHigherLevel> entriesHigherLevel { get; set; }
        public Meta meta { get; set; }
        public List<string> conditionInflict { get; set; }
        public List<string> affectsCreatureType { get; set; }
        public List<string> damageResist { get; set; }
        public bool? hasFluffImages { get; set; }
        public List<string> spellAttack { get; set; }
        public List<string> abilityCheck { get; set; }
    }
    public class SpellData0
    {
        public int ID { get; set; }
        public string name { get; set; }
        public string source { get; set; }
        public int level { get; set; }
        public string school { get; set; }
        public string time { get; set; }
        public string range { get; set; }
        public bool s { get; set; }
        public bool v { get; set; }
        public string? m { get; set; }
        public string duration { get; set; }
        public bool? concentration { get; set; }
        public string entries { get; set; }
        public string scalingLevelDice { get; set; }
        public string damageInflict { get; set; }
        public string savingThrow { get; set; }
        public bool ritual { get; set; }
        public string conditionInflict { get; set; }
        public string affectsCreatureType { get; set; }
        public string damageResist { get; set; }
        public string spellAttack { get; set; }
        public string abilityCheck { get; set; }
    }
    public class Time
    {
        public int number { get; set; }
        public string unit { get; set; }
    }

public class convert
{
    public static void ConvertStartTest()
    {
        using StreamReader reader = new("/(18).json");
        var json = reader.ReadToEnd();
        Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(json);
        foreach (var spell in myDeserializedClass.spell)
        {
            string entries = "";
            foreach (var e in spell.entries)
            {
                entries += "\r\n" + e.ToString();
            }
            string timenumber = "";
            string timeunit = "";
            foreach (var tim in spell.time)
            {
                timeunit += tim.unit;
                timenumber += tim.number;
            }
            string amount = "";
            string type = "";
            bool? concentration = false;
            foreach (var d in spell.duration)
            {
                amount += d.amount;
                type += d.type;
                concentration = d.concentration;
            }
            string m = "";
            if (spell.components.m != null)
            {
                m = spell.components.m.ToString();
            }
            if (concentration == null)
            {
                concentration = false;
            }
            bool ritual;
            if (spell.meta == null)
            {
                ritual = false;
            }
            else
            {
                ritual = spell.meta.ritual;
            }
            string damageinflict = "";
            if (spell.damageInflict != null)
            {
                damageinflict = string.Join("\r\n", spell.damageInflict);
            }
            string saavingthrow = "";
            if (spell.savingThrow != null)
            {
                saavingthrow = string.Join("\r\n", spell.savingThrow);
            }
            string conditioninflict = "";
            if (spell.conditionInflict != null)
            {
                conditioninflict = string.Join("\r\n", spell.conditionInflict);
            }
            string affectscreaturetype = "";
            if (spell.affectsCreatureType != null)
            {
                affectscreaturetype = string.Join("\r\n", spell.affectsCreatureType);
            }
            string damageresist = "";
            if (spell.damageResist != null)
            {
                damageresist = string.Join("\r\n", spell.damageResist);
            }
            string spellattack = "";
            if (spell.spellAttack != null)
            {
                spellattack = string.Join("\r\n", spell.spellAttack);
            }
            string abilitycheck = "";
            if (spell.abilityCheck != null)
            {
                abilitycheck = string.Join("\r\n", spell.abilityCheck);
            }

            string sldlabel = "";
            string sld1 = "";
            string sld5 = "";
            string sld11 = "";
            string sld17 = "";
            if (spell.scalingLevelDice != null)
            {
                if (spell.scalingLevelDice.label != null)
                {
                    sldlabel = spell.scalingLevelDice.label;
                }
                if (spell.scalingLevelDice.scaling._1 != null)
                {
                    sld1 = spell.scalingLevelDice.scaling._1;
                }
                if (spell.scalingLevelDice.scaling._5 != null)
                {
                    sld5 = spell.scalingLevelDice.scaling._5;
                }
                if (spell.scalingLevelDice.scaling._11 != null)
                {
                    sld11 = spell.scalingLevelDice.scaling._11;
                }
                if (spell.scalingLevelDice.scaling._17 != null)
                {
                    sld17 = spell.scalingLevelDice.scaling._17;
                }

            }
            string rangetype = "";
            int rangedystance = 0;
            string rangedystancetype = "";
            if (spell.range.type != null)
            {
                rangetype = spell.range.type;
            }
            if (spell.range.distance != null)
            {
                rangedystance = spell.range.distance.amount;
                rangedystancetype = spell.range.type;
            }
            using (var ldb = new LiteDatabase("GlossData/Glossary.db"))
            {
                var spellss = ldb.GetCollection<SpellData0>();
                spellss.Insert(new SpellData0()
                {
                    name = spell.name, source = spell.source, level = spell.level, school = spell.school,
                    range = $"{rangedystance} {rangedystancetype}", s = spell.components.s, v = spell.components.v, m = m,
                    duration = $"{amount} {type}", concentration = concentration, entries = entries, time = $"{timenumber} {timeunit}",
                    scalingLevelDice = $" {sldlabel}\r\n{sld1}\r\n{sld5}\r\n{sld11}\r\n{sld17}",
                    damageInflict = damageinflict, savingThrow = saavingthrow, ritual = ritual, conditionInflict = conditioninflict,
                    affectsCreatureType = affectscreaturetype, damageResist = damageresist, spellAttack = spellattack, abilityCheck = abilitycheck
                });
            }
        }
    }
}