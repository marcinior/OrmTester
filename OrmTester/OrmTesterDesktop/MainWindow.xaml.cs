using NHibernateTester;
using OrmTesterDesktop.Services;
using OrmTesterDesktop.Views;
using OrmTesterLib.IOService;
using OrmTesterLib.StatisticParametersCalculator;
using OrmTesterLib.TestCore;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using CButton = System.Windows.Controls.Button;

namespace OrmTesterDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel { get; set; }
        private OrmTesterIOService ioService;
        public MainWindow()
        {
            ioService = new OrmTesterIOService();
            var appView = new ChooseApplicationModeView();
            ViewModel = new MainWindowViewModel();
            appView.ShowDialog();
            if(appView.DialogResult == false)
            {
                this.Close();
            }

            if (!string.IsNullOrEmpty(appView.FilePath))
            {
                this.ViewModel.TestResults = ioService.LoadTestFromFile(appView.FilePath);
            }
            
            DataContext = this;
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"\d+");
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void ExecuteTestsButton_Click(object sender, RoutedEventArgs e)
        {
            if(sender is CButton button)
            {
                button.IsEnabled = false;
            }
            var builder = new TestParametersBuilder();

            GatherCreateOperations(builder);

            GatherDeleteOperations(builder);

            GatherUpdateOperations(builder);

            Task.Factory.StartNew(() =>
            {
                var nHibernateTester = new NHibernateTestOperations(builder);
                // var entityFrameworkTester = new EntityFrameworkTester(builder);
                var nHibernateTestResults = nHibernateTester.RunTests(nHibernateTester);
                //var entityFrameworkTestResults = entityFrameworkTester.RunTests(entityFrameworkTester);
                StatisticParametersCalculator stat = new StatisticParametersCalculator();
                this.ViewModel.TestResults = stat.CalculateStatisticParameters(nHibernateTestResults, nHibernateTestResults, CultureInfo.CurrentUICulture);
                Dispatcher.Invoke(() =>
                {
                    if (sender is CButton button1)
                    {
                        button1.IsEnabled = true;
                    }
                    ChartGenerationHelper service = new ChartGenerationHelper
                    {
                        StatisticParameters = this.ViewModel.TestResults
                    };
                });
            });            
        }

        private void GatherCreateOperations(TestParametersBuilder builder)
        {
            GatherCreateManyToManyOperations(builder);

            GatherCreateOneToManyOperations(builder);

            GatherCreateOneToOneOperation(builder);

            GatherCreateNoRelationshipOperation(builder);
        }

        private void GatherCreateManyToManyOperations(TestParametersBuilder builder)
        {
            if (ManyToMany.IsChecked == true)
            {
                if (ManyToManyBulk.IsChecked == true)
                {
                    if (int.TryParse(ManyToManyRepetitionsBulk.Text, out var repetitions))
                    {
                        builder.TestBulkCreateManyToMany(repetitions);
                    }
                    else
                    {
                        builder.TestBulkCreateManyToMany();
                    }
                }
                if (ManyToManySingle.IsChecked == true)
                {
                    if (int.TryParse(ManyToManyRepetitionsSingle.Text, out var repetitions))
                    {
                        builder.TestSingleCreateManyToMany(repetitions);
                    }
                    else
                    {
                        builder.TestSingleCreateManyToMany();
                    }
                }
            }
        }

        private void GatherCreateOneToManyOperations(TestParametersBuilder builder)
        {
            if (OneToMany.IsChecked == true)
            {
                if (OneToManyBulk.IsChecked == true)
                {
                    if (int.TryParse(OneToManyRepetitionsBulk.Text, out var repetitions))
                    {
                        builder.TestBulkCreateOneToMany(repetitions);
                    }
                    else
                    {
                        builder.TestBulkCreateOneToMany();
                    }
                }
                if (OneToManySingle.IsChecked == true)
                {
                    if (int.TryParse(OneToManyRepetitionsSingle.Text, out var repetitions))
                    {
                        builder.TestSingleCreateOneToMany(repetitions);
                    }
                    else
                    {
                        builder.TestSingleCreateOneToMany();
                    }
                }
            }
        }

        private void GatherCreateOneToOneOperation(TestParametersBuilder builder)
        {
            if (OneToOne.IsChecked == true)
            {
                if (OneToOneBulk.IsChecked == true)
                {
                    if (int.TryParse(OneToOneRepetitionsBulk.Text, out var repetitions))
                    {
                        builder.TestBulkCreateOneToOne(repetitions);
                    }
                    else
                    {
                        builder.TestBulkCreateOneToOne();
                    }
                }
                if (OneToOneSingle.IsChecked == true)
                {
                    if (int.TryParse(OneToOneRepetitionsSingle.Text, out var repetitions))
                    {
                        builder.TestSingleCreateOneToOne(repetitions);
                    }
                    else
                    {
                        builder.TestSingleCreateOneToOne();
                    }
                }
            }
        }

        private void GatherCreateNoRelationshipOperation(TestParametersBuilder builder)
        {
            if (None.IsChecked == true)
            {
                if (NoneBulk.IsChecked == true)
                {
                    if (int.TryParse(NoneRepetitionsBulk.Text, out var repetitions))
                    {
                        builder.TestBulkCreateNoRelationship(repetitions);
                    }
                    else
                    {
                        builder.TestBulkCreateNoRelationship();
                    }
                }
                if (NoneSingle.IsChecked == true)
                {
                    if (int.TryParse(NoneRepetitionsSingle.Text, out var repetitions))
                    {
                        builder.TestSingleCreateNoRelationship(repetitions);
                    }
                    else
                    {
                        builder.TestSingleCreateNoRelationship();
                    }
                }
            }
        }
        
        private void GatherDeleteOperations(TestParametersBuilder builder)
        {
            GatherDeleteManyToManyOperation(builder);

            GatherDeleteOneToManyOperation(builder);

            GatherDeleteOneToOneOperation(builder);

            GatherDeleteNoRelationshipOperation(builder);
        }

        private void GatherDeleteManyToManyOperation(TestParametersBuilder builder)
        {
            if (ManyToManyDelete.IsChecked == true)
            {
                if (ManyToManyDeleteBulk.IsChecked == true)
                {
                    if (int.TryParse(ManyToManyDeleteRepetitionsBulk.Text, out var repetitions))
                    {
                        builder.TestBulkDeleteManyToMany(repetitions);
                    }
                    else
                    {
                        builder.TestBulkDeleteManyToMany();
                    }
                }
                if (ManyToManyDeleteSingle.IsChecked == true)
                {
                    if (int.TryParse(ManyToManyDeleteRepetitionsSingle.Text, out var repetitions))
                    {
                        builder.TestSingleDeleteManyToMany(repetitions);
                    }
                    else
                    {
                        builder.TestSingleDeleteManyToMany();
                    }
                }
            }
        }

        private void GatherDeleteOneToManyOperation(TestParametersBuilder builder)
        {
            if (OneToManyDelete.IsChecked == true)
            {
                if (OneToManyDeleteBulk.IsChecked == true)
                {
                    if (int.TryParse(OneToManyDeleteRepetitionsBulk.Text, out var repetitions))
                    {
                        builder.TestBulkDeleteOneToMany(repetitions);
                    }
                    else
                    {
                        builder.TestBulkDeleteOneToMany();
                    }
                }
                if (OneToManyDeleteSingle.IsChecked == true)
                {
                    if (int.TryParse(OneToManyDeleteRepetitionsSingle.Text, out var repetitions))
                    {
                        builder.TestSingleDeleteOneToMany(repetitions);
                    }
                    else
                    {
                        builder.TestSingleDeleteOneToMany();
                    }
                }
            }
        }

        public void GatherDeleteOneToOneOperation(TestParametersBuilder builder)
        {
            if (OneToOneDelete.IsChecked == true)
            {
                if (OneToOneDeleteBulk.IsChecked == true)
                {
                    if (int.TryParse(OneToOneDeleteRepetitionsBulk.Text, out var repetitions))
                    {
                        builder.TestBulkDeleteOneToOne(repetitions);
                    }
                    else
                    {
                        builder.TestBulkDeleteOneToOne();
                    }
                }
                if (OneToOneDeleteSingle.IsChecked == true)
                {
                    if (int.TryParse(OneToOneRepetitionsSingle.Text, out var repetitions))
                    {
                        builder.TestSingleDeleteOneToOne(repetitions);
                    }
                    else
                    {
                        builder.TestSingleDeleteOneToOne();
                    }
                }
            }
        }

        public void GatherDeleteNoRelationshipOperation(TestParametersBuilder builder)
        {
            if (NoneRelationshipDelete.IsChecked == true)
            {
                if (NoneRelationshipDeleteBulk.IsChecked == true)
                {
                    if (int.TryParse(NoneRelationshipDeleteRepetitionsBulk.Text, out var repetitions))
                    {
                        builder.TestBulkDeleteNoRelationship(repetitions);
                    }
                    else
                    {
                        builder.TestBulkDeleteNoRelationship();
                    }
                }
                if (NoneRelationshipDeleteSingle.IsChecked == true)
                {
                    if (int.TryParse(NoneRelationshipDeleteRepetitionsSingle.Text, out var repetitions))
                    {
                        builder.TestSingleDeleteNoRelationship(repetitions);
                    }
                    else
                    {
                        builder.TestSingleDeleteNoRelationship();
                    }
                }
            }
        }
        
        private void GatherUpdateOperations(TestParametersBuilder builder)
        {
            GatherUpdateManyToManyOperation(builder);

            GatherUpdateOneToManyOperation(builder);

            GatherUpdateOneToOneOperation(builder);

            GatherUpdateNoRelationshipOperation(builder);

        }

        private void GatherUpdateManyToManyOperation(TestParametersBuilder builder)
        {
            if (ManyToManyUpdate.IsChecked == true)
            {
                if (ManyToManyUpdateBulk.IsChecked == true)
                {
                    if (int.TryParse(ManyToManyUpdateRepetitionsBulk.Text, out var repetitions))
                    {
                        builder.TestBulkUpdateManyToMany(repetitions);
                    }
                    else
                    {
                        builder.TestBulkUpdateManyToMany();
                    }
                }
                if (ManyToManyUpdateSingle.IsChecked == true)
                {
                    if (int.TryParse(ManyToManyUpdateRepetitionsSingle.Text, out var repetitions))
                    {
                        builder.TestSingleUpdateManyToMany(repetitions);
                    }
                    else
                    {
                        builder.TestSingleUpdateManyToMany();
                    }
                }
            }
        }

        private void GatherUpdateOneToManyOperation(TestParametersBuilder builder)
        {
            if (OneToManyUpdate.IsChecked == true)
            {
                if (OneToManyUpdateBulk.IsChecked == true)
                {
                    if (int.TryParse(OneToManyUpdateRepetitionsBulk.Text, out var repetitions))
                    {
                        builder.TestBulkUpdateOneToMany(repetitions);
                    }
                    else
                    {
                        builder.TestBulkUpdateOneToMany();
                    }
                }
                if (OneToManyUpdateSingle.IsChecked == true)
                {
                    if (int.TryParse(OneToManyUpdateRepetitionsSingle.Text, out var repetitions))
                    {
                        builder.TestSingleUpdateOneToMany(repetitions);
                    }
                    else
                    {
                        builder.TestSingleUpdateOneToMany();
                    }
                }
            }
        }

        private void GatherUpdateOneToOneOperation(TestParametersBuilder builder)
        {
            if (OneToOneUpdate.IsChecked == true)
            {
                if (OneToOneUpdateBulk.IsChecked == true)
                {
                    if (int.TryParse(OneToOneUpdateRepetitionsBulk.Text, out var repetitions))
                    {
                        builder.TestBulkUpdateOneToOne(repetitions);
                    }
                    else
                    {
                        builder.TestBulkUpdateOneToOne();
                    }
                }
                if (OneToOneUpdateSingle.IsChecked == true)
                {
                    if (int.TryParse(OneToOneUpdateRepetitionsSingle.Text, out var repetitions))
                    {
                        builder.TestSingleUpdateOneToOne(repetitions);
                    }
                    else
                    {
                        builder.TestSingleUpdateOneToOne();
                    }
                }
            }
        }

        private void GatherUpdateNoRelationshipOperation(TestParametersBuilder builder)
        {
            if (NoneRelationshipUpdate.IsChecked == true)
            {
                if (NoneRelationshipUpdateBulk.IsChecked == true)
                {
                    if (int.TryParse(NoneRelationshipUpdateRepetitionsBulk.Text, out var repetitions))
                    {
                        builder.TestBulkUpdateNoRelationship(repetitions);
                    }
                    else
                    {
                        builder.TestBulkUpdateNoRelationship();
                    }
                }
                if (NoneRelationshipUpdateSingle.IsChecked == true)
                {
                    if (int.TryParse(NoneRelationshipUpdateRepetitionsSingle.Text, out var repetitions))
                    {
                        builder.TestSingleUpdateNoRelationship(repetitions);
                    }
                    else
                    {
                        builder.TestSingleUpdateNoRelationship();
                    }
                }
            }
        }

        private void ExportToFileButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            var dialogResult = dialog.ShowDialog();
            if(dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                ioService.SaveTestToFile(dialog.SelectedPath, ViewModel.TestResults);
            }
        }

        private void ExportToCsvButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TestTypeCheckbox_Click(object sender, RoutedEventArgs e)
        {
            this.ViewModel.IsExecuteButtonActive = CheckIfButtonShouldBeEnabled();
        }

        private bool CheckIfButtonShouldBeEnabled()
        {
            return (ManyToMany.IsChecked == true && (ManyToManyBulk.IsChecked == true || ManyToManySingle.IsChecked == true)) ||
                (OneToMany.IsChecked == true && (OneToManyBulk.IsChecked == true || OneToManySingle.IsChecked == true)) ||
                (OneToOne.IsChecked == true && (OneToOneBulk.IsChecked == true || OneToOneSingle.IsChecked == true)) ||
                (None.IsChecked == true && (NoneBulk.IsChecked == true || NoneSingle.IsChecked == true)) ||
                (ManyToManyUpdate.IsChecked == true && (ManyToManyUpdateBulk.IsChecked == true || ManyToManyUpdateSingle.IsChecked == true)) ||
                (OneToManyUpdate.IsChecked == true && (OneToManyUpdateBulk.IsChecked == true || OneToManyUpdateSingle.IsChecked == true)) ||
                (OneToOneUpdate.IsChecked == true && (OneToOneUpdateBulk.IsChecked == true || OneToOneUpdateSingle.IsChecked == true)) ||
                (NoneRelationshipUpdate.IsChecked == true && (NoneRelationshipUpdateBulk.IsChecked == true || NoneRelationshipUpdateSingle.IsChecked == true)) ||
                (ManyToManyDelete.IsChecked == true && (ManyToManyDeleteBulk.IsChecked == true || ManyToManyDeleteSingle.IsChecked == true)) ||
                (OneToManyDelete.IsChecked == true && (OneToManyDeleteBulk.IsChecked == true || OneToManyDeleteSingle.IsChecked == true)) ||
                (OneToOneDelete.IsChecked == true && (OneToOneDeleteBulk.IsChecked == true || OneToOneDeleteSingle.IsChecked == true)) ||
                (NoneRelationshipDelete.IsChecked == true && (NoneRelationshipDeleteBulk.IsChecked == true || NoneRelationshipDeleteSingle.IsChecked == true));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
