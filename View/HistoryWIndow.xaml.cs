using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CalculatorProject.Model;

namespace CalculatorProject.View
{
    /// <summary>
    /// Interaction logic for HistoryWIndow.xaml
    /// </summary>
    public partial class HistoryWIndow : Window
    {
        private CalculatorViewModel _viewModel;
        public HistoryWIndow()
        {
            InitializeComponent();
            _viewModel = (CalculatorViewModel)Application.Current.MainWindow.DataContext;
            DataContext = _viewModel;
            HistoryListView.ItemsSource = _viewModel.History;

        }

        private void ClearHistory_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ClearHistory();
            HistoryListView.ItemsSource = _viewModel.History;
        }
    }
}
