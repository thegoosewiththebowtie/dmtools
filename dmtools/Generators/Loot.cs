using System.Diagnostics;
using dmtools.Views;

namespace dmtools.Generators;

public class Loot
{
    public static void GenerateLoot(LootParameters lootParameters)
    {
        if (lootParameters.test)
        {
            Debug.WriteLine("testworks");
            
        }
    }
}