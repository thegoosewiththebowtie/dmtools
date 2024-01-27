using System;
using System.Collections.Generic;
using System.Globalization;

namespace dmtools.Generators;

public class NPC
{
        public static List<string> vowels = new List<string>()
        {
           "a", "e", "i", "o", "u",
        };
        public static List<string> constants = new List<string>()
        {
           "a","b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "y", "z"
        };
        public static List<string> letters = new List<string>()
        {
           "a", "e", "i", "o", "u", "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "y", "z"
        };
        public static List<string> statnames = new List<string>()
        {
            "Strength - ", "Dexterity - ", "Constitution - ", "Intelligence - ", "Wisdom - ", "Charisma - "
        };
        public static List<string> Races = new List<string>()
        {
            "Gnome", "Dwarf", "Half - orc", "Halfling", "Half-elf", "Tiefling", "Human", "Elf"
        };
        public static List<string> Eyecolor = new List<string>()
        {
            "Green", "Blue", "Brown", "Black", "Red", "White", "Silver", "Yellow"
        };
        public static List<string> Hairlength = new List<string>()
        {
            "Bald", "Short", "Long"
        };
        public static List<string> HairColor = new List<string>()
        {
            "Blond", "Light Brown", "Brown", "Dark Brown", "Colorful"
        };

        public static List<string> BodyTypes = new List<string>()
        {
            "Extremely skinny", "Skinny", "Regular", "Overweight", "Extremely Overweight"
        };
        public static Random rnd = new Random();
        public static List<string> CreateNPC(string MinMaxAge)
        {
            string n = Name();
            string sn = Name();
            List<string> stats = new List<string>();
            for (int i = 0; i < 6; i++)
            {
                stats.Add(statnames[i] + Convert.ToString(Stats()));
            }
            List<string> NPC = new List<string>()
            {
                Name(), Name(),
            };
            string MinMaxAge0 = MinMaxAge;
            NPC.AddRange(Appearance(MinMaxAge0));
            return NPC;
        }
        public static string Name()
        {
            List<string> firstname = new List<string>();
            int namelen = rnd.Next(2, 9);
            firstname.Add(letters[rnd.Next(1, letters.Count)]);
            for (int i = 0; i < namelen; i++)
            {
                if (vowels.Contains(firstname[i]))
                {
                    int n = rnd.Next(0, 50);
                    if (n == 0)
                    {
                        firstname.Add(vowels[rnd.Next(0, vowels.Count)]);
                    }
                    else
                    {
                        firstname.Add(constants[rnd.Next(0, constants.Count)]);
                    }  
                }
                else if (constants.Contains(firstname[i]))
                {
                    int n = rnd.Next(0, 50);
                    if (n == 0)
                    {
                        firstname.Add(constants[rnd.Next(0, constants.Count)]);

                    }
                    else
                    {
                        firstname.Add(vowels[rnd.Next(0, vowels.Count)]);
                    }
                }
            }
            firstname[0] = firstname[0].ToUpper();
            return string.Join("",firstname.ToArray());
        }
        public static int Stats()
        {
            int stat = rnd.Next(3, 18);
            return stat;
        }

        public static List<string> Appearance(string MinMaxAge)
        {
            string Race = Races[rnd.Next(0, Races.Count)];
            
            int MinAge = Convert.ToInt32(MinMaxAge.Split("a")[1]);
            int MaxAge = Convert.ToInt32(MinMaxAge.Split("a")[2]);
            int age = rnd.Next(MinAge,MaxAge);
            
            string hair = HairColor[rnd.Next(0, HairColor.Count)] + ", " + Hairlength[rnd.Next(0, Hairlength.Count)];
            
            int height = 0;
            
            string bodytype = BodyTypes[rnd.Next(0, BodyTypes.Count)];
            
            string eyecolor = "0";
            
            
            if (Race == Races[0]) //gnome
            {
                height = rnd.Next(85, 122);
                age = age/2;
                eyecolor = Eyecolor[rnd.Next(0, 1)];
            }
            else if (Race == Races[3]) //halfling
            {
                height = rnd.Next(85, 122);
                age = (int)Math.Ceiling(age * 0.17);
                eyecolor = Eyecolor[2];
            }
            else if (Race == Races[1]) //dwarf
            {
                height = rnd.Next(120, 160);
                age = (int)Math.Ceiling(age * 0.35);
                eyecolor = Eyecolor[rnd.Next(0, 3)];
            }
            else if (Race == Races[2]) //half-orc
            {
                height = rnd.Next(170, 200);
                age = (int)Math.Ceiling(age * 0.75);
                eyecolor = Eyecolor[rnd.Next(0, 4)];
            }
            else if (Race == Races[4]) //half-elf
            {
                height = rnd.Next(155, 190);
                age = (int)Math.Ceiling(age * 0.02);
                eyecolor = Eyecolor[rnd.Next(0, 4)];
            }
            else if (Race == Races[5]) //tiefling
            {
                height = rnd.Next(155, 190);
                age = (int)Math.Ceiling(age * 0.1);
                eyecolor = Eyecolor[rnd.Next(3, 6)];
            }
            else if (Race == Races[6]) //human
            {
                height = rnd.Next(155, 190);
                age = (int)Math.Ceiling(age * 0.08);
                eyecolor = Eyecolor[rnd.Next(0, 4)];
            }
            else if (Race == Races[7]) //elf
            {
                height = rnd.Next(150, 185);
                age = (int)Math.Ceiling(age * 0.75);
                eyecolor = Eyecolor[rnd.Next(0, 4)];
            }

            List<string> appres = new List<string>()
            {
                Race, age.ToString(), hair, height.ToString(), bodytype, eyecolor, 
                
                
                Stats().ToString(), Stats().ToString(), Stats().ToString(), Stats().ToString(), Stats().ToString(), Stats().ToString(),
            };
            return appres;
        }
}