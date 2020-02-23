﻿using OrmTesterLib.StatisticParametersCalculator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OrmTesterDesktop
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            this.TestResults = new List<StatisticParameter>();
        }

        private List<StatisticParameter> testResults;

        public List<StatisticParameter> TestResults
        {
            get => testResults;
            set
            {
                testResults = value;
                NotifyPropertyChanged(nameof(TestResults));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}