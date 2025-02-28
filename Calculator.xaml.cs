using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            var viewModel = DataContext as CalculatorViewModel;
            if (viewModel == null) return;

            switch (e.Key)
            {
                
                case Key.D0: case Key.NumPad0: viewModel.AppendDigit("0"); break;
                case Key.D1: case Key.NumPad1: viewModel.AppendDigit("1"); break;
                case Key.D2: case Key.NumPad2: viewModel.AppendDigit("2"); break;
                case Key.D3: case Key.NumPad3: viewModel.AppendDigit("3"); break;
                case Key.D4: case Key.NumPad4: viewModel.AppendDigit("4"); break;
                case Key.D5: case Key.NumPad5: viewModel.AppendDigit("5"); break;
                case Key.D6: case Key.NumPad6: viewModel.AppendDigit("6"); break;
                case Key.D7: case Key.NumPad7: viewModel.AppendDigit("7"); break;
                case Key.D8: case Key.NumPad8: viewModel.AppendDigit("8"); break;
                case Key.D9: case Key.NumPad9: viewModel.AppendDigit("9"); break;

                
                case Key.Add: viewModel.SetOperator("+"); break;
                case Key.Subtract: viewModel.SetOperator("-"); break;
                case Key.Multiply: viewModel.SetOperator("*"); break;
                case Key.Divide: viewModel.SetOperator("÷"); break;

                
                case Key.Enter: viewModel.CalculateResult(); break;

                
                case Key.Back: viewModel.Backspace(); break;

                
                case Key.Escape: viewModel.ClearAll(); break;

                
                case Key.OemPeriod: case Key.Decimal: viewModel.AppendDecimal(); break;
            }


        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space) e.Handled = true; 
        }

    }
}
