using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using GLib;

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
        public static List<string> Races = new List<string>()
        {
            "Gnome", "Dwarf", "Half - orc", "Halfling", "Half-elf", "Tiefling", "Human", "Elf"
        };
        public static List<string> Eyecolor = new List<string>()
        {
            "Green", "Blue", "Brown", "Black", "Red", "White", "Silver", "Transparent"
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
        public static List<string> DisLikes = new List<string>()
        {
            "Violence/Peace","Talking to strangers/Meet new people", "Stay alone", "Be outside", "Read", "Their eyecolor",
            "Their height", "Their weight", "To make desicions", "Sports", "People around them", "One-night stands",
            "Nature/Technology", "Red/Orange/Transparent/Green/Blue/Purple", "Kids", "Their neighbours", "Smart people/Dumb people",
            "One of your party members", "Darkness", "Spider", "Random person from their village", "Sleeping",
            "Ruler of the country/Their work/Their work/Their work",/*"","","",
            "","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","",
            "","","","","","","","",*/
            
            
        };
        public static Random rnd = new Random();
        public static List<List<string>> CreateNPC(string MinMaxAge)
        {
            string n = Name();
            string sn = Name();
            List<string> stats = new List<string>();
            for (int i = 0; i < 6; i++)
            {
                stats.Add( Convert.ToString(Stats()));
            }
            string MinMaxAge0 = MinMaxAge;
            var DLL = DisLikesR();
            List<List<string>> NPC = new List<List<string>>()
            {
                new List<string>(){n, sn},
                Appearance(MinMaxAge0),
                DLL[0], DLL[1]
            };
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

        public static List<List<string>> DisLikesR()
        {
            List<string> Dis = new List<string>();
            List<string> Likes = new List<string>();
            foreach (var dl in DisLikes)
            {
                var i = rnd.Next(0, 4);
                var dli = dl;
                var l = dl.Split("/").Length;
                if (l != null)
                {
                    var dd =rnd.Next(0, l);
                    dli = dl.Split("/")[dd];
                }
                switch (i)
                {
                    case 0:
                        Dis.Add(dli);
                        break;
                    case 1:
                        Likes.Add(dli);
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                }
            }
            List<List<string>> Return = new List<List<string>>();
            Return.Add(Dis);
            Return.Add(Likes);
            return Return;
        }
}