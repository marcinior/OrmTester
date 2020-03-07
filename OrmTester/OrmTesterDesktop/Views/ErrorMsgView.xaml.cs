using OrmTesterDesktop.ViewModels;
using System.Windows;

namespace OrmTesterDesktop.Views
{
    /// <summary>
    /// Logika interakcji dla klasy ErrorMsgView.xaml
    /// </summary>
    public partial class ErrorMsgView : Window
    {

        public ErrorMsgViewModel ViewModel { get; set; }

        public ErrorMsgView(ErrorMsgViewModel errorMsg)
        {
            this.ViewModel = errorMsg;
            this.DataContext = ViewModel;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
