using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CalculatorProject.Model;

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



        public CalculatorViewModel()
        {
            _calculatorModel = new CalculatorModel();
            DisplayText = "0";
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

            if (!double.TryParse(DisplayText, out double currentValue))
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

            DisplayText = result.ToString();
            _calculatorModel.CurrentOperator = null;
        }


        public void ChangeSign()
        {
            if (double.TryParse(DisplayText, out double currentValue))
            {
                if (currentValue != 0)
                {
                    DisplayText = (-currentValue).ToString();
                }
            }
        }

        public void Inverse()
        {
            if (double.TryParse(DisplayText, out double currentValue) && currentValue != 0)
            {
                DisplayText = (1 / currentValue).ToString();
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
                DisplayText = (currentValue * currentValue).ToString();
            }
        }


        public void SquareRoot()
        {
            if (double.TryParse(DisplayText, out double currentValue) && currentValue >= 0)
            {
                DisplayText = Math.Sqrt(currentValue).ToString();
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
                DisplayText = (currentValue / 100).ToString();
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
                DisplayText = _memoryStack[^1].ToString();
            }
            else
            {
                DisplayText = "0";
            }
        }

       

        public void MemoryClearItem(double value)
        {
            if (_memoryStack.Contains(value))
            {
                _memoryStack.Remove(value);
            }
        }

        public string MemoryShow()
        {
            return _memoryStack.Count > 0 ? string.Join(", ", _memoryStack) : "Memorie goală";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
