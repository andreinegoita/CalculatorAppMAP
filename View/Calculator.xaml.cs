using CalculatorProject.View;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CalculatorProject
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new CalculatorViewModel();
            ApplyTheme("Themes/ThemeLight.xaml");
            ConvertBaseShow.Text = "Current Base: 10";
        }

        private void ApplyTheme(string themePath)
        {
            ResourceDictionary newTheme = new ResourceDictionary
            {
                Source = new Uri(themePath, UriKind.Relative)
            };

            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(newTheme);
        }

        private void LightTheme_Click(object sender, RoutedEventArgs e)
        {
            ApplyTheme("Themes/ThemeLight.xaml");
        }

        private void DarkTheme_Click(object sender, RoutedEventArgs e)
        {
            ApplyTheme("Themes/ThemeDark.xaml");
        }

        private void BlueTheme_Click(object sender, RoutedEventArgs e)
        {
            ApplyTheme("Themes/ThemeBlue.xaml");
        }

        private void GreenTheme_Click(object sender, RoutedEventArgs e)
        {
            ApplyTheme("Themes/ThemeGreen.xaml");
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

                case Key.A: viewModel.AppendDigit("A"); break;
                case Key.B: viewModel.AppendDigit("B"); break;
                case Key.C: viewModel.AppendDigit("C"); break;
                case Key.D: viewModel.AppendDigit("D"); break;
                case Key.E: viewModel.AppendDigit("E"); break;
                case Key.F: viewModel.AppendDigit("F"); break;


                case Key.Add: viewModel.SetOperator("+"); break;
                case Key.Subtract: viewModel.SetOperator("-"); break;
                case Key.Multiply: viewModel.SetOperator("*"); break;
                case Key.Divide: viewModel.SetOperator("÷"); break;

                
                case Key.Enter: viewModel.CalculateResult(); break;

                
                case Key.Back: viewModel.Backspace(); break;

                
                case Key.Escape: viewModel.ClearAll(); break;

                
                case Key.OemPeriod: case Key.Decimal: viewModel.AppendDecimal(); break;
            }

            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                switch (e.Key)
                {
                    case Key.C:
                        viewModel.CopyText();
                        break;
                    case Key.X:
                        viewModel.CutText();
                        break;
                    case Key.V:
                        viewModel.PasteText();
                        break;
                }
            }

        }

        private void StandardMode_Click(object sender, RoutedEventArgs e)
        {
            OperatorDivideBy1.IsEnabled = true;
            Operatorx2.IsEnabled = true;
            OperatorModulo.IsEnabled = true;
            OperatorSquare.IsEnabled = true;
            ConvertOption.IsEnabled = false;
            ConvertOption.Background = Brushes.White;
            ConvertOption.Foreground = Brushes.White;
            ConvertOption.BorderBrush = Brushes.White;
            var viewModel = DataContext as CalculatorViewModel;
            if (viewModel != null)
            {
                viewModel.CurrentBase = 10;
                ConvertBaseShow.Text = "Current Base: 10";
                Properties.Settings.Default.LastBaseUsed = "10";
                Properties.Settings.Default.Save();
            }
            SelectedOptionText.Text = "Standard Mode";  
            Properties.Settings.Default.LastSelectedMode = "Standard";
            Properties.Settings.Default.Save();
        }

        private void ProgrammingMode_Click(object sender, RoutedEventArgs e)
        {
            OperatorDivideBy1.IsEnabled = false;
            Operatorx2.IsEnabled = false;
            OperatorModulo.IsEnabled = false;
            OperatorSquare.IsEnabled = false;
            ConvertOption.IsEnabled = true;
            ConvertOption.Background = Brushes.Gray;
            ConvertOption.Foreground = Brushes.White;
            ConvertOption.BorderBrush = Brushes.White;
            SelectedOptionText.Text = "Programming Mode";  
            Properties.Settings.Default.LastSelectedMode = "Programming";
            Properties.Settings.Default.Save();

        }

        



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string lastSelectedMode = Properties.Settings.Default.LastSelectedMode;

            if (lastSelectedMode == "Standard")
            {
                StandardMode_Click(null, null);
            }
            else if (lastSelectedMode == "Programming")
            {
                ProgrammingMode_Click(null, null);
            }
        }



        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space) e.Handled = true; 
        }

        private void MemoryAddButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as CalculatorViewModel;
            viewModel?.MemoryAdd();
        }

        private void MemorySubtractButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as CalculatorViewModel;
            viewModel?.MemorySubtract();
        }

        private void MemoryStoreButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as CalculatorViewModel;
            viewModel?.MemoryStore();
        }

        private void MemoryRecallButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as CalculatorViewModel;
            viewModel?.MemoryRecall();
        }

        private void MemoryShowButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as CalculatorViewModel;
            if (viewModel != null && viewModel.MemoryStack.Count > 0)
            {
                MemoryWindow memoryWindow = new MemoryWindow(viewModel.MemoryStack);
                if (memoryWindow.ShowDialog() == true)
                {
                    if (memoryWindow.SelectedValue.HasValue)
                    {
                        viewModel.DisplayText = memoryWindow.SelectedValue.Value.ToString();
                    }
                }
            }
            else
            {
                MessageBox.Show("Memoria este goală!", "Atenție", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void MemoryClearButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as CalculatorViewModel;
            viewModel?.MemoryClear();
        }


        private void AboutShowButton_Click(object sender, RoutedEventArgs e)
        {
                About aboutWindow = new About();
                aboutWindow.Show();
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as CalculatorViewModel;
            viewModel?.CopyText();
        }

        private void CutButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as CalculatorViewModel;
            viewModel?.CutText();
        }

        private void PasteButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as CalculatorViewModel;
            viewModel?.PasteText();
        }


        private void DigitGroupingButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as CalculatorViewModel;
            if (viewModel != null)
            {
                viewModel.ToggleDigitGrouping();
                viewModel.ReformatDisplay();
            }
        }

        private void Base2_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as CalculatorViewModel;
            if (viewModel != null)
            {
                viewModel.CurrentBase = 2;
                ConvertBaseShow.Text = "Current Base: 2";
                Properties.Settings.Default.LastBaseUsed = "2";
                Properties.Settings.Default.Save();
            }
        }

        private void Base8_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as CalculatorViewModel;
            if (viewModel != null)
            {
                viewModel.CurrentBase = 8;
                ConvertBaseShow.Text = "Current Base: 8";
                Properties.Settings.Default.LastBaseUsed = "8";
                Properties.Settings.Default.Save();
            }
        }

        private void Base10_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as CalculatorViewModel;
            if (viewModel != null)
            {
                viewModel.CurrentBase = 10;
                ConvertBaseShow.Text = "Current Base: 10";
                Properties.Settings.Default.LastBaseUsed = "10";
                Properties.Settings.Default.Save();
            }
        }

        private void Base16_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as CalculatorViewModel;
            if (viewModel != null)
            {
                viewModel.CurrentBase = 16;
                ConvertBaseShow.Text = "Current Base: 16";
                Properties.Settings.Default.LastBaseUsed = "16";
                Properties.Settings.Default.Save();
            }
        }


        private void ComplexOperationButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as CalculatorViewModel;
            if (viewModel == null) return;

            string inputExpression = Microsoft.VisualBasic.Interaction.InputBox(
                "Introduceți expresia matematică:",
                "Operatie Complexă",
                ""
            );

            if (!string.IsNullOrWhiteSpace(inputExpression))
            {
                viewModel?.EvaluateComplexExpression(inputExpression);
            }
            else
            {
                MessageBox.Show("Expresia introdusă nu este validă!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void HistoryButton_Click(object sender,RoutedEventArgs e)
        {

            var viewModel = DataContext as CalculatorViewModel;
            if (viewModel == null) return;
            HistoryWIndow historyWIndow = new HistoryWIndow();
            historyWIndow.ShowDialog();

        }



    }
}
