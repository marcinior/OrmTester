using System;
using System.Collections.Generic;
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
    /// Logika interakcji dla klasy ChooseApplicationModeView.xaml
    /// </summary>
    public partial class ChooseApplicationModeView : Window
    {
        public string FilePath { get; set; }
        public ChooseApplicationModeView()
        {
            InitializeComponent();
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Test data files (*.dat)|*.dat"
            };
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.FilePath = dialog.FileName;
                this.DialogResult = true;
                this.Close();
            }
        }

        private void StartNewTestButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
