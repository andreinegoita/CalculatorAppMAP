using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CalculatorProject.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CalculatorProject
{
    class CalculatorViewModel:INotifyPropertyChanged
    {
        private CalculatorModel _calculatorModel;
        private string _displayText;
        private List<double> _memoryStack = new List<double>();
        public List<double> MemoryStack => _memoryStack;

        private double _memoryValue = 0;

        public string DisplayText
        {
            get { return _displayText; }
            set
            {
                if (_displayText != value)
                {
                    _displayText = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsDigitGroupingEnabled { get; set; } = false;


        public CalculatorViewModel()
        {
            _calculatorModel = new CalculatorModel();
            IsDigitGroupingEnabled = Properties.Settings.Default.IsDigitGroupingEnabled;
            DisplayText = "0";
        }


        private string FormatNumber(double number)
        {
            if (IsDigitGroupingEnabled)
            {
                return number.ToString("N0", CultureInfo.CurrentCulture);
            }
            else
            {
                return number.ToString();
            }
        }

        public void ToggleDigitGrouping()
        {
            IsDigitGroupingEnabled = !IsDigitGroupingEnabled;
            Properties.Settings.Default.IsDigitGroupingEnabled = IsDigitGroupingEnabled;
            Properties.Settings.Default.Save();
            if (IsDigitGroupingEnabled)
            {
                MessageBox.Show("Digit grouping is enabled");
            }
            else
            {
                MessageBox.Show("Digit grouping is disabled");
            }
        }

        public void ReformatDisplay()
        {
            if (IsDigitGroupingEnabled)
            {
                
                string cleanText = DisplayText.Replace(CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator, "");
                if (double.TryParse(cleanText, out double number))
                {
                    DisplayText = number.ToString("N0", CultureInfo.CurrentCulture);
                }
            }
        }

        public void AppendDigit(string digit)
        {
            if (DisplayText == "0" || DisplayText == "Error")
            {
                DisplayText = digit;
            }
            else
            {
                DisplayText += digit;
            }
        }

        public void SetOperator(string operatorSymbol)
        {
            if (_calculatorModel.CurrentOperator != null)
            {
                CalculateResult();
            }

            if (double.TryParse(DisplayText, out double number))
            {
                _calculatorModel.LastValue = number;
                _calculatorModel.CurrentOperator = operatorSymbol;
                DisplayText = "";
            }
        }



        public void CalculateResult()
        {
            if (_calculatorModel.CurrentOperator == null || DisplayText == "")
                return;
            string cleanText = DisplayText.Replace(CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator, "");
            if (!double.TryParse(cleanText, out double currentValue))
            {
                DisplayText = "Error";
                return;
            }

            double result = 0;
            switch (_calculatorModel.CurrentOperator)
            {
                case "+":
                    result = _calculatorModel.LastValue + currentValue;
                    break;
                case "-":
                    result = _calculatorModel.LastValue - currentValue;
                    break;
                case "*":
                    result = _calculatorModel.LastValue * currentValue;
                    break;
                case "÷":
                    if (currentValue != 0)
                        result = _calculatorModel.LastValue / currentValue;
                    else
                    {
                        DisplayText = "Error";
                        return;
                    }
                    break;
            }

            DisplayText = FormatNumber(result);
            _calculatorModel.CurrentOperator = null;
        }


        public void ChangeSign()
        {
            if (double.TryParse(DisplayText, out double currentValue))
            {
                if (currentValue != 0)
                {
                    currentValue = -currentValue;
                    DisplayText = FormatNumber(currentValue);
                }
            }
        }

        public void Inverse()
        {
            if (double.TryParse(DisplayText, out double currentValue) && currentValue != 0)
            {
                DisplayText = FormatNumber(1 / currentValue);
            }
            else
            {
                DisplayText = "Error";
            }
        }

        public void Square()
        {
            if (double.TryParse(DisplayText, out double currentValue))
            {
                DisplayText = FormatNumber(currentValue * currentValue);
            }
        }


        public void SquareRoot()
        {
            if (double.TryParse(DisplayText, out double currentValue) && currentValue >= 0)
            {
                DisplayText = FormatNumber(Math.Sqrt(currentValue));
            }
            else
            {
                DisplayText = "Error";
            }
        }

        public void Percentage()
        {
            if (double.TryParse(DisplayText, out double currentValue))
            {
                DisplayText = FormatNumber(currentValue / 100);
            }
        }

        public void AppendDecimal()
        {
            if (DisplayText.Contains("."))
                return;

            DisplayText += ".";
        }

        public void Backspace()
        {
            if (DisplayText.Length > 1)
            {
                DisplayText = DisplayText.Substring(0, DisplayText.Length - 1);
            }
            else
            {
                DisplayText = "0"; 
            }
        }


        public void ClearEntry()
        {
            DisplayText = "0";
        }


        public void ClearAll()
        {
            _calculatorModel.LastValue = 0;
            _calculatorModel.CurrentOperator = null;
            DisplayText = "0";
        }

        public void MemoryAdd()
        {
            if (double.TryParse(DisplayText, out double currentValue))
            {
                _memoryValue += currentValue;
            }
        }

        public void MemorySubtract()
        {
            if (double.TryParse(DisplayText, out double currentValue))
            {
                _memoryValue -= currentValue;
            }
        }

        public void MemoryStore()
        {
            if (double.TryParse(DisplayText, out double currentValue))
            {
                if (!_memoryStack.Contains(currentValue)) 
                {
                    _memoryStack.Add(currentValue);
                    _memoryValue = currentValue;
                    DisplayText = string.Empty;
                }
                else
                {
                    MessageBox.Show("Aceasta valoare este deja stocata in memorie!");
                }
            }
        }

        public void MemoryRecall()
        {
            if (_memoryStack.Count > 0)
            {
                DisplayText = FormatNumber(_memoryStack[^1]);
            }
            else
            {
                DisplayText = "0";
            }
        }

        public void MemoryClear()
        {
            _memoryStack.Clear();
            _memoryValue = 0;
        }


        public void CopyText()
        {
            if (!string.IsNullOrEmpty(DisplayText) && DisplayText != "0")
            {
                Clipboard.SetText(DisplayText);
            }
        }

        public void CutText()
        {
            if (!string.IsNullOrEmpty(DisplayText) && DisplayText != "0")
            {
                Clipboard.SetText(DisplayText);
                DisplayText = "0";  
            }
        }

        public void PasteText()
        {
            if (Clipboard.ContainsText())
            {
                string clipboardText = Clipboard.GetText();
                if (double.TryParse(clipboardText, out double number))
                {
                    DisplayText = number.ToString();
                    ReformatDisplay();
                }
                else
                {
                    DisplayText = "Error"; 
                }
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
