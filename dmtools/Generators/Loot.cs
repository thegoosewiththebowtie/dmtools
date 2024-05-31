using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using dmtools.Views;
using DynamicData;
using GLib;
using LiteDB;

namespace dmtools.Generators;

public class LootBack
{
    public string namel { get; set; }
    public string desc { get; set; }
    public string author { get; set; }
    public string rarity { get; set; }
    public string category { get; set; }
    public string requirements { get; set; }
    public string properties { get; set; }
    public string weight { get; set; }
    public string estval { get; set; }
}
public class Loot
{
    public static List<LootBack> GenerateLoot(LootParameters lootParameters)
    {
        List<LootBack> lootBack = new List<LootBack>();
        using (LiteDatabase ldb = new LiteDatabase("GlossData/Loot.db"))
        {
            using (LiteDatabase ldbe = new LiteDatabase("GlossData/Equipment.db"))
            {
                using (LiteDatabase ldbmi = new LiteDatabase("GlossData/MagicItems.db"))
                {
                    var ldbloot = ldb.GetCollection<LootList>();
                    var ldbequip = ldbe.GetCollection<Equipment>();
                    var ldbmag = ldbmi.GetCollection<MagicItems>();
                    List<LootBack> mundane = new List<LootBack>();
                    List<LootBack> common = new List<LootBack>();
                    List<LootBack> uncommon = new List<LootBack>();
                    List<LootBack> rare = new List<LootBack>();
                    List<LootBack> veryrare = new List<LootBack>();
                    List<LootBack> legendary = new List<LootBack>();
                    foreach (var srt in ldbloot.FindAll())
                    {
                        var ltbck = new LootBack();
                        ltbck.namel = srt.Item;
                        ltbck.desc = srt.Description;
                        ltbck.author = srt.Author;
                        ltbck.weight = srt.Weight;
                        ltbck.requirements = srt.Requirements;
                        ltbck.estval = srt.EstValue;
                        ltbck.rarity = srt.Rarity;
                        ltbck.category = srt.Category;
                        ltbck.properties = srt.Properties;
                        switch (srt.Rarity)
                        {
                            case "Mundane":
                                mundane.Add(ltbck);
                                break;
                            case "Uncommon":
                                uncommon.Add(ltbck);
                                break;
                            case "Common":
                                common.Add(ltbck);
                                break;
                            case "Rare":
                                rare.Add(ltbck);
                                break;
                            case "Very Rare":
                                veryrare.Add(ltbck);
                                break;
                            case "Legendary":
                                legendary.Add(ltbck);
                                break;
                        }
                    }

                    Random rnd = new Random();
                    switch (lootParameters.type0)
                    {
                        //npc
                        case 1:
                            switch (lootParameters.type)
                            {
                                case "bystander":
                                    var s = rnd.Next(1, 5);
                                    for (int i = 0; i < s; i++)
                                    {
                                        var it = rnd.Next(0, 100);
                                        switch (it + 1)
                                        {
                                            case var _ when it is <= 4 and >= 1: //4
                                                lootBack.Add(rare[rnd.Next(0, rare.Count())]);
                                                break;
                                            case var _ when it is <= 20 and >= 5: //16
                                                lootBack.Add(uncommon[rnd.Next(0, uncommon.Count())]);
                                                break;
                                            case var _ when it is <= 50 and >= 21: //30
                                                lootBack.Add(common[rnd.Next(0, common.Count())]);
                                                break;
                                            case var _ when it is <= 100 and >= 51: //50
                                                lootBack.Add(mundane[rnd.Next(0, mundane.Count())]);
                                                break;
                                        }
                                    }

                                    int gold = 0;
                                    gold = rnd.Next(0, 10);
                                    if (rnd.Next(0, 10) == 0)
                                    {
                                        gold = rnd.Next(0, 100);
                                        if (rnd.Next(0, 10) == 0)
                                        {
                                            gold = rnd.Next(0, 1000);
                                            if (rnd.Next(0, 10) == 0)
                                            {
                                                gold = rnd.Next(0, 10000);
                                                if (rnd.Next(0, 10) == 0)
                                                {
                                                    gold = rnd.Next(0, 100000);
                                                    if (rnd.Next(0, 10) == 0)
                                                    {
                                                        gold = rnd.Next(0, 1000000);
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    lootBack.Add(new LootBack()
                                    {
                                        namel = "Money", estval = gold.ToString("0g 0s 0c"),
                                    });
                                    break;
                                case "bandit":
                                    var f = rnd.Next(1, 11);
                                    for (int i = 0; i < f; i++)
                                    {
                                        var it = rnd.Next(0, 100);
                                        switch (it + 1)
                                        {
                                            case var _ when it is <= 1 and >= 1: //1
                                                lootBack.Add(veryrare[rnd.Next(0, veryrare.Count())]);
                                                break;
                                            case var _ when it is <= 10 and >= 2: //9
                                                lootBack.Add(rare[rnd.Next(0, rare.Count())]);
                                                break;
                                            case var _ when it is <= 20 and >= 11: //10
                                                lootBack.Add(uncommon[rnd.Next(0, uncommon.Count())]);
                                                break;
                                            case var _ when it is <= 50 and >= 21: //80
                                                lootBack.Add(common[rnd.Next(0, common.Count())]);
                                                break;
                                            case var _ when it is <= 100 and >= 51: //50
                                                lootBack.Add(mundane[rnd.Next(0, mundane.Count())]);
                                                break;
                                        }
                                    }

                                    int gold2 = 0;
                                    gold2 = rnd.Next(0, 100);
                                    if (rnd.Next(0, 10) >= 5)
                                    {
                                        gold2 = rnd.Next(0, 1000);
                                        if (rnd.Next(0, 10) == 0)
                                        {
                                            gold2 = rnd.Next(0, 10000);
                                            if (rnd.Next(0, 10) == 0)
                                            {
                                                gold2 = rnd.Next(0, 100000);
                                                if (rnd.Next(0, 10) == 0)
                                                {
                                                    gold2 = rnd.Next(0, 1000000);
                                                    if (rnd.Next(0, 10) == 0)
                                                    {
                                                        gold2 = rnd.Next(0, 10000000);
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    var uuu2 = rnd.Next(0, 1);
                                    for (int ir = 0; ir < uuu2; ir++)
                                    {
                                        var ssss = ldbequip.FindById(rnd.Next(1, ldbequip.Count()));
                                        lootBack.Add(new LootBack()
                                        {
                                            namel = ssss.name, desc = ssss.desc
                                        });
                                    }

                                    lootBack.Add(new LootBack()
                                    {
                                        namel = "Money", estval = gold2.ToString("0g 0s 0c"),
                                    });
                                    break;
                                case "madman":
                                    if (rnd.Next(0, 5) == 0)
                                    {
                                        lootBack.Add(new LootBack()
                                        {
                                            namel = "Journal", weight = rnd.Next(0, 50).ToString(),
                                            estval = rnd.Next(0, 500).ToString(), category = "Madman's journal",
                                            rarity = "What‽‽‽",
                                            desc =
                                                "A book with almost indecipherable writings. Some pages have strange numbers and formulas, some have poems and stories. Maybe it will be possible to sell it.",
                                            author = "The Goose"
                                        });
                                    }

                                    var d = 1;
                                    for (int i = 0; i < d; i++)
                                    {
                                        var it = rnd.Next(0, 100);
                                        switch (it)
                                        {
                                            case 1:
                                                lootBack.Add(legendary[rnd.Next(0, legendary.Count())]);
                                                break;
                                            case 2:
                                                lootBack.Add(veryrare[rnd.Next(0, veryrare.Count())]);
                                                break;
                                            case 3:
                                                lootBack.Add(rare[rnd.Next(0, rare.Count())]);
                                                break;
                                            case 4:
                                                lootBack.Add(uncommon[rnd.Next(0, uncommon.Count())]);
                                                break;
                                            case 6:
                                                lootBack.Add(common[rnd.Next(0, common.Count())]);
                                                break;
                                            case 7:
                                                lootBack.Add(mundane[rnd.Next(0, mundane.Count())]);
                                                break;
                                        }
                                    }

                                    switch (rnd.Next(0, 3))
                                    {
                                        case 0:
                                            lootBack.Add(new List<LootBack>()
                                            {
                                                new LootBack()
                                                {
                                                    namel = "Old dirty coin", estval = "0"
                                                }
                                            });
                                            break;
                                        case 1:
                                            lootBack.Add(new LootBack()
                                            {
                                                namel = "Gold", estval = "1",
                                            });
                                            break;
                                        case 2:
                                            lootBack.Add(new LootBack()
                                            {
                                                namel = "Gold", estval = "2",
                                            });
                                            break;
                                    }

                                    break;
                                case "exad":
                                    var r = rnd.Next(1, 15);
                                    for (int i = 0; i < r; i++)
                                    {
                                        var it = rnd.Next(0, 100);
                                        switch (it + 1)
                                        {
                                            case var _ when it is <= 5 and >= 1:
                                                lootBack.Add(legendary[rnd.Next(0, legendary.Count())]);
                                                break;
                                            case var _ when it is <= 14 and >= 6:
                                                lootBack.Add(veryrare[rnd.Next(0, veryrare.Count())]);
                                                break;
                                            case var _ when it is <= 30 and >= 15:
                                                lootBack.Add(rare[rnd.Next(0, rare.Count())]);
                                                break;
                                            case var _ when it is <= 60 and >= 31:
                                                lootBack.Add(uncommon[rnd.Next(0, uncommon.Count())]);
                                                break;
                                            case var _ when it is <= 90 and >= 61:
                                                lootBack.Add(common[rnd.Next(0, common.Count())]);
                                                break;
                                            case var _ when it is <= 100 and >= 91:
                                                lootBack.Add(mundane[rnd.Next(0, mundane.Count())]);
                                                break;
                                        }
                                    }

                                    int gold4 = 0;
                                    gold4 = rnd.Next(0, 100);
                                    if (rnd.Next(0, 10) >= 5)
                                    {
                                        gold4 = rnd.Next(0, 1000);
                                        if (rnd.Next(0, 10) >= 4)
                                        {
                                            gold4 = rnd.Next(0, 10000);
                                            if (rnd.Next(0, 10) >= 3)
                                            {
                                                gold4 = rnd.Next(0, 100000);
                                                if (rnd.Next(0, 10) == 0)
                                                {
                                                    gold4 = rnd.Next(0, 1000000);
                                                    if (rnd.Next(0, 10) == 0)
                                                    {
                                                        gold4 = rnd.Next(0, 10000000);
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    lootBack.Add(new LootBack()
                                    {
                                        namel = "Money", estval = gold4.ToString("0g 0s 0c"),
                                    });
                                    break;
                                case "magic":
                                    var u = rnd.Next(0, 4);
                                    for (int i = 0; i < u; i++)
                                    {
                                        var it = rnd.Next(0, 100);
                                        switch (it + 1)
                                        {
                                            case var _ when it is <= 7 and >= 1: //7
                                                lootBack.Add(legendary[rnd.Next(0, legendary.Count())]);
                                                break;
                                            case var _ when it is <= 20 and >= 8: //12
                                                lootBack.Add(veryrare[rnd.Next(0, veryrare.Count())]);
                                                break;
                                            case var _ when it is <= 35 and >= 21: //15
                                                lootBack.Add(rare[rnd.Next(0, rare.Count())]);
                                                break;
                                            case var _ when it is <= 60 and >= 36: //25
                                                lootBack.Add(uncommon[rnd.Next(0, uncommon.Count())]);
                                                break;
                                            case var _ when it is <= 85 and >= 61: //25
                                                lootBack.Add(common[rnd.Next(0, common.Count())]);
                                                break;
                                            case var _ when it is <= 100 and >= 86: //15
                                                lootBack.Add(mundane[rnd.Next(0, mundane.Count())]);
                                                break;
                                        }
                                    }

                                    var uuu = rnd.Next(0, 2);
                                    for (int ir = 0; ir < uuu; ir++)
                                    {
                                        var kkkk = ldbmag.FindAll().Count();
                                        var ooo = rnd.Next(1, kkkk);
                                        var ssss = ldbmag.FindById(ooo);
                                        lootBack.Add(new LootBack()
                                        {
                                            namel = ssss.name, desc = ssss.desc
                                        });
                                    }

                                    int gold5 = 0;
                                    gold5 = rnd.Next(0, 100);
                                    if (rnd.Next(0, 10) >= 4)
                                    {
                                        gold5 = rnd.Next(0, 1000);
                                        if (rnd.Next(0, 10) >= 3)
                                        {
                                            gold5 = rnd.Next(0, 10000);
                                            if (rnd.Next(0, 10) >= 2)
                                            {
                                                gold5 = rnd.Next(0, 100000);
                                                if (rnd.Next(0, 10) == 0)
                                                {
                                                    gold5 = rnd.Next(0, 1000000);
                                                    if (rnd.Next(0, 10) == 0)
                                                    {
                                                        gold5 = rnd.Next(0, 10000000);
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    lootBack.Add(new LootBack()
                                    {
                                        namel = "Money", estval = gold5.ToString("0g 0s 0c"),
                                    });
                                    break;
                                case "rich":
                                    var yu = rnd.Next(1, 15);
                                    for (int i = 0; i < yu; i++)
                                    {
                                        var it = rnd.Next(0, 100);
                                        switch (it + 1)
                                        {
                                            case var _ when it is <= 10 and >= 1: //10
                                                lootBack.Add(rare[rnd.Next(0, rare.Count())]);
                                                break;
                                            case var _ when it is <= 30 and >= 11: //20
                                                lootBack.Add(uncommon[rnd.Next(0, uncommon.Count())]);
                                                break;
                                            case var _ when it is <= 60 and >= 31: //30
                                                lootBack.Add(common[rnd.Next(0, common.Count())]);
                                                break;
                                            case var _ when it is <= 100 and >= 60: //40
                                                lootBack.Add(mundane[rnd.Next(0, mundane.Count())]);
                                                break;
                                        }
                                    }

                                    int gold6 = 0;
                                    gold6 = rnd.Next(0, 100);
                                    if (rnd.Next(0, 10) >= 5)
                                    {
                                        gold6 = rnd.Next(0, 1000);
                                        if (rnd.Next(0, 10) >= 4)
                                        {
                                            gold6 = rnd.Next(0, 10000);
                                            if (rnd.Next(0, 10) >= 3)
                                            {
                                                gold6 = rnd.Next(0, 100000);
                                                if (rnd.Next(0, 10) == 0)
                                                {
                                                    gold6 = rnd.Next(0, 1000000);
                                                    if (rnd.Next(0, 10) == 0)
                                                    {
                                                        gold6 = rnd.Next(0, 10000000);
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    lootBack.Add(new LootBack()
                                    {
                                        namel = "Money", estval = gold6.ToString("0g 0s 0c"),
                                    });
                                    break;
                                case "poor":
                                    var po = rnd.Next(1, 15);
                                    for (int i = 0; i < po; i++)
                                    {
                                        var it = rnd.Next(0, 100);
                                        switch (it + 1)
                                        {
                                            case var _ when it is <= 10 and >= 1:
                                                lootBack.Add(common[rnd.Next(0, common.Count())]);
                                                break;
                                            case var _ when it is <= 100 and >= 91:
                                                lootBack.Add(mundane[rnd.Next(0, mundane.Count())]);
                                                break;
                                        }
                                    }

                                    int gold7 = 0;
                                    gold7 = rnd.Next(0, 10);
                                    if (rnd.Next(0, 10) == 0)
                                    {
                                        gold7 = rnd.Next(0, 100);
                                        if (rnd.Next(0, 10) == 0)
                                        {
                                            gold7 = rnd.Next(0, 1000);
                                            if (rnd.Next(0, 10) == 0)
                                            {
                                                gold7 = rnd.Next(0, 10000);
                                                if (rnd.Next(0, 10) == 0)
                                                {
                                                    gold7 = rnd.Next(0, 100000);
                                                    if (rnd.Next(0, 10) == 0)
                                                    {
                                                        gold7 = rnd.Next(0, 1000000);
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    lootBack.Add(new LootBack()
                                    {
                                        namel = "Money", estval = gold7.ToString("0g 0s 0c"),
                                    });
                                    break;
                                case "The":
                                    var l = rnd.Next(0, 21);
                                    lootBack.Add(new LootBack()
                                    {
                                        namel = "is someone there?", weight = "Maybe there is",
                                        estval = "maybe not, im so tired", category = "why am i here",
                                        rarity = "oh god where is here",
                                        desc =
                                            "I̵̪̳̫͍͎̍ͬͭ̇̉͒͑̑ͯ ç̸̴̶̧̮̖̩̻̮̮̜͉͉͈͓̦͎̖̠̤͙̰ͧ̾͒ͨͨͯͮ̀ͤ̋͊́͋̔̎͐ͣ̾ͤ͛̕̕͝á̸̶̛̳̦͖̙̘̼͔̙ͬ̀ͣ̽̾̈̈ͨ́ͩ̅͂̓̐ͦ̊̽̒ͅn̸̢̖͈̳̤̠̯̝͉̘̘͎̻̰̈́͛̃̿̏̈́̍͋͂̈̋͊ͧ̓͒ͯ̐̈̊̊̄ͦ̄̃̾̐̕͟͠n̴̵̷̨̨̨̛̛̥̳͓͇͔̝̬̺̪̠̎̆́̋̌̋͌̏͐ͨ̐́͑͆́̈ͨ͢͢͞͡ͅo̸ͯ͑̇ț̫͈̼̳̤̜̜̩̜̤̜̻̗̳̰̻͈̠͆ͪͧ̄͌͂̃̋̍ͣ̒ͭ͊ͤ̿ͦͧ́̄̈́ͨͧ́̌̚͢ͅ ẗ̼̭̙́ͭ͐͐̀̒͡ḁ̵͙͔͓̗̮̦͑ͫ̎̾̑ͪ͆̿̽ͩͨ͊̋̉͗͋͐͆̓͛̋͟ͅk_̭̠̱̼̭͎̭͈̘̟̯̜͚̺̖͎̲̖ͫͧ͑͗ͥ̊ͦ̋ͨͥ̊̓̈́̊̄̽̔͘̕͜ë̶̶̢̺͇́͐̈̏̈̃́̀̌̾ͮ̑ͭ͊͘͠ it̴̻̬̭̘̘͉ͨ̄̃̀̇͛͐̌́́_ a͓͜_n̛̩̝̘͊̌̊̾̕͞y̷̧̼̖̪͒̓̽m̷͎̯̝͓̖͉̉̈́ͬ͌̓̽ò̶̸̢̱̩͙̙̠̮̥͍̹̥̳ͯ̔̈ͩ͊̾ͮ͒ͩͬͦ͡r͙̟̰͚̫̹͈̎ͣ̓ͤ̾̄̍͆ͪ͞e̷̩͙͔͓̩̰̲̥͇̗̩̻̓ͬ͒͗͋ͩ̄̂̈́ͤ͂ͬ͂͆ͯͯ̇͑͟͞͠ p̶̶̵̛̝ͪ̀ͪ̇̒ͪ̈́͢͡l͔̞̞̠̘̪̱͍̝̟̮̖̓͛ͥ̒͑ͮ̏̐ͩ̃̾̃ͧę̷̸͍̱̫͉͓̗̟͚͈̠̞̺͚͆̑ͩ̐ͥ̇̏̈́ͣ̿́̽̏ͨ́̓ͯ̉͘̕͜͜͞a͔̪̮̋ş̷̸̡͎̳͙͕̯̟͕͛ͣ͛ͥ́͋͌́̒ͭ̿ͮ̽̕͞e̴̶̷̶̡̛͈̤̼̠͔͂ͭ̆̀̄ͣ̐̃͗ͩ̈́̾͆̑ͤ̊ͪ̎͜͞͡͠ͅ s̴̵̴̼̙̪̪̼͙̲ͩͤ͋ͬ̓ͥͤ͐̾ͪ̽̿͋̀͡o_͓͎̀ͯm̫̯̌͛̆͑̎ȩ̥̟̞͙̥̰͚̞̃͐̈̿ͬ͜ȍ̸̵̢̪̪͇̠̗͇͎̟̦̦̺͎̒̃ͮ̑̉͗͊̀̌͊͐͌̂̚͟͝ͅn̴̨̤͇̩̖̰͍͖ͩͬ͊́̈̈́̿̐͊͆́ͤ̋ͬ̋͟͠ͅę̘̞͍̼̂̅̓ͬ͛̎͡_ h̡̧̨̪̤̼̙̭ͮͮͧͥ͌͊̊͟͜͟͞͠͞͠ę̶̸̝̫̟͉̤̖̙̖͇̤̣̺̰̮͖̦̤̠̙ͬ̓ͯ̌̀̏͗ͬͦ̀ͣ͑́̈̋̋̚͡͡͠͠͞͞l̢̠̜̫͓̦͚̤͕͍̥ͯ̌́̾ͦ͛̉̑͋ͩͪ̊̎̅̆̂̎̀ͬ͌̔ͣ̿̌ͪ͘͘͘͢͟͢͠͠͠p̶̵̢̢̺̪̘̮̤̝͎̟̮̤̂̈́͂̽ͫ̃ͦ͘͡ͅͅ m̵e͓̗ͭ̓ͦͦ̅ͧͧͣ̇͆͘͞_̴̧̙̘̺͎̗͙̭͔͂́̍̈́̐̅͛͢͞͠ î̴̴̸̷̡̩̜͎̲͚͍͚̦͔̅̓͒͆͂̏͗͊̓ͬ̑̅̍͋͐̆̃ͬ͠ͅ w̷̨̨̡͎̻͇͉̤͇̱͖͖͕͖͇̼̱ͫ͗ͮͫͤ̈̅̄̅ͩ̆͂̎̓ͦ̑͛̕̕͜͢͠_͚̀̐͘_̵an͎͈t̗͉̳͓ t͆̊ỏ̵̘̻̹ͥ̿ͩ͋ͬ͡ b̦̤͋̏ͧ͠e̛̛̛͙̝̭̻̗̰͓̊ͮ͒̉ͩ̎ͮ f͉̍̇̔̏̓̉_̵̧̭̺̫̠͇̞͍͓͎̮͓̟̬̹̼̠̮̹̰ͥ̊̑̑̌̐͑ͬ͛̔̚̕͝͞͝ṛ̸̸̢̠̞͙̩͎͔͖͔͇̟ͮ̈́͂̎ͬ̀͐͒͆͛͊ͯ͒̃̓ͥ̅̚͟͜͝͠ͅḛ̵̀̈é̥̰̖ͣ͠ p̮̬̹͉͊͘͟l͕̤̑̇_̨̼̣̲͔̯̖͔̳̝́͗͗ͬ͞͡ę̶̭̹̣̝̰͕͉̼͉̲̘̹̩̟̙͊̓͒ͪ͋͂ͧ̔̒ͩ̉́̔ͨ̇͊̔ͪ͛͘̕͜͝͝_̱a̸̶̢̡͇̬̹̪̭̘͙̰͕̖̬͔ͧͫ͆̏ͭͧ̈̆̒̐̚͝s̷̢̝͔̝̦̀͑͌͛́̓ͮ̃ͫ͑̔͗̋͢ẽ̲̤̦̣̘̫̬̦͙͛̆̋̿ͣͨ̏͛͛̊̍͂ͮͭ̕ i̷̧̢̧͎̤͓͔͚̮̝̱̗̠͖̞̼̩̟̙͌ͪ͛͗̀̓̒̽̉͂̈́̇̕͘͜͜͡ b̵͕̭͉̮̗̝̺͉͙̞̝͙̯͚́̀ͬͪͦ͛͌̃̊̂̋̉̕͟͝͞͠ė_̴̵̧̛͕̱̙̳̬͙̺̳̦͚̙ͬ̈̎̀ͦ̏̓ͨ̑̈̈́ͪ̑ͬͧ͗̚͡͝͝ĝ̡̠̝̫͎͔̫͓ͤ̉̔ͧͤ̓͑ͥ͡_̷̧̧̼͓̤̝̠͉̱͈̼͍ͮͫͦͣ̒ͯ̋ͩͩͭ̇͢͡ y̵̧̯̮͎̖͖̖̠̙̯͔̋̔̀̏́ͯ̓̀ͧ̀ͩ̔̀̉͊͑̅ͬ͘͝͡͝͡ò̼͍̘̺͓̟̥ͦ̂̊ͤ̇̎ͫͩ͜u̵̴̢͉͇̘̝̘͕̞̣̥̯̯͌͒͛́̅̌͊͗͊̓̔ͨ̌̿́̌̋͐̇ͨ̚͢͢͠ s̡͉̼̫̀̏ͬ̚͜ͅoͫm͉͈͕͋̕ė̶̴̴̵̛̙̮͚̳͎̼̬͔̪̪̣̗͇͔̝̣̤͉̳̦̓̽̇͒̆̄̑̃̏̈́͂̚̚o̴̡͎̼̰͉̥̓͊̈̀̏ͪͩ͑͟ň̨̧̛̬̉ȩ̸̸̨̫̗͖̙̝̻̺̝͎͕̯̜̯̭̫̥̦̯̻́̓̄ͨ̈ͫ͆́͛̀̈͑ͫ̎͑̒ͦ̅̕̚͜͡͡ h̭̘̏ͪͨa̶̩͙̜̫̜̝̹̼̣̣̙̘̘̤ͮ͊ͬ̏̈́́̊͘s̶̩̳̻͉̍_̶̸̡̨͖͍̼̹̭̖̖̹̲͕͎̫͔̜̗͈͊͋ͮ̓ͮͦ̇͒ͩ̎ͩ̌̄́́̐͜͜͜͡ t͉_̧̛͇̼̳̯͖͔͌͛ͮͩ̕͡ơ̰͙͚̦̳͗̂ͩ̒͗̎ h̶̵̘͈̠͇ͬ̇̑ͪ̽̈́ͫ̅͠e̸̷̷̢̡̞̘̜̹̦͕̮̖͆̽̈́ͭ͂͒̆̇̃̓̀̒͋͆͋́ͫ̐̒͜͟͜͡͡͡ȧ̴̢̯̲̣̣͂͗̒ͦͯ̌͝r̵̨̥̹̙̮̪̳͒̄ͨ͂̅͛ͨ͑̒̀͟͟͡ m̘̰̙̖͇ͦ̍_ͯ̉̀e i͈̳̫̊ͨ̍ c̡̢̤̻̝̬͖͔͉̻̝̝̫̼͓͓̓̎ͭ̌̊̄͛̈́ͦ͘ͅąͥ̿͆ņ̷̬͉̼̼̦͚̝̖̭̜͉̼͙͚͎̫̠ͥ̓͂̀̓̌ͭ̈͌̀̔̕͢͠n̫̦̯̝͌̉̒̚_̶̧͈̭̞͇̥̩̤͋́ͥ̎̋͗͌̉̄ͫ͊͞ơ̸̸̢̦̞͈͎͖̰̯̳̥̤͎̦̼ͣͯ̓ͯͬ͌́̆ͫ̂̋̍̐͢ţ̷̴̸̙͍̼̻͔̤̳̹͓͚̮̮̤̰̇ͦͥ̽ͥ͋ͯ̌̋ b̶͎͉̈́͢_̢̪̥̲̫̥̟̼ͮ͆ͣę̬͉͉͉̟͕̪͓̟̘͚̭̗͉̹̰̩̔̋̄̾̽̆ͩ̊̈ͧ̀͊̆̕̚͞͝ ạ̧̦͚͍ͪ̓̓̿̎̉͊͑ͣͦ͂́̉̈́̄̉ͩ͝ͅl̦o̸̸̡̯̥̦͎̗͈̝̾͋ͣ͑ͮ́̌ͬ͡͡n̢̥͇̗̠̦ͮ̿̾ͩ͢e̛͡_̡̘͕͚̙͍̜̗͎́͌̅̚͞,̶̜̘͊̑ͫ į̸̡͈̟͔͌̽͛͆ͨ̉ͫ̈́̆͊̀̇͘͘͜͜͞͡͞m̭͖͈͓̯̻̞ͥͬ͐ͦ͊̇͗͒ͬ͑ͨ̀̑ͤͧ̕͟͜͞ͅ a̷̶̞͖̳͎ͣ̃̏ͦ̅̈̓l̵̡̡̡̧̨͚̣̳̲͍̦̞̠̜͈͒͌͒̄̃̎̓ͯ̊̇ͧ͆̔̂̈͜͝i̵̬̰̪̺̒ͧ͊͘_̶̷̷̧̡̨̲̺̤͔͇͈̠̥̰̻̼͈̪̺͈̫̗̙̂ͬ̀͑ͭ͒͑̓̓̏̑̂͢v̷̶̛̠̹̥̟͇̜͕͓̖͕͖̯̺̖̪̤̘̇̈́͊ͭͥ̌̑͛̈́͛̅ͯ̕͝͡e̹͚͚̭̣̥̣͂͌̑̄ͦ͞͞ i̶̧͉͖̠̝̲̰̳̠̪ͣ̔̈́̑ͧ̀̓̽̈́͋͊ͥ̊̈́̉̍̋̂͗͘͢͢͡͞m̩͖̒̋̑̐ͣ ả̶̧̢̙̩̠͎̪̮͈̥̘̫̳̜͎̹͚͍̂̈̔̉͛́͑͋̐͟͟͡͞͡l̸̵̨̧̧͉̰͈̦̝̙͗ͤ̿̇ͨ̍͂ͧͩ͂̽ͩ͐͘̕͡͝ỉ̵̴̴̛͖̪̪̘͕̘͙̳̙̞͚̝̯̙͔̩̄̌͊̔͑̊ͫͮͪͩ͒ͥͨ̅͌͘͝͞v̡͙͌̅̐̓̌̓ë̟͚́ h̷̵̛̛̛͙̟͕̩͙̻̩̱̜̝̟̹͕̠̻͙ͪ̒ͦ̑ͭ̇̍̓̊̂͆̿̄̀̈́̎͊̊ͬ͌͜͡ͅe̲̮̠̫͚͚̰͂͒̇ͧ̆̌̔̋̔̌̍͐̎̈͐͘̚͢͠l̵͍̲̠ͨ͗̐_̵̷̢̨̛̣̣͈̲͙̬̪̞̦̅́͐ͫͫͩ̔̇̐ͦ̽̋̃̾̆ͯͧ̔͋ͥ͘͡p̖̜͎͚̣̜̞̞̠͂̄ͥͨͪ͆ͣ̍͢͞ ṁ̴̸̷̨̧̨̡̬̦̼̳̩̬͕͉͓̣͙̥̗̦̠̳̖̤̏ͧ̂͂ͭ̀̊ͤ̋ͦ͒͊ͣ͒̑͑ͦ̄͟͟͞ḙ̡̧̇̓̍̾͗͒̀͟͝,͒ d̸̸̜̤̯̠̩͔̥̞̫͈̣͍̝̎ͥ̇͆͐̽̈́̾ͤ̇͋͒́͘͜͢͞͡ͅe̬͉͊͗̓̃͐̔_̷͓̤̳͚ͣ̈́̆̉̓a̶̸̷̧̢̧̙̝̞̞̩̰͚͍̮ͧ̄ͪ̓̃̓͆ͤͭ̌͠r͈̫̟̣͕͇͓͙͕̯̓͂ͪ̿_̨̢̛͖̰̼̘̯̩̖̻̰̳̲͌ͫͦ͆ͬ̌͗̀̾ͤͫͤi̷̶̢̛͚͙̩͍̤̰̯̻̠̰͖̼̲̤͌̇ͥ́̆̐ͨͭ́ͪͭͭ̏ͥ̔͑͟͠͡͠ͅͅȩ̴̴̶̨̢̫̠̖̖̼̠̤̝̫̠̲͚̩̜̦̠͍͛ͮͤͤ̈̅̇͒ͨͥ̑͋̇̒ͫ̽̌̈́̇͠͠͝͡͞?̬̠͆_̵̡̣͙̣͖̗͈̜̫̣̙̽͋͊͐̌ͮͨ͒̓̽͗ͦ̃ͭ̅̈́͜͜͢͝ͅ I̧̖̲̬̺̪͚̳̮̳͇̭̜̬̩̲̪͒̾̆̏ͦͥͧ̓̋ͬ̒ͫͯ̉̅͠ s̍_̸͕̪͚̖̣͈͓̤ͥ̈́ͩ̈́ͫ̿̄ͥ̆̕͟͠t̵̺̝͔͇̝̣͍̰̰͉̻̘̗̯͍̹͕͎ͯ͛͆͛͐̿͂̏̒̏̍ͩ̈̍͋ͤ̆̀̉͟͞i̶͙͙͇̠͖ͯ̋̏̒̌̇͂ͦ_̶̬̲̠͇̐ͩl̸̶̢̨̢̟͙̘̗̺̪̯̞͙̤̣̺͂́̇ͩ̊͐ͤ̌̊̉ͭ͘l̵̶̡̧̬͉̰͓̫̹̩̣̘͗ͬ͂ͦ̀͊̕͟ lo̸̥̣͙̬̺̤̖͕̩̟ͧ́ͯ́̽ͧͩͫ̓̊̄ͪ͘v̶̴̵̧̧̢̡̛̪͖̺̹̜͙̞̭̞̞̞̗̩̲̣̭̦͔̝͔̻̀ͦ͊̅͌̅̎̾̆̀̾́̈͢͠ȩ͔͗ y͔ͦ̀͞ͅò̵̸̪̟̖̍̎̿ͨ̏͘͝͝u͒ͨ͞_̯͍̋̃ͯ͗ͨ̃ͤ̅ͨ̏̈́ͩ̚͡ͅ,̷̷̶̡̡̛̱̟̹̮̗̜͉͓̟͓̖̬͕͉̹̮͐̇͗̀̏̅͛ͫͩ̃͂̈ͨ͂̒͌͑ͩ͗̚͞͡͞ so̪͜ṙ̷̴͔̹̹̣̭͔̬͖̭ͧͪ̔͑ͦͤ̆ͤ̒ͪ͜͠r̡̺̟̻̠͉͕̬̭̓ͩͣ̋ͫ̐ͦ̀͆̚͜͞ý̡͇̟̮̦̩̲͙̾̇ͪ̍ͪͣͣ̃ i̥̿̒ d̷̪͚͓̗̫̻͇͚̈́͌͂̆̌ͬ͗̾ͤ̚͠͠ỉ̢̇ḓ̷̷̶̸̨͔̺̉̓̌̆ͩ̀̄̑̀̍͆̂̽n̨̙̹ͭ̉͐_̡̢̤̥̘͍͙̗͗̑̅̌͊̔͌ͦ͝_̜ţ͓̹̫̗͈̻̮̺̦ͦ̇͊ͨ̔̎ͯ͢͞ m̴̛͉͕͎͚̳̺̟̩͕̯̖̯̯̰̝̌͂̽͛̏̏̇̎̏̓̐͗̔͟͢͞͡ͅ_e̴̴͕͍̣̻̊ͮ͘̚a̡̛͚ͧ̋̉͒̈́ň̴̨̨̼̤͔̙̬͖̳̝͔̬̫̠̦̘ͧ͂͋͋ͦͬ̆ͥͣ̂ͫ̈̏̋ͩ͞ t̸̡̜̥̜̥̞̪̖̱̦̦̻͚͉̠͂̍̀̓ͣ̓̍̓͗͒̍ͪ̈́́̋̔̚͟ő̡̻̤̘̰͙̩̠̣̠̼ͪ̃̏ͭ̔̇ͪ͗̕͢͡ ḵ̡̛̺̺̙͇̭͙͙̯̮̩̯͉͈̱̘ͣ͂̋̾̎ͩ̑̀͆͐̀ͤ̎̊ͫ͌̇̓ͥ̕͟͢͜͢͝͝ͅͅî̴̧͖̬̙̲̤͎̎̌̊͝l̵̷̷̴̵̵̟͚͔̠̠̺̯̯͉̙͓̗̎̇ͣ͊͐̀̊̏ͬͤ̅ͬͦ͐́ͭͭ̕͞l͙͡ y̢̰̣͔̠̻̠̠ͯ̽̇̍ͯ͊ͦ̔͒͛̕͟o̴̢̧̘̹̥̫͈̥̝̱͕ͬͪ̓̆ͧ̀͜͢u̶̢̢̺̘̥̘̲͎̫ͫ͛̆̆͆ͬ̀ͬ̿̕,̴͉̣͙̬͙̳̗̜ͮ̄͒͛ͬͦ̈́̆̔̽͌ͥ̊͞͠͞ a̵̷̙̦̘͙̙̘̻̗̔͒̀̽̉m͛̋_̴̢̼̪͙̺͍̟͈̦̃ͩͣͤ̀̀͘͟͞ i̢̨̧̛͈̲̲̊̈͋̔ͧ̐͡ d̘͘_̸̵͖̟͍̀͆̾̏͂͋ͩ̐̈́ͪ̈́̈̈́̑͘͟͠é̐̌͢ą̸̷̷̵̼̞͓̼̗̖͚͖̙͕̟̙͚̦̆͑ͮ̀͑ͭ̇̋ͮ̓̓͑̀ͨ͌̄͑ͥ̆̎̾͌͒͊̊̚͞d̸̷̛̬̬͎͈̯̼̞͈̭͇̖̙̯͑̂ͩ̿̾̈́̇̉ͣͦ̕͞͡ͅͅ?̛͔̠̜̦̆̓ͨ̈ ą̴̧̧̘̲̙͔̦̙̖̙̉̏̑͗͐̎̅̃̕͘͘͠͝͡r̸͇̤͔̗̅́̎͐̒̚e̷̛̦̦̻̰̻ͯͩͥ́̀̾̉̽ͯ̓̑̍͗̒̃̓̎̋͢͝ y̗̟̭̫̟̩̐̌́ͨ̓̄̉́͢o̧̡̗̙̯͎̼̙̥̻̼̳̹̠͕͈̿̊͌́̒ͬ̂ͬ̈ͫ̽͛͑͂̇́͐͝ừ̸̠͙́̂̏̾͒ͧ̈́͞ h͖͕̤ͧͥ̉e̴͍͚̤͍̞͌͐͐̓ͬ̈́̄͌ͨ͘͞r̗̤̻̬̘̳͇̠̿ͯ́͋̚͢͡e̸̗͚̳͗ͪ̏̃͆͟͝ ț̨̦̃̋ͬͭ_̸͉͖̦͌͒͂̐ǫ̵̵̴̯͙͔̺̤̳̪̝͕̳̬̣͙̼͖̬̼ͮ͒̉͂ͯ̈̓ͨ͛͂̓͆́͘͟͡͝͠ǫ̶̨͔̀̌͗ͦ̌̊̍́̅͞ͅͅ_̵̳͆ͅ?̨̯̼͙̺̗̼͚̝̰̜͎͕͛̍̔̇ͫ̋ͨ͒͜͢͞_̰̮̑͗̑̂́͡ͅ į̵̛̰̹͖͓̯̘͙̦̤̳̠̯͈̙̺͇̲͒͋͋̿̄̓ͫ͐̋̌ͮ͑̔ͯ̈͛ͅͅ w̟͕ͦ͂ǫ̧͍͈͎̖͆͆̊̍ͧ̊ͨ͊_̫̘͛ͩ̈ͅų̮̜̻͖͚͆́̑̇ͬͥ͛l̺̠͋ͪ͠d̷͕̯͓̠̥̙̮͙̯̘̍ͮͦͨ̏͌͛̈ͪ̀̿́͒͐ͥͦͣ͛͒̚͟͡͞͝ͅͅ_̠̼̙ c̲ͩͪ͐͝_̛̱̜͔̖ͫͯͬ͡r̶̭̜̘̜̀ͥ̐̍_̨̼͕̘̱̹̞͚̞̣̆̌͊ͭͦ̈͌̾̿̎ͥyͮ́̐̽ b̴̢̛̳͈̫̫͚͕̭͔̻͎̰̻̠͈͙̀͊ͧͭͣͣ͑̆ͩ̓͋ͥ͗̿͂͒͌̾́̈́̍̓͂ͣ͜͟͟u̸̵̴̳̣̭͕̯͐̚̕t̵̷̡͈͈͇̪̻̤̱̰̱͇͎̭̂̅͆̌ͩ̇ͬ͛̈̔͐̃͘ i͕̖͖̯̣̍ͬ̏̏͠ c̶̵̨̰̺̺̺̝̗̬̏ͯ̑ͪ̂ͤ͜͝_̶̴̨̛̪̭̪̠̲̲̣̬͉̟̗́̆̎ͮ̔ͦ̚͜͝ͅa_̷̨̪͉̞̤̝͙̞͈̜̪͔̝̦͉͂̊̏̊͌̽ͨ̾̔̇͐͑ͮͣ͌ͬ͘̚͟͞͞͞͡_̹͒ͭ̾ǹ̶̵̺̯̠̗͍̒͂ͮ̆ͤ͆͘͟ͅ_̧̗̙̹͇̏̉ͪ̈́ͨ͑ͭ͆̍̌͊̓ͥͮ͘͢͡t̛̼͎̙̱͇̗͉̹̮̰̜̍̂̎̾̇̂́ͣ͗́ͧ̒͗̅̂ͥͫ͢ͅͅ a̷̧̹͇̼̬͒̑̿̋͌ͧͮ̐͢͝n̨̘̺̭͓̗͔͔̻ͯ͐̀͒̀̔̈́̍ͭ̀̉̃̄̕͟͟y̸̶̶̧̢̡̧̬͈̹͎͔̙̜̖̹͙͕͛ͩ̈́͗ͩ͛͒̎ͨͨ̆͗̈́ͥ̈́́͊́ͩͪ̄̋͟͢͟͝͠ṃ̷̶̷̢͈̪͈̠̺͇͈͊ͪͧͭ̐̇̉͊̿̓͝͡͡͞o͚̘̤̣̭̻͆ͨ̂ͫ̾ͩ̇ͥ̒ͨͅr̢̡̢̬̣͕͖̼̮̘͍͈̦͎̟̓̉̽̆ͬ͗ͯ̓́̐̀̒͘͘͜͟ͅ_̵̹̙͈͎͗̋e̬͉̩̥̤͓͊̆_̧̥̬̺̬̲͔̽ͪ̿̓̃͒̽͆ͯͨ͐̾̚͞͝͝ î̴̴̵̷̢̟̝̗͕̘͇̞̜̗ͤ̈̓̋͆̈̎̽͒ͥͬ͌͟͟͝͞ͅm̢̠̠͕̠̖̭͔͊̀͛̋̌̀ͯ̽́̀͐͟ͅ s̢̱̩͖̥͓̜͈͕̠ͧ̅̀́ͯ̆ͫ̒ͦ̔͜͢͞͞ȍ̻_̴̴̸͔͇͕̱͚̫͓̞̥̾̽̑̔ͦͦͦͩ͂̉ͯ̀_ ţ̴̢̡̨̮̻̪̥̥͔̞̝͚͖͉̬̆ͫͫ̄̍ͦ̔̑͆ͭ͗ͫ̉ͭͮ͒́͒̍̀̇́̾͗ͮ̇͢͜͡ͅi͑̿r͓̹̹̰̘̹͔̰͇̭̐̇͋̊̈́̂̇̑̕͠_̞̙̼̂ͪ͒̎ͧ̾͝e̛̺̬̠͙̜͕͎̠̫͕̙͖̺̞̖̟͈̯̟̤̦̓͌ͭ͐̀̈́́̅̉̃̐ͩ̋̄ͭ̋̿̚̚͠ḋ̸̴̡̛̜̜̜͉͇̗̣̲͔̻̥̺̘̽̔̉ͨͨ̍̓̓͟͝͡,̨̢̩̝̲̝̪͌̄̓̾́̌ͥ̊́͟͢__̛̲͎̘̭͔͙̣ͮ̃ͧͥ̊͋̌ͨͣ̔̿͂̚͜ heͯ_͎̭͗ͤ̿l͙̲͓͕̇ͥ͛͌̈͘p̸̷̡̠ͥͩͦ̄̏̓͂̕͜͡͡ m̈e̷̢̝͙̭̖̰̩̪͍͙͔͔̅ͯͫ͐̃̔̃̏ͩ̚͘̕_̴͙̜̖̣͕̼̓̈́̐ͦ͞͞,̷̗͕̝̎̊ͭ̅͋ͥ̇͟͠ d̵̴̻̠͈͍̱̥̬̘͉͎̯̥̲̪͍͎̠ͣͫ́̂̃͌͆̏̋͛ͭ̾̀̿́̂ͯ̌̅͆͛̽̅͒̿̚ė̶̘̙̙̿̀ͮ̔̑̂͡a̛͈̙̭̭̓́ͦ͋̾̀̏͘ͅr̨̢̦͖̘̒̂,̸̡̻͙̟̭͖͕̩̜͐̀ͪͫ͘ į̸̧̢̛̟͉͓̬̮̙̫̝͕͎̹̟̯͕̱̮͍͍̏̽̈́̍ͭ̌ͭ̆̊͑̆́͋̂͘͡͞ l̡̨̻̺̞͇̲͎̖͙̫̟̗̦̤̱̘͎͂ͣͣ̀̃ͣ̌̓ͪ͂̔̽͒ͥͥ̓͗ͤ͌̓̕͜͟͜ö̷͓̲̬̗́͐͌͟v̡̢̰̠͈̖̰̠̯ͨͬͭ̊̕_̪̯̖̐͢é̸̺̠̖̱̺͖͕̠̼͙̹͔̜̃ͬ̊̿͗̂̈̔͟͝_̭̇͐ͨͤ_̡̠̞̬̖̜ͧ̒̆̀͌̒ y̸̡̝̹͕̮̞̦͐̆ͥ̉̌ͬ͂ͩ̈́͠͞͝͞o̢̧̢̬͈̩̿̇u͇̗̹̻͂ͦ̇̒͛̃ s̶͎̠̮̪̓ͮ͊͆̄͝ơ̷̢̺̣̙͖̼̪͓͇̮̲͇̦͓͋͌̑̐̀ͩͩͦ͛͊̚͞ m̷̶̶̴̶̧̯̙͎͚̲͍͇̬̬̝͈̼ͫ̌͆̾ͦ͒ͤͭ̉ͩͤ̃͊͑ͩ͛̚͝͡͠ŭ̴̸̢̧̩͇̞̰͖͚̲̼̰̹̩̯̯̜̗̭͑̎̑ͭͭ͐̿̒̌ͪͥ͢͡͡͡c̓͂̓̇ḧ̢̛̫͉͔̭̓̈́ͣͨ̔͜_̶͉̘̬̝ͩͬ̀͛͌̿̊̔ i̴̴̟̖̜͉̖ͨ̍̈́̂ͭ͑͡ ş̨̛̬͕̤͍̝̮̖͍̔́̒̑ͭ̾ͯ̈́̉ͮ̔̚͞͡_̲̓͞t̵̢͔̦̯̻̫̙͎̙̘̟̑ͯ̎̎͆ͤ͂ͭ͛̑͑̆ͮ̐̈́̚i̵̧̛̟̤̦ͦͧ̍ͭ͘ĺ̸̷̡̼͙̟̘̦̘̗̬͉͎̱͙̺̌̐̈ͤ̇͛ͣͩͪͨͩͪͮ̕͢l̼̖̦͇̤̪̗̰̪̙͂ͥ͑ͫͫ̂ͥ̄́ͭ́̓͟͢͠ͅ l̶̷̷̢̨̜̖̖̰͍̜͇̱̮͆̈́̀ͩͤ̋̈́̊̅̾̀̓ͥͮ̚͡o̸̴̶̡̧͇͕̩̣͖̪̰͖̜̻̔̎ͪ̓̾̀͐̉̑͋͒̉ͧ̔̓̀͛̍̈ͬͫͣ̃ͦ̋̓͡͝͡v̵̴̶̷̸̛̘͚̬̐ͯͪ̆e̷̖͚͙ͥ̌̋̈͂̀ͪ̔̑̚͟͞_̵̮̻̞̈̐̂ͨ͒_̗̖̤̝͕̟̟̝̥̂̓̎́̚ ỳ̷͍̫̩̳͖̰̞̳̠ͩ͗́̄̐͟o̶̷̸̢̤̱͔̻̩͚̹͕̥̤̐̓͊̓̒ͫ̄̅̐̃ͭ̑̂͛̓ͫ̎̅̈́̇̽͢͠͝u̙̞̅̅.̦̫̘̻̜̟̭̻̬̦̄ͯ͂̓̂͊̅ͥ̚͢͠.̨̟̖̀._̴̴͙͙͖̺͈̋̑͑̂̋̅͞ͅ.̸̶͕͉͉̝̼̻͇̂͗̓ͤ̾̽̇ͦͮͯ͜_̷̨̢̡̳̯͖̻̭͇͍͎ͨ̿̅̇͗̄̑̋ͤ͘͞͞ͅ p̶̢̩̤̪͔̱̥̝̩̫̖̏͊̎̊̋̒̊ͪ͘ͅl̶̵̡̢̨̤̘͔͔̤͚̜͉͙͓͔̖̻̦̳̭͚̥͋̓̎̈́͊͐̅ͣ͒ͭ̐̊ͮ̆̈̓̚̚͜ȩͪ̊a̶̦͍̣̟͓̤͗͒́ͫ͊ͮ͢͟͞s̸̸̸̷̴̨̖͕͙̮ͨ̋̃̓͋̌̚͡͝͠e̴̡̡̨̥͖̖͚̘̯͋̄ͫ̎͗̽̇͑̉͘͟͞_",
                                        author = "The"
                                    });
                                    break;
                            }

                            break;
                        //boss
                        case 2:
                            switch (lootParameters.type)
                            {
                                case "mb":
                                    var s = rnd.Next(1, 10);
                                    for (int i = 0; i < s; i++)
                                    {
                                        var it = rnd.Next(0, 100);
                                        switch (it + 1)
                                        {
                                            case var _ when it is <= 10 and >= 1: //10
                                                lootBack.Add(legendary[rnd.Next(0, legendary.Count())]);
                                                break;
                                            case var _ when it is <= 30 and >= 11: //20
                                                lootBack.Add(veryrare[rnd.Next(0, veryrare.Count())]);
                                                break;
                                            case var _ when it is <= 60 and >= 31: //30
                                                lootBack.Add(rare[rnd.Next(0, rare.Count())]);
                                                break;
                                            case var _ when it is <= 100 and >= 61: //40
                                                lootBack.Add(uncommon[rnd.Next(0, uncommon.Count())]);
                                                break;
                                        }
                                    }

                                    int gold = 0;
                                    gold = rnd.Next(0, 10);
                                    if (rnd.Next(0, 10) <= 10)
                                    {
                                        gold = rnd.Next(0, 100);
                                        if (rnd.Next(0, 10) <= 6)
                                        {
                                            gold = rnd.Next(0, 1000);
                                            if (rnd.Next(0, 10) <= 4)
                                            {
                                                gold = rnd.Next(0, 10000);
                                                if (rnd.Next(0, 10) == 0)
                                                {
                                                    gold = rnd.Next(0, 100000);
                                                    if (rnd.Next(0, 10) == 0)
                                                    {
                                                        gold = rnd.Next(0, 1000000);
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    lootBack.Add(new LootBack()
                                    {
                                        namel = "Money", estval = gold.ToString("0g 0s 0c"),
                                    });
                                    break;
                                case "go":
                                    var fe = rnd.Next(1, 10);
                                    for (int i = 0; i < fe; i++)
                                    {
                                        var it = rnd.Next(0, 100);
                                        switch (it + 1)
                                        {
                                            case var _ when it is <= 10 and >= 1: //10
                                                lootBack.Add(legendary[rnd.Next(0, legendary.Count())]);
                                                break;
                                            case var _ when it is <= 40 and >= 11: //20
                                                lootBack.Add(veryrare[rnd.Next(0, veryrare.Count())]);
                                                break;
                                            case var _ when it is <= 80 and >= 41: //30
                                                lootBack.Add(rare[rnd.Next(0, rare.Count())]);
                                                break;
                                            case var _ when it is <= 100 and >= 81: //40
                                                lootBack.Add(uncommon[rnd.Next(0, uncommon.Count())]);
                                                break;
                                        }
                                    }

                                    int gold2e = 0;
                                    gold2e = rnd.Next(0, 100);
                                    if (rnd.Next(0, 10) >= 5)
                                    {
                                        gold2e = rnd.Next(0, 1000);
                                        if (rnd.Next(0, 10) >= 5)
                                        {
                                            gold2e = rnd.Next(0, 10000);
                                            if (rnd.Next(0, 10) == 5)
                                            {
                                                gold2e = rnd.Next(0, 100000);
                                                if (rnd.Next(0, 10) == 5)
                                                {
                                                    gold2e = rnd.Next(0, 1000000);
                                                    if (rnd.Next(0, 10) == 0)
                                                    {
                                                        gold2e = rnd.Next(0, 10000000);
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    var uuu2e = rnd.Next(0, 10);
                                    for (int ir = 0; ir < uuu2e; ir++)
                                    {
                                        var ssss = ldbequip.FindById(rnd.Next(1, ldbequip.Count()));
                                        lootBack.Add(new LootBack()
                                        {
                                            namel = ssss.name, desc = ssss.desc
                                        });
                                    }

                                    lootBack.Add(new LootBack()
                                    {
                                        namel = "Money", estval = gold2e.ToString("0g 0s 0c"),
                                    });
                                    break;
                                case "tbb":
                                    var fu = rnd.Next(1, 10);
                                    for (int i = 0; i < fu; i++)
                                    {
                                        var it = rnd.Next(0, 100);
                                        switch (it + 1)
                                        {
                                            case var _ when it is <= 50 and >= 1: //10
                                                lootBack.Add(legendary[rnd.Next(0, legendary.Count())]);
                                                break;
                                            case var _ when it is <= 70 and >= 51: //20
                                                lootBack.Add(veryrare[rnd.Next(0, veryrare.Count())]);
                                                break;
                                            case var _ when it is <= 90 and >= 71: //30
                                                lootBack.Add(rare[rnd.Next(0, rare.Count())]);
                                                break;
                                            case var _ when it is <= 100 and >= 91: //40
                                                lootBack.Add(uncommon[rnd.Next(0, uncommon.Count())]);
                                                break;
                                        }
                                    }

                                    int gold2u = 0;
                                    gold2u = rnd.Next(0, 100);
                                    if (rnd.Next(0, 10) >= 10)
                                    {
                                        gold2u = rnd.Next(0, 1000);
                                        if (rnd.Next(0, 10) >= 10)
                                        {
                                            gold2u = rnd.Next(0, 10000);
                                            if (rnd.Next(0, 10) >= 5)
                                            {
                                                gold2u = rnd.Next(0, 100000);
                                                if (rnd.Next(0, 10) >= 5)
                                                {
                                                    gold2u = rnd.Next(0, 1000000);
                                                    if (rnd.Next(0, 10) == 0)
                                                    {
                                                        gold2u = rnd.Next(0, 10000000);
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    var uuu2u = rnd.Next(0, 5);
                                    for (int ir = 0; ir < uuu2u; ir++)
                                    {
                                        var ssss = ldbequip.FindById(rnd.Next(1, ldbequip.Count()));
                                        lootBack.Add(new LootBack()
                                        {
                                            namel = ssss.name, desc = ssss.desc
                                        });
                                    }

                                    lootBack.Add(new LootBack()
                                    {
                                        namel = "Money", estval = gold2u.ToString("0g 0s 0c"),
                                    });
                                    break;
                                case "tbbcc":
                                    var fo = rnd.Next(1, 10);
                                    for (int i = 0; i < fo; i++)
                                    {
                                        var it = rnd.Next(0, 100);
                                        switch (it + 1)
                                        {
                                            case var _ when it is <= 10 and >= 1: //10
                                                lootBack.Add(legendary[rnd.Next(0, legendary.Count())]);
                                                break;
                                            case var _ when it is <= 40 and >= 11: //20
                                                lootBack.Add(veryrare[rnd.Next(0, veryrare.Count())]);
                                                break;
                                            case var _ when it is <= 80 and >= 41: //30
                                                lootBack.Add(rare[rnd.Next(0, rare.Count())]);
                                                break;
                                            case var _ when it is <= 100 and >= 81: //40
                                                lootBack.Add(uncommon[rnd.Next(0, uncommon.Count())]);
                                                break;
                                        }
                                    }

                                    int gold2o = 0;
                                    gold2o = rnd.Next(0, 100);
                                    if (rnd.Next(0, 10) >= 5)
                                    {
                                        gold2o = rnd.Next(0, 1000);
                                        if (rnd.Next(0, 10) == 0)
                                        {
                                            gold2o = rnd.Next(0, 10000);
                                            if (rnd.Next(0, 10) == 0)
                                            {
                                                gold2o = rnd.Next(0, 100000);
                                                if (rnd.Next(0, 10) == 0)
                                                {
                                                    gold2o = rnd.Next(0, 1000000);
                                                    if (rnd.Next(0, 10) == 0)
                                                    {
                                                        gold2o = rnd.Next(0, 10000000);
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    var uuu2o = rnd.Next(0, 10);
                                    for (int ir = 0; ir < uuu2o; ir++)
                                    {
                                        var ssss = ldbequip.FindById(rnd.Next(1, ldbequip.Count()));
                                        lootBack.Add(new LootBack()
                                        {
                                            namel = ssss.name, desc = ssss.desc
                                        });
                                    }

                                    lootBack.Add(new LootBack()
                                    {
                                        namel = "Money", estval = gold2o.ToString("0g 0s 0c"),
                                    });
                                    break;
                                case "god":
                                    int gold2q = 0;
                                    gold2q = rnd.Next(0, 100);
                                    if (rnd.Next(0, 10) >= 5)
                                    {
                                        gold2q = rnd.Next(0, 1000);
                                        if (rnd.Next(0, 10) >= 5)
                                        {
                                            gold2q = rnd.Next(0, 10000);
                                            if (rnd.Next(0, 10) >= 5)
                                            {
                                                gold2q = rnd.Next(0, 100000);
                                                if (rnd.Next(0, 10) >= 5)
                                                {
                                                    gold2q = rnd.Next(0, 1000000);
                                                    if (rnd.Next(0, 10) >= 5)
                                                    {
                                                        gold2q = rnd.Next(0, 10000000);
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    var uuu2q = rnd.Next(0, 10);
                                    for (int ir = 0; ir < uuu2q; ir++)
                                    {
                                        var ssss = ldbmag.FindById(rnd.Next(1, ldbmag.Count()));
                                        lootBack.Add(new LootBack()
                                        {
                                            namel = ssss.name, desc = ssss.desc
                                        });
                                    }

                                    lootBack.Add(new LootBack()
                                    {
                                        namel = "Money", estval = gold2q.ToString("0g 0s 0c"),
                                    });
                                    break;
                                case "wiz":
                                    var fc = rnd.Next(1, 5);
                                    for (int i = 0; i < fc; i++)
                                    {
                                        var it = rnd.Next(0, 100);
                                        switch (it + 1)
                                        {
                                            case var _ when it is <= 10 and >= 1: //10
                                                lootBack.Add(legendary[rnd.Next(0, legendary.Count())]);
                                                break;
                                            case var _ when it is <= 40 and >= 11: //20
                                                lootBack.Add(veryrare[rnd.Next(0, veryrare.Count())]);
                                                break;
                                            case var _ when it is <= 80 and >= 41: //30
                                                lootBack.Add(rare[rnd.Next(0, rare.Count())]);
                                                break;
                                            case var _ when it is <= 100 and >= 81: //40
                                                lootBack.Add(uncommon[rnd.Next(0, uncommon.Count())]);
                                                break;
                                        }
                                    }

                                    int gold2c = 0;
                                    gold2c = rnd.Next(0, 100);
                                    if (rnd.Next(0, 10) >= 5)
                                    {
                                        gold2c = rnd.Next(0, 1000);
                                        if (rnd.Next(0, 10) >= 5)
                                        {
                                            gold2c = rnd.Next(0, 10000);
                                            if (rnd.Next(0, 10) == 0)
                                            {
                                                gold2c = rnd.Next(0, 100000);
                                                if (rnd.Next(0, 10) == 0)
                                                {
                                                    gold2c = rnd.Next(0, 1000000);
                                                    if (rnd.Next(0, 10) == 0)
                                                    {
                                                        gold2c = rnd.Next(0, 10000000);
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    var uuu2c = rnd.Next(0, 10);
                                    for (int ir = 0; ir < uuu2c; ir++)
                                    {
                                        var ssss = ldbmag.FindById(rnd.Next(1, ldbmag.Count()));
                                        lootBack.Add(new LootBack()
                                        {
                                            namel = ssss.name, desc = ssss.desc
                                        });
                                    }

                                    lootBack.Add(new LootBack()
                                    {
                                        namel = "Money", estval = gold2c.ToString("0g 0s 0c"),
                                    });
                                    break;
                                case "bh":
                                    var fa = rnd.Next(1, 10);
                                    for (int i = 0; i < fa; i++)
                                    {
                                        var it = rnd.Next(0, 100);
                                        switch (it + 1)
                                        {
                                            case var _ when it is <= 10 and >= 1: //10
                                                lootBack.Add(legendary[rnd.Next(0, legendary.Count())]);
                                                break;
                                            case var _ when it is <= 40 and >= 11: //20
                                                lootBack.Add(veryrare[rnd.Next(0, veryrare.Count())]);
                                                break;
                                            case var _ when it is <= 80 and >= 41: //30
                                                lootBack.Add(rare[rnd.Next(0, rare.Count())]);
                                                break;
                                            case var _ when it is <= 100 and >= 81: //40
                                                lootBack.Add(uncommon[rnd.Next(0, uncommon.Count())]);
                                                break;
                                        }
                                    }

                                    int gold2a = 0;
                                    gold2a = rnd.Next(0, 100);
                                    if (rnd.Next(0, 10) >= 5)
                                    {
                                        gold2a = rnd.Next(0, 1000);
                                        if (rnd.Next(0, 10) == 0)
                                        {
                                            gold2a = rnd.Next(0, 10000);
                                            if (rnd.Next(0, 10) == 0)
                                            {
                                                gold2a = rnd.Next(0, 100000);
                                                if (rnd.Next(0, 10) == 0)
                                                {
                                                    gold2a = rnd.Next(0, 1000000);
                                                    if (rnd.Next(0, 10) == 0)
                                                    {
                                                        gold2a = rnd.Next(0, 10000000);
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    var uuu2a = rnd.Next(0, 1);
                                    for (int ir = 0; ir < uuu2a; ir++)
                                    {
                                        var ssss = ldbequip.FindById(rnd.Next(1, ldbequip.Count()));
                                        lootBack.Add(new LootBack()
                                        {
                                            namel = ssss.name, desc = ssss.desc
                                        });
                                    }

                                    lootBack.Add(new LootBack()
                                    {
                                        namel = "Money", estval = gold2a.ToString("0g 0s 0c"),
                                    });
                                    break;
                            }

                            break;
                        //shop
                        case 3:
                            break;
                            switch (lootParameters.type)
                            {
                                case "bystander":
                                    var s = rnd.Next(1, 5);
                                    for (int i = 0; i < s; i++)
                                    {
                                        var it = rnd.Next(0, 100);
                                        switch (it + 1)
                                        {
                                            case var _ when it is <= 4 and >= 1: //4
                                                lootBack.Add(rare[rnd.Next(0, rare.Count())]);
                                                break;
                                            case var _ when it is <= 20 and >= 5: //16
                                                lootBack.Add(uncommon[rnd.Next(0, uncommon.Count())]);
                                                break;
                                            case var _ when it is <= 50 and >= 21: //30
                                                lootBack.Add(common[rnd.Next(0, common.Count())]);
                                                break;
                                            case var _ when it is <= 100 and >= 51: //50
                                                lootBack.Add(mundane[rnd.Next(0, mundane.Count())]);
                                                break;
                                        }
                                    }

                                    int gold = 0;
                                    gold = rnd.Next(0, 10);
                                    if (rnd.Next(0, 10) == 0)
                                    {
                                        gold = rnd.Next(0, 100);
                                        if (rnd.Next(0, 10) == 0)
                                        {
                                            gold = rnd.Next(0, 1000);
                                            if (rnd.Next(0, 10) == 0)
                                            {
                                                gold = rnd.Next(0, 10000);
                                                if (rnd.Next(0, 10) == 0)
                                                {
                                                    gold = rnd.Next(0, 100000);
                                                    if (rnd.Next(0, 10) == 0)
                                                    {
                                                        gold = rnd.Next(0, 1000000);
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    lootBack.Add(new LootBack()
                                    {
                                        namel = "Money", estval = gold.ToString("0g 0s 0c"),
                                    });
                                    break;
                                case "bandit":
                                    var f = rnd.Next(1, 11);
                                    for (int i = 0; i < f; i++)
                                    {
                                        var it = rnd.Next(0, 100);
                                        switch (it + 1)
                                        {
                                            case var _ when it is <= 1 and >= 1: //1
                                                lootBack.Add(veryrare[rnd.Next(0, veryrare.Count())]);
                                                break;
                                            case var _ when it is <= 10 and >= 2: //9
                                                lootBack.Add(rare[rnd.Next(0, rare.Count())]);
                                                break;
                                            case var _ when it is <= 20 and >= 11: //10
                                                lootBack.Add(uncommon[rnd.Next(0, uncommon.Count())]);
                                                break;
                                            case var _ when it is <= 50 and >= 21: //80
                                                lootBack.Add(common[rnd.Next(0, common.Count())]);
                                                break;
                                            case var _ when it is <= 100 and >= 51: //50
                                                lootBack.Add(mundane[rnd.Next(0, mundane.Count())]);
                                                break;
                                        }
                                    }

                                    int gold2 = 0;
                                    gold2 = rnd.Next(0, 100);
                                    if (rnd.Next(0, 10) >= 5)
                                    {
                                        gold2 = rnd.Next(0, 1000);
                                        if (rnd.Next(0, 10) == 0)
                                        {
                                            gold2 = rnd.Next(0, 10000);
                                            if (rnd.Next(0, 10) == 0)
                                            {
                                                gold2 = rnd.Next(0, 100000);
                                                if (rnd.Next(0, 10) == 0)
                                                {
                                                    gold2 = rnd.Next(0, 1000000);
                                                    if (rnd.Next(0, 10) == 0)
                                                    {
                                                        gold2 = rnd.Next(0, 10000000);
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    var uuu2 = rnd.Next(0, 1);
                                    for (int ir = 0; ir < uuu2; ir++)
                                    {
                                        var ssss = ldbequip.FindById(rnd.Next(1, ldbequip.Count()));
                                        lootBack.Add(new LootBack()
                                        {
                                            namel = ssss.name, desc = ssss.desc
                                        });
                                    }

                                    lootBack.Add(new LootBack()
                                    {
                                        namel = "Money", estval = gold2.ToString("0g 0s 0c"),
                                    });
                                    break;
                                case "madman":
                                    if (rnd.Next(0, 5) == 0)
                                    {
                                        lootBack.Add(new LootBack()
                                        {
                                            namel = "Journal", weight = rnd.Next(0, 50).ToString(),
                                            estval = rnd.Next(0, 500).ToString(), category = "Madman's journal",
                                            rarity = "What‽‽‽",
                                            desc =
                                                "A book with almost indecipherable writings. Some pages have strange numbers and formulas, some have poems and stories. Maybe it will be possible to sell it.",
                                            author = "The Goose"
                                        });
                                    }

                                    var d = 1;
                                    for (int i = 0; i < d; i++)
                                    {
                                        var it = rnd.Next(0, 100);
                                        switch (it)
                                        {
                                            case 1:
                                                lootBack.Add(legendary[rnd.Next(0, legendary.Count())]);
                                                break;
                                            case 2:
                                                lootBack.Add(veryrare[rnd.Next(0, veryrare.Count())]);
                                                break;
                                            case 3:
                                                lootBack.Add(rare[rnd.Next(0, rare.Count())]);
                                                break;
                                            case 4:
                                                lootBack.Add(uncommon[rnd.Next(0, uncommon.Count())]);
                                                break;
                                            case 6:
                                                lootBack.Add(common[rnd.Next(0, common.Count())]);
                                                break;
                                            case 7:
                                                lootBack.Add(mundane[rnd.Next(0, mundane.Count())]);
                                                break;
                                        }
                                    }

                                    switch (rnd.Next(0, 3))
                                    {
                                        case 0:
                                            lootBack.Add(new List<LootBack>()
                                            {
                                                new LootBack()
                                                {
                                                    namel = "Old dirty coin", estval = "0"
                                                }
                                            });
                                            break;
                                        case 1:
                                            lootBack.Add(new LootBack()
                                            {
                                                namel = "Gold", estval = "1",
                                            });
                                            break;
                                        case 2:
                                            lootBack.Add(new LootBack()
                                            {
                                                namel = "Gold", estval = "2",
                                            });
                                            break;
                                    }

                                    break;
                                case "exad":
                                    var r = rnd.Next(1, 15);
                                    for (int i = 0; i < r; i++)
                                    {
                                        var it = rnd.Next(0, 100);
                                        switch (it + 1)
                                        {
                                            case var _ when it is <= 5 and >= 1:
                                                lootBack.Add(legendary[rnd.Next(0, legendary.Count())]);
                                                break;
                                            case var _ when it is <= 14 and >= 6:
                                                lootBack.Add(veryrare[rnd.Next(0, veryrare.Count())]);
                                                break;
                                            case var _ when it is <= 30 and >= 15:
                                                lootBack.Add(rare[rnd.Next(0, rare.Count())]);
                                                break;
                                            case var _ when it is <= 60 and >= 31:
                                                lootBack.Add(uncommon[rnd.Next(0, uncommon.Count())]);
                                                break;
                                            case var _ when it is <= 90 and >= 61:
                                                lootBack.Add(common[rnd.Next(0, common.Count())]);
                                                break;
                                            case var _ when it is <= 100 and >= 91:
                                                lootBack.Add(mundane[rnd.Next(0, mundane.Count())]);
                                                break;
                                        }
                                    }

                                    int gold4 = 0;
                                    gold4 = rnd.Next(0, 100);
                                    if (rnd.Next(0, 10) >= 5)
                                    {
                                        gold4 = rnd.Next(0, 1000);
                                        if (rnd.Next(0, 10) >= 4)
                                        {
                                            gold4 = rnd.Next(0, 10000);
                                            if (rnd.Next(0, 10) >= 3)
                                            {
                                                gold4 = rnd.Next(0, 100000);
                                                if (rnd.Next(0, 10) == 0)
                                                {
                                                    gold4 = rnd.Next(0, 1000000);
                                                    if (rnd.Next(0, 10) == 0)
                                                    {
                                                        gold4 = rnd.Next(0, 10000000);
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    lootBack.Add(new LootBack()
                                    {
                                        namel = "Money", estval = gold4.ToString("0g 0s 0c"),
                                    });
                                    break;
                                case "magic":
                                    var u = rnd.Next(0, 4);
                                    for (int i = 0; i < u; i++)
                                    {
                                        var it = rnd.Next(0, 100);
                                        switch (it + 1)
                                        {
                                            case var _ when it is <= 7 and >= 1: //7
                                                lootBack.Add(legendary[rnd.Next(0, legendary.Count())]);
                                                break;
                                            case var _ when it is <= 20 and >= 8: //12
                                                lootBack.Add(veryrare[rnd.Next(0, veryrare.Count())]);
                                                break;
                                            case var _ when it is <= 35 and >= 21: //15
                                                lootBack.Add(rare[rnd.Next(0, rare.Count())]);
                                                break;
                                            case var _ when it is <= 60 and >= 36: //25
                                                lootBack.Add(uncommon[rnd.Next(0, uncommon.Count())]);
                                                break;
                                            case var _ when it is <= 85 and >= 61: //25
                                                lootBack.Add(common[rnd.Next(0, common.Count())]);
                                                break;
                                            case var _ when it is <= 100 and >= 86: //15
                                                lootBack.Add(mundane[rnd.Next(0, mundane.Count())]);
                                                break;
                                        }
                                    }

                                    var uuu = rnd.Next(0, 2);
                                    for (int ir = 0; ir < uuu; ir++)
                                    {
                                        var kkkk = ldbmag.FindAll().Count();
                                        var ooo = rnd.Next(1, kkkk);
                                        var ssss = ldbmag.FindById(ooo);
                                        lootBack.Add(new LootBack()
                                        {
                                            namel = ssss.name, desc = ssss.desc
                                        });
                                    }

                                    int gold5 = 0;
                                    gold5 = rnd.Next(0, 100);
                                    if (rnd.Next(0, 10) >= 4)
                                    {
                                        gold5 = rnd.Next(0, 1000);
                                        if (rnd.Next(0, 10) >= 3)
                                        {
                                            gold5 = rnd.Next(0, 10000);
                                            if (rnd.Next(0, 10) >= 2)
                                            {
                                                gold5 = rnd.Next(0, 100000);
                                                if (rnd.Next(0, 10) == 0)
                                                {
                                                    gold5 = rnd.Next(0, 1000000);
                                                    if (rnd.Next(0, 10) == 0)
                                                    {
                                                        gold5 = rnd.Next(0, 10000000);
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    lootBack.Add(new LootBack()
                                    {
                                        namel = "Money", estval = gold5.ToString("0g 0s 0c"),
                                    });
                                    break;
                                case "rich":
                                    var yu = rnd.Next(1, 15);
                                    for (int i = 0; i < yu; i++)
                                    {
                                        var it = rnd.Next(0, 100);
                                        switch (it + 1)
                                        {
                                            case var _ when it is <= 10 and >= 1: //10
                                                lootBack.Add(rare[rnd.Next(0, rare.Count())]);
                                                break;
                                            case var _ when it is <= 30 and >= 11: //20
                                                lootBack.Add(uncommon[rnd.Next(0, uncommon.Count())]);
                                                break;
                                            case var _ when it is <= 60 and >= 31: //30
                                                lootBack.Add(common[rnd.Next(0, common.Count())]);
                                                break;
                                            case var _ when it is <= 100 and >= 60: //40
                                                lootBack.Add(mundane[rnd.Next(0, mundane.Count())]);
                                                break;
                                        }
                                    }

                                    int gold6 = 0;
                                    gold6 = rnd.Next(0, 100);
                                    if (rnd.Next(0, 10) >= 5)
                                    {
                                        gold6 = rnd.Next(0, 1000);
                                        if (rnd.Next(0, 10) >= 4)
                                        {
                                            gold6 = rnd.Next(0, 10000);
                                            if (rnd.Next(0, 10) >= 3)
                                            {
                                                gold6 = rnd.Next(0, 100000);
                                                if (rnd.Next(0, 10) == 0)
                                                {
                                                    gold6 = rnd.Next(0, 1000000);
                                                    if (rnd.Next(0, 10) == 0)
                                                    {
                                                        gold6 = rnd.Next(0, 10000000);
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    lootBack.Add(new LootBack()
                                    {
                                        namel = "Money", estval = gold6.ToString("0g 0s 0c"),
                                    });
                                    break;
                                case "poor":
                                    var po = rnd.Next(1, 15);
                                    for (int i = 0; i < po; i++)
                                    {
                                        var it = rnd.Next(0, 100);
                                        switch (it + 1)
                                        {
                                            case var _ when it is <= 10 and >= 1:
                                                lootBack.Add(common[rnd.Next(0, common.Count())]);
                                                break;
                                            case var _ when it is <= 100 and >= 91:
                                                lootBack.Add(mundane[rnd.Next(0, mundane.Count())]);
                                                break;
                                        }
                                    }

                                    int gold7 = 0;
                                    gold7 = rnd.Next(0, 10);
                                    if (rnd.Next(0, 10) == 0)
                                    {
                                        gold7 = rnd.Next(0, 100);
                                        if (rnd.Next(0, 10) == 0)
                                        {
                                            gold7 = rnd.Next(0, 1000);
                                            if (rnd.Next(0, 10) == 0)
                                            {
                                                gold7 = rnd.Next(0, 10000);
                                                if (rnd.Next(0, 10) == 0)
                                                {
                                                    gold7 = rnd.Next(0, 100000);
                                                    if (rnd.Next(0, 10) == 0)
                                                    {
                                                        gold7 = rnd.Next(0, 1000000);
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    lootBack.Add(new LootBack()
                                    {
                                        namel = "Money", estval = gold7.ToString("0g 0s 0c"),
                                    });
                                    break;
                            }

                            break;
                        //env
                        case 4:
                            switch (lootParameters.type)
                            {
                                case "forest":
                                    var s = rnd.Next(1, 5);
                                    for (int i = 0; i < s; i++)
                                    {
                                        var it = rnd.Next(0, 100);
                                        switch (it + 1)
                                        {
                                            case var _ when it is <= 4 and >= 1: //4
                                                lootBack.Add(rare[rnd.Next(0, rare.Count())]);
                                                break;
                                            case var _ when it is <= 20 and >= 5: //16
                                                lootBack.Add(uncommon[rnd.Next(0, uncommon.Count())]);
                                                break;
                                            case var _ when it is <= 50 and >= 21: //30
                                                lootBack.Add(common[rnd.Next(0, common.Count())]);
                                                break;
                                            case var _ when it is <= 100 and >= 51: //50
                                                lootBack.Add(mundane[rnd.Next(0, mundane.Count())]);
                                                break;
                                        }
                                    }

                                    int gold = 0;
                                    gold = rnd.Next(0, 10);
                                    if (rnd.Next(0, 10) == 0)
                                    {
                                        gold = rnd.Next(0, 100);
                                        if (rnd.Next(0, 10) == 0)
                                        {
                                            gold = rnd.Next(0, 1000);
                                            if (rnd.Next(0, 10) == 0)
                                            {
                                                gold = rnd.Next(0, 10000);
                                                if (rnd.Next(0, 10) == 0)
                                                {
                                                    gold = rnd.Next(0, 100000);
                                                    if (rnd.Next(0, 10) == 0)
                                                    {
                                                        gold = rnd.Next(0, 1000000);
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    lootBack.Add(new LootBack()
                                    {
                                        namel = "Money", estval = gold.ToString("0g 0s 0c"),
                                    });
                                    break;
                                case "cave":
                                    var f = rnd.Next(1, 11);
                                    for (int i = 0; i < f; i++)
                                    {
                                        var it = rnd.Next(0, 100);
                                        switch (it + 1)
                                        {
                                            case var _ when it is <= 1 and >= 1: //1
                                                lootBack.Add(veryrare[rnd.Next(0, veryrare.Count())]);
                                                break;
                                            case var _ when it is <= 10 and >= 2: //9
                                                lootBack.Add(rare[rnd.Next(0, rare.Count())]);
                                                break;
                                            case var _ when it is <= 20 and >= 11: //10
                                                lootBack.Add(uncommon[rnd.Next(0, uncommon.Count())]);
                                                break;
                                            case var _ when it is <= 50 and >= 21: //80
                                                lootBack.Add(common[rnd.Next(0, common.Count())]);
                                                break;
                                            case var _ when it is <= 100 and >= 51: //50
                                                lootBack.Add(mundane[rnd.Next(0, mundane.Count())]);
                                                break;
                                        }
                                    }

                                    int gold2 = 0;
                                    gold2 = rnd.Next(0, 100);
                                    if (rnd.Next(0, 10) >= 5)
                                    {
                                        gold2 = rnd.Next(0, 1000);
                                        if (rnd.Next(0, 10) == 0)
                                        {
                                            gold2 = rnd.Next(0, 10000);
                                            if (rnd.Next(0, 10) == 0)
                                            {
                                                gold2 = rnd.Next(0, 100000);
                                                if (rnd.Next(0, 10) == 0)
                                                {
                                                    gold2 = rnd.Next(0, 1000000);
                                                    if (rnd.Next(0, 10) == 0)
                                                    {
                                                        gold2 = rnd.Next(0, 10000000);
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    var uuu2v = rnd.Next(0, 1);
                                    for (int ir = 0; ir < uuu2v; ir++)
                                    {
                                        var ssss = ldbequip.FindById(rnd.Next(1, ldbequip.Count()));
                                        lootBack.Add(new LootBack()
                                        {
                                            namel = ssss.name, desc = ssss.desc
                                        });
                                    }

                                    lootBack.Add(new LootBack()
                                    {
                                        namel = "Money", estval = gold2.ToString("0g 0s 0c"),
                                    });
                                    break;
                                case "duneasy":
                                    var d = 1;
                                    for (int i = 0; i < d; i++)
                                    {
                                        var it = rnd.Next(0, 100);
                                        switch (it + 1)
                                        {
                                            case var _ when it is <= 1 and >= 1: //1
                                                lootBack.Add(veryrare[rnd.Next(0, veryrare.Count())]);
                                                break;
                                            case var _ when it is <= 10 and >= 2: //9
                                                lootBack.Add(rare[rnd.Next(0, rare.Count())]);
                                                break;
                                            case var _ when it is <= 20 and >= 11: //10
                                                lootBack.Add(uncommon[rnd.Next(0, uncommon.Count())]);
                                                break;
                                            case var _ when it is <= 50 and >= 21: //80
                                                lootBack.Add(common[rnd.Next(0, common.Count())]);
                                                break;
                                            case var _ when it is <= 100 and >= 51: //50
                                                lootBack.Add(mundane[rnd.Next(0, mundane.Count())]);
                                                break;
                                        }
                                    }

                                    int gold2v = 0;
                                    gold2v = rnd.Next(0, 100);
                                    if (rnd.Next(0, 10) >= 5)
                                    {
                                        gold2v = rnd.Next(0, 1000);
                                        if (rnd.Next(0, 10) == 0)
                                        {
                                            gold2v = rnd.Next(0, 10000);
                                            if (rnd.Next(0, 10) == 0)
                                            {
                                                gold2v = rnd.Next(0, 100000);
                                                if (rnd.Next(0, 10) == 0)
                                                {
                                                    gold2v = rnd.Next(0, 1000000);
                                                    if (rnd.Next(0, 10) == 0)
                                                    {
                                                        gold2v = rnd.Next(0, 10000000);
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    var uuu2 = rnd.Next(0, 1);
                                    for (int ir = 0; ir < uuu2; ir++)
                                    {
                                        var ssss = ldbequip.FindById(rnd.Next(1, ldbequip.Count()));
                                        lootBack.Add(new LootBack()
                                        {
                                            namel = ssss.name, desc = ssss.desc
                                        });
                                    }

                                    lootBack.Add(new LootBack()
                                    {
                                        namel = "Money", estval = gold2v.ToString("0g 0s 0c"),
                                    });
                                    break;
                            }

                            break;
                    }

                    return lootBack;
                }
            }
        }
    }
}