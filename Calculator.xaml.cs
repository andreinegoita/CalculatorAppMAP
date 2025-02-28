using System.Windows;
using System.Windows.Controls;

namespace CalculatorProject
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new CalculatorViewModel();  
        }

        private void DigitButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var viewModel = DataContext as CalculatorViewModel;
                viewModel?.AppendDigit(button.Content.ToString());
            }
        }

        private void OperatorButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            string operatorSymbol = button.Content.ToString();
            var viewModel = DataContext as CalculatorViewModel;
            viewModel?.SetOperator(operatorSymbol);
        }

        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as CalculatorViewModel;
            viewModel?.CalculateResult();
        }
        private void ChangeSignButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as CalculatorViewModel;
            viewModel?.ChangeSign();
        }

        private void InverseButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as CalculatorViewModel;
            viewModel?.Inverse();
        }

        private void SquareButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as CalculatorViewModel;
            viewModel?.Square();
        }

        private void SquareRootButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as CalculatorViewModel;
            viewModel?.SquareRoot();
        }

        private void PercentageButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as CalculatorViewModel;
            viewModel?.Percentage();
        }


        private void DecimalButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as CalculatorViewModel;
            viewModel?.AppendDecimal();
        }

        private void BackspaceButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as CalculatorViewModel;
            viewModel?.Backspace();
        }

        private void ClearEntryButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as CalculatorViewModel;
            viewModel?.ClearEntry();
        }

        private void ClearAllButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as CalculatorViewModel;
            viewModel?.ClearAll();
        }



    }
}
