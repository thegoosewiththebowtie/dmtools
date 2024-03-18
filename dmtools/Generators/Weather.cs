using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Config.Net;
using dmtools.Views;

namespace dmtools.Generators;

public class Weather
{
    public static int profid { get; set; }
    public static List<string> weather = new List<string>()
    {
        $"{Application.Current.FindResource("sunny")}",
        $"{Application.Current.FindResource("partiallycloudy")}",
        $"{Application.Current.FindResource("overcast")}",
        $"{Application.Current.FindResource("fog")}",
        $"{Application.Current.FindResource("drizzle")}",
        $"{Application.Current.FindResource("rain")}",
        $"{Application.Current.FindResource("storm")}",
        $"{Application.Current.FindResource("thunderstorm")}"
    };
    public static string ChangeWeather()
    {
        Random rndd = new Random();
        Profile profile = new ConfigurationBuilder<Profile>().UseIniFile("Profile.ini").Build();
        profid = profile.ProfileID;
        var settings = new ConfigurationBuilder<ISettings>().UseIniFile("Settings/q0" + profid +".ini").Build();
        var i = 0;
        foreach (var wn in weather)
        {
            if (wn == settings.Weather)
            {
                break;
            }
            i += 1;
        }
        var newweather = 0;
        if (i == 0 || i == 1)
        {
            newweather = rndd.Next(0, 3);
        }
        else if (i == 7 || i == 8)
        {
            newweather = rndd.Next(5, 8);
        }
        else if (i == 9)
        {
            newweather = rndd.Next(0, 8);
        }
        else
        {
            newweather = rndd.Next(i - 2, i + 2);
        }
        return weather[newweather];
    }
    
}