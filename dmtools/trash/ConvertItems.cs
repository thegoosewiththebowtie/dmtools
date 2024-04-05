using System;
using System.Collections.Generic;
using System.IO;
using dmtools.Views;
using GLib;
using LiteDB;
using Newtonsoft.Json;

namespace dmtools.Generators;

    public class ArmorClass
    {
        public int @base { get; set; }
        public bool dex_bonus { get; set; }
        public int? max_bonus { get; set; }
    }

    public class Content
    {
        public Item item { get; set; }
        public int quantity { get; set; }
    }

    public class Cost
    {
        public int quantity { get; set; }
        public string unit { get; set; }
    }

    public class Damage
    {
        public string damage_dice { get; set; }
        public DamageType damage_type { get; set; }
    }

    public class DamageType
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class EquipmentCategory
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class GearCategory
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Item
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Property
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Range
    {
        public int normal { get; set; }
        public int? @long { get; set; }
    }

    public class Root
    {
        public string index { get; set; }
        public string name { get; set; }
        public EquipmentCategory equipment_category { get; set; }
        public string weapon_category { get; set; }
        public string weapon_range { get; set; }
        public string category_range { get; set; }
        public Cost cost { get; set; }
        public Damage damage { get; set; }
        public Range range { get; set; }
        public double weight { get; set; }
        public List<Property> properties { get; set; }
        public string url { get; set; }
        public ThrowRange throw_range { get; set; }
        public TwoHandedDamage two_handed_damage { get; set; }
        public List<string> special { get; set; }
        public string armor_category { get; set; }
        public ArmorClass armor_class { get; set; }
        public int? str_minimum { get; set; }
        public bool? stealth_disadvantage { get; set; }
        public GearCategory gear_category { get; set; }
        public List<string> desc { get; set; }
        public int? quantity { get; set; }
        public List<Content> contents { get; set; }
        public string tool_category { get; set; }
        public string vehicle_category { get; set; }
        public Speed speed { get; set; }
        public string capacity { get; set; }
    }

    public class Speed
    {
        public double quantity { get; set; }
        public string unit { get; set; }
    }

    public class ThrowRange
    {
        public int normal { get; set; }
        public int @long { get; set; }
    }

    public class TwoHandedDamage
    {
        public string damage_dice { get; set; }
        public DamageType damage_type { get; set; }
    }
public class All
{
    public List<Root> all { get; set; }
}


public class ConvertItems
{
    public static void ConvertI ()
    {
        using (var ldb = new LiteDatabase("a/Equipment.db"))
        { 
            var huh = ldb.GetCollection<Equipment>();
            using StreamReader reader = new("a/EQ.json");
            var json = reader.ReadToEnd();
            All hehe = JsonConvert.DeserializeObject<All>(json);
            foreach (var robloxoof in hehe.all)
            {
                if (robloxoof.equipment_category == null)
                {
                    robloxoof.equipment_category = new EquipmentCategory()
                    {
                        name = String.Empty
                    };
                }
                if (robloxoof.cost == null)
                {
                    robloxoof.cost = new Cost()
                    { 
                        unit = String.Empty
                    };
                }

                if (robloxoof.damage == null)
                {
                    robloxoof.damage = new Damage()
                    {
                        damage_dice = String.Empty, damage_type = new DamageType()
                        {
                            name = String.Empty
                        }
                    };
                }
                if (robloxoof.range == null)
                {
                    robloxoof.range = new Range()
                    {
                        
                    };
                }
                if (robloxoof.properties == null)
                {
                    robloxoof.properties = new List<Property>()
                    {
                        new Property()
                        {
                            
                        }
                    };
                }
                List<string> props = new List<string>();
                foreach (var p in robloxoof.properties)
                {
                    props.Add(p.name);
                }

                if (robloxoof.throw_range == null)
                {
                    robloxoof.throw_range = new ThrowRange()
                    {

                    };
                }

                if (robloxoof.two_handed_damage == null)
                {
                    robloxoof.two_handed_damage = new TwoHandedDamage()
                    {
                        damage_dice = String.Empty, damage_type = new DamageType()
                        {
                            name = String.Empty
                        }
                    };
                }

                if (robloxoof.armor_class == null)
                {
                    robloxoof.armor_class = new ArmorClass()
                    {
                        
                    };
                }

                if (robloxoof.gear_category == null)
                {
                    robloxoof.gear_category = new GearCategory()
                    {
                        name = String.Empty
                    };
                }

                if (robloxoof.contents == null)
                {
                    robloxoof.contents = new List<Content>()
                    {
                        new Content()
                        {
                            item = new Item()
                            {
                                name = String.Empty
                            }
                        }
                    };
                }
                List<string> contitems = new List<string>();
                foreach (var p in robloxoof.contents)
                {
                    if (!string.IsNullOrWhiteSpace(p.item.name))
                    {
                        props.Add(p.item.name);
                    }
                }
                List<string> contams = new List<string>();
                foreach (var p in robloxoof.contents)
                {
                    if (p.quantity != null)
                    {
                        contams.Add(p.quantity.ToString());
                    }
                }

                if (robloxoof.speed == null)
                {
                    robloxoof.speed = new Speed()
                    {
                        unit = String.Empty
                    };
                }
                var sp = String.Empty;
                if (robloxoof.special != null)
                {
                    sp = string.Join("$", robloxoof.special);
                }
                var pr = String.Empty;
                if (props  != null)
                {
                    pr = string.Join("$", props);
                }
                var des = String.Empty;
                if (robloxoof.desc != null)
                {
                    des = string.Join("$", robloxoof.desc);
                }
                var ci = String.Empty;
                if (robloxoof.desc != null)
                {
                    ci = string.Join("$", contitems);
                }

                var ca = string.Empty;
                if (contams != null)
                {
                    ca = string.Join("$", contams);
                }
                huh.Insert(new Equipment()
                {
                    name = robloxoof.name, equipment_category = robloxoof.equipment_category.name, weapon_category = robloxoof.weapon_category,
                    weapon_range = robloxoof.weapon_range, category_range = robloxoof.category_range, cost_amount = robloxoof.cost.quantity.ToString(),
                    cost_coin = robloxoof.cost.unit, damage_dice = robloxoof.damage.damage_dice, damage_type = robloxoof.damage.damage_type.name,
                    range_normal = robloxoof.range.normal, range_long = robloxoof.range.@long, weight = robloxoof.weight,
                    properties = pr, ThrRanlong = robloxoof.throw_range.@long, ThrRannormal = robloxoof.throw_range.normal,
                    TwoHandedDamagedamage_dice = robloxoof.two_handed_damage.damage_dice, 
                    TwoHandedDamagedamage_type = robloxoof.two_handed_damage.damage_type.name, special = sp,
                    armor_category = robloxoof.armor_category, ArmorClassbase = robloxoof.armor_class.@base,
                    ArmorClassdex_bonus = robloxoof.armor_class.dex_bonus, ArmorClassmax_bonus = robloxoof.armor_class.max_bonus,
                    str_minimum = robloxoof.str_minimum, stealth_disadvantage = robloxoof.stealth_disadvantage,
                    gear_category = robloxoof.gear_category.name, desc = des, quantity = robloxoof.quantity,
                    contents_items = ci, contents_amountss = ca, 
                    tool_category = robloxoof.tool_category, vehicle_category = robloxoof.vehicle_category, speedquantity = robloxoof.speed.quantity.ToString(),
                    speedunit = robloxoof.speed.unit, capacity = robloxoof.capacity
                });
            }
            
        }
    }
}