using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sudoku_Solver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Sudoku9X9Solver obj; // Sudoku solver instance
        private readonly DataProvider.IDataProvider dataProvider; // Data provider
        private int[,] Matrix; // input data
        private List<string> MatrixLst; // Input data in the list for ui
       
        /// <summary>
        /// constructor
        /// </summary>
        public  MainWindow()
        {
            InitializeComponent();
            InitLogger();
            dataProvider = DataProvider.DataProviderFactory.Create();
            var validator = Validator.InputValidatorFactory.Create();
            obj = new Sudoku9X9Solver(validator);

            Serilog.Log.Information("Application loaded");
        }


        #region "Form events"

        /// <summary>
        /// Sudoku solver process event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnSolveSudoku_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var timeTracker = System.Diagnostics.Stopwatch.StartNew();
                Serilog.Log.Information("btnSolveSudoku_Click clicked");
                btnSolveSudoku.IsEnabled = false;
                var (res, outputMatrix) = await obj.Process(Matrix).ConfigureAwait(false);
                if (res)
                {
                    Serilog.Log.Information($"TIMETAKEN::{timeTracker.ElapsedMilliseconds} ms");
                    Serilog.Log.Information("Sudoku solved successfully");
                    SuccessMessage();
                    Dispatcher.Invoke(() =>
                    {
                        MatrixLst = ArrayToList(outputMatrix);
                        dataView.ItemsSource = MatrixLst;
                    });
                }
                else
                {
                    FailureMessage();
                }
            }
            catch(DataProvider.DataProviderException dataProviderEx)
            {
                Serilog.Log.Error(dataProviderEx,"btnSolveSudoku_Click");
                FailureMessage();
            }
            catch(Validator.InputValidatorException validatorEx)
            {
                Serilog.Log.Error(validatorEx, "btnSolveSudoku_Click");
                FailureMessage();
            }
            catch(Exception ex)
            {
                Serilog.Log.Error(ex, "btnSolveSudoku_Click");
                FailureMessage();
            }
            finally
            {
                Dispatcher.Invoke(() =>
                {
                    btnSolveSudoku.IsEnabled = true;
                });
            }

            void SuccessMessage()
            {
                Dispatcher.Invoke(() =>
                {
                    lblMessage.Content = "Success!";
                    lblMessage.Foreground = Brushes.Green;
                });
            }

            void FailureMessage()
            {
                Dispatcher.Invoke(() =>
                {
                    lblMessage.Content = "Failed!";
                    lblMessage.Foreground = Brushes.Red;
                });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnReset_Click(object sender, RoutedEventArgs e)
        {
            await LoadData();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
           await LoadData();
            Serilog.Log.Information("Application default data loaded");
        }

        #endregion "Form events"

        #region "Private methods"

        /// <summary>
        /// Load data from the source
        /// </summary>
        /// <returns></returns>
        private async Task LoadData()
        {
            Matrix = await dataProvider.GetData().ConfigureAwait(false);

            Dispatcher.Invoke(() =>
            {
                MatrixLst = ArrayToList(Matrix);
                dataView.ItemsSource = MatrixLst;
                lblMessage.Content = "";
            });
        }


        /// <summary>
        /// Initialize logger for the application
        /// </summary>
        private void InitLogger()
        {
            var path = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = System.IO.Path.Combine(path, "logs", "SudokuSolverApplog-{Date}.txt");
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .WriteTo.RollingFile(path)
               .CreateLogger();
        }

        /// <summary>
        /// 2D array to List
        /// </summary>
        /// <param name="matrix">2d Array</param>
        /// <returns>List of the data</returns>
        private List<String> ArrayToList(int[,] matrix)
        {
            var tmp = new List<String>();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {

                    if (matrix[i, j] == 0)
                    {
                        tmp.Add(" ");
                    }
                    else
                        tmp.Add(matrix[i, j].ToString());
                }
            }

            return tmp;
        }

        #endregion "Private methods"
    }
}
