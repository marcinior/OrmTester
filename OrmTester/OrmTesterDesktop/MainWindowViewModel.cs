using OrmTesterLib.StatisticParametersCalculator;
using OrmTesterLib.TestCore;
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
        public List<TestResult> NHibernateResults { get; set; }
        public List<TestResult> EFResults { get; set; }
        private bool isExecuteButtonActive;

        public List<StatisticParameter> TestResults
        {
            get => testResults;
            set
            {
                testResults = value;
                NotifyPropertyChanged(nameof(TestResults));
            }
        }

        public bool IsExecuteButtonActive { 
            get => isExecuteButtonActive; 
            set 
            { 
                isExecuteButtonActive = value;
                NotifyPropertyChanged(nameof(IsExecuteButtonActive));
            } 
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
