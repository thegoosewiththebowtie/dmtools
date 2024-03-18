using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml.Styling;
using dmtools.PopUps;
using dmtools.GlossData;
using LiteDB;

namespace dmtools.Views;

public partial class GlossaryView : UserControl
{
    public List<SpellData0> spells { get; set; }
    public GlossaryView()
    {
        InitializeComponent();
        spells = new List<SpellData0>();
        using (var ldb = new LiteDatabase("GlossData/Glossary.db"))
        {
            var spellss = ldb.GetCollection<SpellData0>();
            foreach (var spl in spellss.FindAll())
            {
                spells.Add(spl);
            }
        }
        SearchSpells.ItemsSource = spells;
        SpelsGrid.ItemsSource = spells;
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        var nno = new NO("PLEASE DO NOT THE CAT");
        nno.Height = 250;
        GlossData.convert.ConvertStartTest();
    }

    private void List_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        using (var ldb = new LiteDatabase("GlossData/Glossary.db"))
        {
            var spellss = ldb.GetCollection<SpellData0>();
            var selspl = spellss.FindById(((sender as DataGrid).SelectedItem as SpellData0).ID);
        }
    }

    private void Languagetest_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        Translate(((sender as ComboBox).SelectedItem as ComboBoxItem).Content.ToString());
    }
    
    public void Translate(string targetLanguage)
    {
        
    }
}