using OrmTesterDesktop.ViewModels;
using OrmTesterLib.IOService;
using System;
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
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }
        public ResultsView(MainWindowViewModel viewModel) : this()
        {
            ViewModel = viewModel;
            this.DataContext = viewModel;
            ioService = new OrmTesterIOService(CultureInfo.CurrentUICulture);
        }

        private void ExportToFileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ioService.SaveTestToFile(Directory.GetCurrentDirectory(), ViewModel.TestResults);
                this.DisplayMessage(Properties.Resources.SaveFileSuccess);
            }
            catch(Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
        }

        private void ExportToCsvButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialog = new SaveFileDialog()
                {
                    Filter = "Excel File (*.xlsx)|*.xlsx"
                };
                var dialogResult = dialog.ShowDialog();
                if (dialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    ioService.SaveTestToExcel(dialog.FileName, ViewModel.EFResults, ViewModel.NHibernateResults);
                    this.DisplayMessage(Properties.Resources.ExportToExcelSuccess);
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
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
    }
}
