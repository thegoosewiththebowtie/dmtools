using System;
using System.Diagnostics;
using dmtools.Views;
using LiteDB;

namespace dmtools.GlossData;

public class test
{
    public static void test0()
    {
        using (var ldb = new LiteDatabase("GlossData/Spells.db"))
        {
            var spellss = ldb.GetCollection<Spells>();
            foreach (var s in spellss.FindAll())
            {
                Debug.WriteLine(s.desc);
            }
        }
        using (var ldb = new LiteDatabase("GlossData/Bestiary.db"))
        {
            var beastss = ldb.GetCollection<Bestiary>();
            foreach (var b in beastss.FindAll())
            {
                Debug.WriteLine(b.desc);
            }
        }
    }
}