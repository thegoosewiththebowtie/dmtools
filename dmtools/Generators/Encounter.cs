using System;
using System.Collections.Generic;
using dmtools.Views;
using DryIoc.FastExpressionCompiler.LightExpression;
using LiteDB;

namespace dmtools.Generators;

public class GendEnc
{
    
}
public class Encounter
{
    public static List<Bestiary> GenerateEncounter(double min, double max, int am)
    {
        using (var ldb = new LiteDatabase("GlossData/Bestiary.db"))
        {
            var besti = ldb.GetCollection<Bestiary>();
            List<Bestiary> fll = new List<Bestiary>();
            List<Bestiary> ret = new List<Bestiary>();
            Random rnd = new Random();
            foreach (var f in besti.FindAll())
            {
                if (min <= f.challenge_rating && f.challenge_rating <= max)
                {
                    fll.Add(f);
                }
            }
            for (int i = 0; i < am; i++)
            {
                ret.Add(fll[rnd.Next(0, fll.Count)]);
            }
            return ret;
        }
    }
}