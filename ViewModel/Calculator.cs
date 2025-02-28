using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorProject
{
    class Calculator:INotifyPropertyChanged
    {
        private string _displayText;
        private double _lastValue;
        private string _currentOperator;
        public string DisplayText
        {
            get { return _displayText; }
            set
            {
                if (_displayText != value)
                {
                    _displayText = value;
                    OnPropertyChanged(nameof(DisplayText));
                }
            }
        }

        public Calculator()
        {
            DisplayText = string.Empty;
            _lastValue = 0;
            _currentOperator = string.Empty;
        }

        public void AppendDigit(string digit)
        {
            DisplayText += digit;
        }

        public void CalculateResult()
        {
            if (_currentOperator == string.Empty)
                return;

            double currentValue = double.Parse(DisplayText.Substring(DisplayText.LastIndexOf(' ') + 1));
            double result = 0;

            switch (_currentOperator)
            {
                case "+":
                    result = _lastValue + currentValue;
                    break;
                case "-":
                    result = _lastValue - currentValue;
                    break;
                case "*":
                    result = _lastValue * currentValue;
                    break;
                case "÷":
                    if (currentValue != 0)
                        result = _lastValue / currentValue;
                    else
                    { 
                       DisplayText = "Error";
                    }
                    break;
                    
            }

            DisplayText = result.ToString(); 
            _currentOperator = string.Empty; 
        }
 
        public void SetOperator(string operatorSymbol)
        {
            if (_currentOperator != string.Empty)
            {
                CalculateResult();  
            }

            _lastValue = double.Parse(DisplayText); 
            _currentOperator = operatorSymbol; 
            DisplayText = $"{_lastValue} {_currentOperator} ";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
