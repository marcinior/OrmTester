using LiveCharts;
using OrmTesterDesktop.ViewModels;
using System;
using System.Windows;
using System.Windows.Forms;

namespace OrmTesterDesktop.Views
{
    /// <summary>
    /// Logika interakcji dla klasy ChartView.xaml
    /// </summary>
    public partial class ChartView : Window
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public string YAxisLabel { get; set; }

        public ChartView(string yAxisLabel, bool maximize = false)
        {
            YAxisLabel = yAxisLabel;
            InitializeComponent();
            DataContext = this;
            SeriesCollection = new SeriesCollection();
            if (maximize)
            {
                this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            }
        }

        public void SaveChart()
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "Images (*.png)|*.png";

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {                    
                    try
                    {
                        OrmTesterLib.IOService.ChartService.SaveToPng(Chart, dialog.FileName);
                        this.DisplayMessage(Properties.Resources.SaveChartSuccess);
                    }
                    catch (Exception ex)
                    {
                        this.DisplayMessage(ex.Message);
                    }
                }
            }
        }

        private void DisplayMessage(string message)
        {
            var errorMsg = new ErrorMsgViewModel
            {
                ErrorMsg = message
            };

            this.Dispatcher.Invoke(() =>
            {
                var errorView = new ErrorMsgView(errorMsg);
                errorView.ShowDialog();
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SaveChart();
        }
    }
}
