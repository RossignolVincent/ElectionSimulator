﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ElectionSimulator
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ElectionViewModel ElectionVM { get; set; }

        public App()
        {
            ElectionVM = new ElectionViewModel();
        }
    }
}
