/* Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);

using System.Collections.Generic;
using System.IO;
using dmtools.Views;
using DynamicData;
using LiteDB;
using Newtonsoft.Json;
using OpenGL;

namespace dmtools.GlossData; 
public class Ability
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Action
    {
        public string name { get; set; }
        public string multiattack_type { get; set; }
        public string desc { get; set; }
        public List<Action> actions { get; set; }
        public int? attack_bonus { get; set; }
        public Dc dc { get; set; }
        public List<Damage> damage { get; set; }
        public Usage usage { get; set; }
        public Option options { get; set; }
        public List<Attack> attacks { get; set; }
        public ActionOptions action_options { get; set; }
        public string action_name { get; set; }
        public object count { get; set; }
        public string type { get; set; }
    }

    public class ActionOptions
    {
        public int choose { get; set; }
        public string type { get; set; }
        public From from { get; set; }
    }

    public class Armor
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class ArmorClass
    {
        public string type { get; set; }
        public int value { get; set; }
        public Condition condition { get; set; }
        public Spell spell { get; set; }
        public List<Armor> armor { get; set; }
        public string desc { get; set; }
    }

    public class Attack
    {
        public string name { get; set; }
        public Dc dc { get; set; }
        public List<Damage> damage { get; set; }
    }

    public class Condition
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class ConditionImmunity
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Damage
    {
        public DamageType damage_type { get; set; }
        public string damage_dice { get; set; }
        public Dc dc { get; set; }
        public int? choose { get; set; }
        public string type { get; set; }
        public From from { get; set; }
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
        public int dc_value { get; set; }
        public string success_type { get; set; }
    }

    public class DcType
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Form
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class From
    {
        public string option_set_type { get; set; }
        public List<Option> options { get; set; }
    }

    public class Item
    {
        public string option_type { get; set; }
        public string action_name { get; set; }
        public int count { get; set; }
        public string type { get; set; }
        public string desc { get; set; }
    }

    public class LegendaryAction
    {
        public string name { get; set; }
        public string desc { get; set; }
        public int? attack_bonus { get; set; }
        public List<Damage> damage { get; set; }
        public Dc dc { get; set; }
    }

    public class Option
    {
        public string option_type { get; set; }
        public DamageType damage_type { get; set; }
        public string damage_dice { get; set; }
        public string notes { get; set; }
        public string name { get; set; }
        public Dc dc { get; set; }
        public List<Damage> damage { get; set; }
        public List<Item> items { get; set; }
        public string action_name { get; set; }
        public int? count { get; set; }
        public string type { get; set; }
        public string desc { get; set; }
        public int choose { get; set; }
        public From from { get; set; }
    }

    public class Proficiency
    {
        public int value { get; set; }
        public Proficiency2 proficiency { get; set; }
    }

    public class Proficiency2
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Reaction
    {
        public string name { get; set; }
        public string desc { get; set; }
        public Dc dc { get; set; }
    }

    public class Root
    {
        public string index { get; set; }
        public string name { get; set; }
        public string size { get; set; }
        public string type { get; set; }
        public string alignment { get; set; }
        public List<ArmorClass> armor_class { get; set; }
        public int hit_points { get; set; }
        public string hit_dice { get; set; }
        public string hit_points_roll { get; set; }
        public Speed speed { get; set; }
        public int strength { get; set; }
        public int dexterity { get; set; }
        public int constitution { get; set; }
        public int intelligence { get; set; }
        public int wisdom { get; set; }
        public int charisma { get; set; }
        public List<Proficiency> proficiencies { get; set; }
        public List<string> damage_vulnerabilities { get; set; }
        public List<string> damage_resistances { get; set; }
        public List<string> damage_immunities { get; set; }
        public List<ConditionImmunity> condition_immunities { get; set; }
        public Senses senses { get; set; }
        public string languages { get; set; }
        public double challenge_rating { get; set; }
        public int proficiency_bonus { get; set; }
        public int xp { get; set; }
        public List<SpecialAbility> special_abilities { get; set; }
        public List<Action> actions { get; set; }
        public List<LegendaryAction> legendary_actions { get; set; }
        public string image { get; set; }
        public string url { get; set; }
        public string desc { get; set; }
        public string subtype { get; set; }
        public List<Reaction> reactions { get; set; }
        public List<Form> forms { get; set; }
    }

    public class Senses
    {
        public string darkvision { get; set; }
        public int passive_perception { get; set; }
        public string blindsight { get; set; }
        public string truesight { get; set; }
        public string tremorsense { get; set; }
    }

    public class Slots
    {
        [JsonProperty("1")] public int _1 { get; set; }

        [JsonProperty("2")] public int? _2 { get; set; }

        [JsonProperty("3")] public int? _3 { get; set; }

        [JsonProperty("4")] public int? _4 { get; set; }

        [JsonProperty("5")] public int? _5 { get; set; }

        [JsonProperty("6")] public int? _6 { get; set; }

        [JsonProperty("7")] public int? _7 { get; set; }

        [JsonProperty("8")] public int? _8 { get; set; }

        [JsonProperty("9")] public int? _9 { get; set; }
    }

    public class SpecialAbility
    {
        public string name { get; set; }
        public string desc { get; set; }
        public Dc dc { get; set; }
        public Spellcasting spellcasting { get; set; }
        public Usage usage { get; set; }
        public List<Damage> damage { get; set; }
        public int? attack_bonus { get; set; }
    }

    public class Speed
    {
        public string walk { get; set; }
        public string swim { get; set; }
        public string fly { get; set; }
        public string burrow { get; set; }
        public string climb { get; set; }
        public bool? hover { get; set; }
    }

    public class Spell
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public int level { get; set; }
        public Usage usage { get; set; }
        public string notes { get; set; }
    }

    public class Spellcasting
    {
        public int level { get; set; }
        public Ability ability { get; set; }
        public int dc { get; set; }
        public int modifier { get; set; }
        public List<string> components_required { get; set; }
        public string school { get; set; }
        public Slots slots { get; set; }
        public List<Spell> spells { get; set; }
    }

    public class Usage
    {
        public string type { get; set; }
        public int? times { get; set; }
        public List<string> rest_types { get; set; }
        public string dice { get; set; }
        public int? min_value { get; set; }
    }

    public class All
    {
        public List<Root> all { get; set; }
    }

    public class convertMonsters
    {
        public static void StartConvertTest()
        {
            using StreamReader reader = new("a/m.json");
            var json = reader.ReadToEnd();
            All ress = JsonConvert.DeserializeObject<All>(json);
            using (var ldb = new LiteDatabase("GlossData/Bestiary.db"))
            {
                var bestiary = ldb.GetCollection<Bestiary>();
                foreach (var b0 in ress.all)
                {
                    List<string> l_ac_type = new List<string>();
                    List<string> l_ac_value = new List<string>();
                    List<string> l_ac_condition = new List<string>();
                    List<string> l_ac_spell = new List<string>();
                    List<string> l_ac_armor = new List<string>();
                    List<string> l_ac_desc = new List<string>();
                    if (b0.armor_class == null)
                    {
                        b0.armor_class = new List<ArmorClass>()
                        {
                            new ArmorClass()
                            {
                                type = "", armor = new List<Armor>()
                                {
                                    new Armor()
                                    {
                                        index = "", name = "", url = "",
                                    }
                                },
                                condition = new Condition()
                                {
                                    index = "", name = "", url = ""
                                },
                                desc = "", value = -99,
                                spell = new Spell()
                                {
                                    index = "", name = "", url = "", level = -99, notes = "",
                                    usage = new Usage()
                                    {
                                        min_value = -99, type = "", rest_types = new List<string>(){""},
                                        dice = ""
                                    }
                                }
                            }
                        };
                    }
                    foreach (var ac in b0.armor_class)
                    {
                        if (ac.type == null)
                        {
                            l_ac_type.Add("");
                        }
                        else
                        {
                            l_ac_type.Add(ac.type);

                        }                        
                        if (ac.value == null)
                        {
                            l_ac_value.Add("");
                        }
                        else
                        {
                            l_ac_value.Add(ac.value.ToString());
                        }                        
                        if (ac.condition == null)
                        {
                            l_ac_condition.Add("");
                        }
                        else
                        {
                            l_ac_condition.Add(ac.condition.name);

                        }                        
                        if (ac.spell == null)
                        {
                            l_ac_spell.Add("");
                        }
                        else
                        {
                            l_ac_spell.Add(ac.spell.name);
                        }                        
                        if (ac.armor == null)
                        {
                            l_ac_armor.Add("");
                        }
                        else
                        {
                            List<string> al = new List<string>();
                            foreach (var aca in ac.armor)
                            {
                                al.Add(aca.name);
                            }
                            l_ac_armor.Add(string.Join(", ", al));
                        }
                        if (ac.desc == null)
                        {
                            l_ac_desc.Add("");
                        }
                        else
                        {
                            l_ac_desc.Add(ac.desc);
                        }
                    }
                    if (b0.speed == null)
                    {
                        b0.speed = new Speed()
                            { walk = "", burrow = "", climb = "", fly = "", hover = false, swim = "" };
                    }
                    if (b0.speed.walk == null)
                    {
                        b0.speed.walk = "";
                    }
                    if (b0.speed.burrow == null)
                    {
                        b0.speed.burrow = "";
                    }
                    if (b0.speed.climb == null)
                    {
                        b0.speed.climb = "";
                    }
                    if (b0.speed.fly == null)
                    {
                        b0.speed.fly = "";
                    }
                    if (b0.speed.swim == null)
                    {
                        b0.speed.swim = "";
                    }
                    if (b0.speed.hover == null)
                    {
                        b0.speed.hover = false;
                    }
                    if (b0.proficiencies == null)
                    {
                        b0.proficiencies = new List<Proficiency>()
                        {
                            new Proficiency()
                            {
                                value = -99,
                                proficiency = new Proficiency2()
                                {
                                    index = "", name = "", url = ""
                                }
                            }
                        };
                    }
                    List<string> pr_v = new List<string>();
                    List<string> pr_n = new List<string>();
                    foreach (var p in b0.proficiencies)
                    {
                        if (p.value == null)
                        {
                            pr_v.Add("");
                        }
                        else
                        {
                            pr_v.Add(p.value.ToString());
                        }
                        if (p.proficiency == null)
                        {
                            p.proficiency = new Proficiency2() { index = "", name = "", url = "" };
                        }
                        if (p.proficiency.name == null)
                        {
                            pr_n.Add("");
                        }
                        else
                        {
                            pr_n.Add(p.proficiency.name);
                        }
                    }
                    if (b0.damage_vulnerabilities == null)
                    {
                        b0.damage_vulnerabilities = new List<string>() { "" };
                    }
                    if (b0.damage_resistances == null)
                    {
                        b0.damage_resistances = new List<string>() { "" };
                    }
                    if (b0.damage_immunities == null)
                    {
                        b0.damage_immunities = new List<string>() { "" };
                    }
                    if (b0.condition_immunities == null)
                    {
                        b0.condition_immunities = new List<ConditionImmunity>()
                        {
                            new ConditionImmunity(){index = "", name = "", url = ""}
                        };
                    }
                    List<string> ci = new List<string>();
                    foreach (var c in b0.condition_immunities)
                    {
                        if (c.name != null)
                        {
                            ci.Add(c.name);
                        }
                    }
                    if (b0.senses == null)
                    {
                        b0.senses = new Senses()
                        {
                            darkvision = "", blindsight = "", passive_perception = -99, tremorsense = "", truesight = ""
                        };
                    }
                    if (b0.senses.darkvision == null)
                    {
                        b0.senses.darkvision = "";
                    }
                    if (b0.senses.blindsight == null)
                    {
                        b0.senses.blindsight = "";
                    }
                    if (b0.senses.passive_perception == null)
                    {
                        b0.senses.passive_perception = -99;
                    }
                    if (b0.senses.tremorsense == null)
                    {
                        b0.senses.tremorsense = "";
                    }
                    if (b0.senses.truesight == null)
                    {
                        b0.senses.truesight = "";
                    }
                    if (b0.languages == null)
                    {
                        b0.languages = "";
                    }
                    if (b0.challenge_rating == null)
                    {
                        b0.challenge_rating = -99;
                    }
                    if (b0.proficiency_bonus == null)
                    {
                        b0.proficiency_bonus = -99;
                    }
                    if (b0.xp == null)
                    {
                        b0.xp = -99;
                    }
                    if (b0.special_abilities == null)
                    {
                        b0.special_abilities = new List<SpecialAbility>()
                        {
                            new SpecialAbility()
                            {
                                attack_bonus = -99,
                                damage = new List<Damage>()
                                {
                                    new Damage()
                                    {
                                        choose = -99,
                                        damage_dice = "",
                                        damage_type = new DamageType()
                                        {
                                            index = "", name = "", url = ""
                                        },
                                        type = "",
                                        dc = new Dc()
                                        {
                                            dc_value = -99,
                                            dc_type = new DcType()
                                            {
                                                index = "", name = "", url = ""
                                            }
                                        },
                                        from = new From()
                                        {
                                        }
                                    }
                                }, name = "", usage = new Usage()
                                {
                                    dice = "", type = "", rest_types = new List<string>(){""}, min_value = -99, times = -99
                                }, dc = new Dc()
                                {
                                    dc_value = -99, success_type = "", dc_type = new DcType()
                                    {
                                        index = "", name = "", url = ""
                                    }
                                }, desc = "", spellcasting = new Spellcasting()
                            }
                        };
                    }
                    List<string> sa_names = new List<string>();
                    List<string> sa_desc = new List<string>();
                    List<string> sa_u_type = new List<string>();
                    List<string> sa_u_dice = new List<string>();
                    List<string> sa_u_minv = new List<string>();
                    List<string> sa_u_tim = new List<string>();
                    foreach (var sa in b0.special_abilities)
                    {
                        if (sa.name == null)
                        {
                            sa_names.Add("");
                        }
                        else
                        {
                            sa_names.Add(sa.name);
                        }
                        if (sa.desc == null)
                        {
                            sa_desc.Add("");
                        }
                        else
                        {
                            sa_desc.Add(sa.desc);
                        }
                        if (sa.usage == null)
                        {
                            sa.usage = new Usage()
                            {
                                type = "", dice = ""
                            };
                        }
                        if (sa.usage.type == null)
                        {
                            sa_u_type.Add("");
                        }
                        else
                        {
                            sa_u_type.Add(sa.usage.type);
                        }
                        if (sa.usage.dice == null)
                        {
                            sa_u_dice.Add("");
                        }
                        else
                        {
                            sa_u_dice.Add(sa.usage.dice);
                        }
                        if (sa.usage.min_value == null)
                        {
                            sa_u_minv.Add("");
                        }
                        else
                        {
                            sa_u_minv.Add(sa.usage.min_value.ToString());
                        }
                        if (sa.usage.times == null)
                        {
                            sa_u_tim.Add("");
                        }
                        else
                        {
                            sa_u_tim.Add(sa.usage.times.ToString());
                        }
                    }

                    if (b0.actions == null)
                    {
                        b0.actions = new List<Action>()
                        {
                            new Action()
                            {
                                attack_bonus = -99, name = "", action_name = "",
                                count = "", usage = new Usage()
                                {
                                    dice = "", rest_types = new List<string>(){""}, type = "", min_value = -99, times = -99
                                }, desc = "", type = "", multiattack_type = ""
                            }
                        };
                    }
                    List<string> a_bon = new List<string>();
                    List<string> a_name = new List<string>();
                    List<string> a_desc = new List<string>();
                    List<string> au_type = new List<string>();
                    List<string> au_times = new List<string>();
                    List<string> au_dice = new List<string>();
                    List<string> au_minv = new List<string>();
                    foreach (var a in b0.actions)
                    {
                        if (a.attack_bonus == null)
                        {
                            a_bon.Add("");
                        }
                        else
                        {
                            a_bon.Add(a.attack_bonus.ToString());
                        }
                        if (a.name == null)
                        {
                            a_name.Add("");
                        }
                        else
                        {
                            a_name.Add(a.name.ToString());
                        }
                        if (a.desc == null)
                        {
                            a_desc.Add("");
                        }
                        else
                        {
                            a_desc.Add(a.desc.ToString());
                        }
                        if (a.usage == null)
                        {
                            a.usage = new Usage() { };
                        }
                        if (a.usage.type == null)
                        {
                            au_type.Add("");
                        }
                        else
                        {
                            au_type.Add(a.usage.type.ToString());
                        }
                        if (a.usage.times == null)
                        {
                            au_times.Add("");
                        }
                        else
                        {
                            au_times.Add(a.usage.times.ToString());
                        }
                        if (a.usage.dice == null)
                        {
                            au_dice.Add("");
                        }
                        else
                        {
                            au_dice.Add(a.usage.dice.ToString());
                        }
                        if (a.usage.min_value == null)
                        {
                            au_minv.Add("");
                        }
                        else
                        {
                            au_minv.Add(a.usage.min_value.ToString());
                        }
                    }

                    if (b0.legendary_actions == null)
                    {
                        b0.legendary_actions = new List<LegendaryAction>()
                        {
                            new LegendaryAction()
                            {
                                attack_bonus = -99, damage = new List<Damage>()
                                {
                                    new Damage()
                                    {
                                        damage_dice = ""
                                    }
                                }, name = "", desc = ""
                            }
                        };
                    }
                    List<string> legnames = new List<string>();
                    List<string> legdesc = new List<string>();
                    foreach (var la in b0.legendary_actions)
                    {
                        if (la.name == null)
                        {
                            legnames.Add("");
                        }
                        else
                        {
                            legnames.Add(la.name);
                        }
                        if (la.desc == null)
                        {
                            legdesc.Add("");
                        }
                        else
                        {
                            legdesc.Add(la.desc);   
                        }
                    }
                    if (b0.reactions == null)
                    {
                        b0.reactions = new List<Reaction>()
                        {
                            new Reaction()
                            {
                                desc = "", name = ""
                            }
                        };
                    }
                    List<string> ren = new List<string>();
                    List<string> red = new List<string>();
                    foreach (var r in b0.reactions)
                    {
                        if (r.name == null)
                        {
                            ren.Add("");
                        }
                        else
                        {
                            ren.Add(r.name);
                        }
                        if (r.desc == null)
                        {
                            red.Add("");
                        }
                        else
                        {
                            red.Add(r.desc);
                        }
                    }
                    if (b0.forms == null)
                    {
                        b0.forms = new List<Form>()
                        {
                            new Form()
                            {
                                index = "", name = "", url = ""
                            }
                        };
                    }
                    List<string> formsl = new List<string>();
                    foreach (var f in b0.forms)
                    {
                        if (f != null)
                        {
                            formsl.Add(f.name);
                        }
                        else
                        {
                            formsl.Add("");
                        }
                    }
                    bestiary.Insert(new Bestiary()
                    {
                        name = b0.name, size = b0.size, type = b0.type, alignment = b0.alignment,
                        ac_type = string.Join("$", l_ac_type), ac_value = string.Join("$", l_ac_value),
                        ac_condition = string.Join("$", l_ac_condition), ac_spell = string.Join("$", l_ac_spell),
                        ac_armor = string.Join("$", l_ac_armor), ac_desc = string.Join("$", l_ac_desc),
                        hit_points = b0.hit_points, hit_dice = b0.hit_dice, hit_points_roll = b0.hit_points_roll,
                        speed_walk = b0.speed.walk, speed_swim = b0.speed.swim, speed_fly = b0.speed.fly,
                        speed_burrow = b0.speed.burrow, speed_climb = b0.speed.climb, speed_hover = b0.speed.hover,
                        strength = b0.strength, dexterity = b0.dexterity, constitution = b0.constitution,
                        intelligence = b0.intelligence, wisdom = b0.wisdom, charisma = b0.charisma,
                        prof_name = string.Join("$", pr_n), prof_val = string.Join("$", pr_v), 
                        damage_vulnerabilities = string.Join(", ", b0.damage_vulnerabilities),
                        damage_resistances = string.Join(", ", b0.damage_resistances),
                        damage_immunities = string.Join(", ", b0.damage_immunities),
                        condition_immunities = string.Join(", ", ci), darkvision = b0.senses.darkvision,
                        passive_perception = b0.senses.passive_perception, blindsight = b0.senses.blindsight,
                        truesight = b0.senses.truesight, tremorsense = b0.senses.tremorsense, 
                        languages = b0.languages, challenge_rating = b0.challenge_rating, proficiency_bonus = b0.proficiency_bonus,
                        xp = b0.xp, sa_name = string.Join("$", sa_names), sa_desc = string.Join("$", sa_desc),
                        u_type = string.Join("$", sa_u_type), u_times = string.Join("$", sa_u_tim),
                        u_dice = string.Join("$", sa_u_dice), u_min_value = string.Join("$", sa_u_minv),
                        attack_bonus = string.Join("$", a_bon), a_names = string.Join("$", a_name),
                        a_desc = string.Join("$", a_desc), a_u_type = string.Join("$", au_type),
                        a_u_times = string.Join("$", au_times), a_u_dice = string.Join("$", au_dice),
                        a_u_min_value = string.Join("$", au_minv), la_name = string.Join("$", legnames),
                        la_desc = string.Join("$", legdesc), desc = b0.desc, subtype = b0.subtype,
                        r_names = string.Join("$", ren), r_descs = string.Join("$", red), forms = string.Join(", ", formsl)
                        
                    });
                }
            }
        }
    }*/