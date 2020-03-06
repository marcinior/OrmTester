using OrmTesterLib.IOService;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace OrmTesterDesktop.Views
{
    /// <summary>
    /// Logika interakcji dla klasy ResultsView.xaml
    /// </summary>
    public partial class ResultsView : Window
    {

        public MainWindowViewModel ViewModel { get; set; }
        private OrmTesterIOService ioService;
        public ResultsView()
        {
            InitializeComponent();
        }
        public ResultsView(MainWindowViewModel viewModel) : this()
        {
            ViewModel = viewModel;
            this.DataContext = viewModel;
            ioService = new OrmTesterIOService(CultureInfo.CurrentUICulture);
        }

        private void ExportToFileButton_Click(object sender, RoutedEventArgs e)
        {
            ioService.SaveTestToFile(Directory.GetCurrentDirectory(), ViewModel.TestResults);
        }

        private void ExportToCsvButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog()
            {
                Filter = "Excel File (*.xlsx)|*.xlsx"
            };
            var dialogResult = dialog.ShowDialog();
            if (dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                ioService.SaveTestToExcel(dialog.FileName, ViewModel.EFResults, ViewModel.NHibernateResults);
            }
        }
    }
}
