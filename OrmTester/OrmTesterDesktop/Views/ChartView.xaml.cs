using LiveCharts;
using OrmTesterLib.StatisticParametersCalculator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

        public ChartView()
        {            
            InitializeComponent();
            DataContext = this;
            SeriesCollection = new SeriesCollection();
            Formatter = value => value.ToString("N");            
        }

        public void SaveChart()
        {
            using (var dialog = new SaveFileDialog()) 
            {
                dialog.Filter = "Images (*.png)|*.png";
                
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    OrmTesterLib.IOService.ChartService.SaveToPng(Chart, dialog.FileName);
                }
            }
        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SaveChart();
        }
    }
}
