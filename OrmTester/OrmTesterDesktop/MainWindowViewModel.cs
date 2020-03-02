using OrmTesterDesktop.Commands;
using OrmTesterDesktop.Services;
using OrmTesterLib.Enums;
using OrmTesterLib.StatisticParametersCalculator;
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
            this.chartGenerator = new ChartGenerationHelper();
            this.TestResults = new List<StatisticParameter>();            
            this.AverageCommand = new CreateChartCommand(chartGenerator.GenerateAverageBarChart);
            this.SDCommand = new CreateChartCommand(chartGenerator.GenerateStandardDeviationBarChart);
            this.CoVCommand = new CreateChartCommand(chartGenerator.GenerateCoefficentOfVariationBarChart);
        }

        public bool AreCreateButtonsAvailable { get => this.TestResults.Any(test => test.OperationType == OperationType.Create); }
        public bool AreUpdateButtonsAvailable { get => this.TestResults.Any(test => test.OperationType == OperationType.Update); }
        public bool AreDeleteButtonsAvailable { get => this.TestResults.Any(test => test.OperationType == OperationType.Delete); }

        public CreateChartCommand AverageCommand { get; set; }
        public CreateChartCommand SDCommand { get; set; }
        public CreateChartCommand CoVCommand { get; set; }

        private List<StatisticParameter> testResults;
        private bool isExecuteButtonActive;
        private ChartGenerationHelper chartGenerator;

        public List<StatisticParameter> TestResults
        {
            get => testResults;
            set
            {
                testResults = value;
                this.chartGenerator.StatisticParameters = value;
                NotifyPropertyChanged(nameof(TestResults));
                NotifyPropertyChanged(nameof(AreCreateButtonsAvailable));
                NotifyPropertyChanged(nameof(AreUpdateButtonsAvailable));
                NotifyPropertyChanged(nameof(AreDeleteButtonsAvailable));
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
