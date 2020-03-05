﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OrmTesterDesktop.Views
{
    /// <summary>
    /// Logika interakcji dla klasy ResultsView.xaml
    /// </summary>
    public partial class ResultsView : Window
    {

        public MainWindowViewModel ViewModel { get; set; }
        public ResultsView()
        {
            InitializeComponent();
        }
        public ResultsView(MainWindowViewModel viewModel) : this()
        {
            ViewModel = viewModel;
            this.DataContext = viewModel;
        }
    }
}