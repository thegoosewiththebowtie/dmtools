﻿using System;
using Avalonia.Controls;
using LiteDB;
using System.IO;
using System.Net;

namespace dmtools.Views;

public partial class MainWindow : Window
{
    public static event EventHandler SizzeChanged;

    public MainWindow()
    {
        InitializeComponent();
    }
    public void Control_OnSizeChanged(object? sender, SizeChangedEventArgs e)
    {
        EventHandler eventHandler = SizzeChanged;
        eventHandler(sender, e);
    }
}