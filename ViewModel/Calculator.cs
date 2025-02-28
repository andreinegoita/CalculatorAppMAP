using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CalculatorProject.Model;

namespace CalculatorProject
{
    class CalculatorViewModel:INotifyPropertyChanged
    {
        private CalculatorModel _calculatorModel;
        private string _displayText;
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
            DisplayText = string.Empty;
        }

        public void AppendDigit(string digit)
        {
            DisplayText += digit;
        }
        public void SetOperator(string operatorSymbol)
        {
            if (_calculatorModel.CurrentOperator != null)
            {
                CalculateResult();
            }
            _calculatorModel.LastValue = double.Parse(DisplayText);
            _calculatorModel.CurrentOperator = operatorSymbol;
            DisplayText = $"{_calculatorModel.LastValue} {_calculatorModel.CurrentOperator} ";
        }


        public void CalculateResult()
        {
            if (_calculatorModel.CurrentOperator == null)
                return;

            double currentValue = double.Parse(DisplayText.Substring(DisplayText.LastIndexOf(' ') + 1));
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




        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
