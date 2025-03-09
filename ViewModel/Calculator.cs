using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CalculatorProject.Model;
using CalculatorProject.View;


namespace CalculatorProject
{
    class CalculatorViewModel:INotifyPropertyChanged
    {
        private CalculatorModel _calculatorModel;

        private string _displayText;

        private List<double> _memoryStack = new List<double>();
        public List<double> MemoryStack => _memoryStack;

        private double _memoryValue = 0;

        public ObservableCollection<string> History { get; set; } = new ObservableCollection<string>();

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

        private string ConvertToBase(double number, int numberBase)
        {
            if (numberBase == 10)
                return number.ToString();

            if (number < 0)
                return "-" + ConvertToBase(-number, numberBase);

            long integerPart = (long)number;
            string result = Convert.ToString(integerPart, numberBase).ToUpper();

            double fractionalPart = number - integerPart;
            if (fractionalPart > 0 && numberBase != 2)
            {
                result += ".";
                int precision = 5;
                while (fractionalPart > 0 && precision > 0)
                {
                    fractionalPart *= numberBase;
                    int digit = (int)fractionalPart;
                    if (digit >= 10 && digit <= 15)
                    {
                        result += (char)('A' + (digit - 10)); 
                    }
                    else
                    {
                        result += digit.ToString();
                    }
                    fractionalPart -= digit;
                    precision--;
                }
            }

            return result;
        }
        public bool IsDigitGroupingEnabled { get; set; } = false;

        private int _currentBase=10;
        public int CurrentBase
        {
            get { return _currentBase; }
            set
            {
                if (_currentBase != value)
                {
                    _currentBase = value;
                    OnPropertyChanged();
                    ReformatDisplay(); 
                }
            }
        }

        public CalculatorViewModel()
        {
            _calculatorModel = new CalculatorModel();
            IsDigitGroupingEnabled = Properties.Settings.Default.IsDigitGroupingEnabled;
            DisplayText = "0";
        }




        public void ConvertResultToBase()
        {
            if (double.TryParse(DisplayText, out double currentValue))
            {
                DisplayText = ConvertToBase(currentValue, CurrentBase);
            }
        }



        private string FormatNumber(double number)
        {
            if (IsDigitGroupingEnabled)
            {
                return number.ToString("N0", CultureInfo.CurrentCulture);
            }
            else
            {
                return number.ToString("G", CultureInfo.InvariantCulture); 
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


            string expression = $"{_calculatorModel.LastValue} {_calculatorModel.CurrentOperator} {currentValue}";
            AddToHistory(expression, result.ToString());

            if (CurrentBase != 10)
            {
                DisplayText = ConvertToBase(result, CurrentBase);
            }
            else
            {
                DisplayText = FormatNumber(result);

            }
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
        public void EvaluateComplexExpression(string expression)
        {
            if (expression.EndsWith("="))
            {
                expression.TrimEnd('=');
                try
                {
                    double result = EvaluateExpression(expression);
                    DisplayText = result.ToString();
                }
                catch (Exception)
                {
                    DisplayText = "Error";
                }
            }
            else
            {
                MessageBox.Show("Expresia nu este completa!");
            }
        }

        private double EvaluateExpression(string expression)
        {
            var tokens = Tokenize(expression);
            var postfix = InfixToPostfix(tokens);
            return EvaluatePostfix(postfix);
        }

        private List<string> Tokenize(string expression)
        {
            var tokens = new List<string>();
            StringBuilder number = new StringBuilder();
            for (int i = 0; i < expression.Length; i++)
            {
                char c = expression[i];
                if (char.IsDigit(c) || c == '.')
                {
                    number.Append(c);
                }
                else if (c == ' ')
                {
                    continue;
                }
                else
                {
                    if (number.Length > 0)
                    {
                        tokens.Add(number.ToString());
                        number.Clear();
                    }
                    tokens.Add(c.ToString());
                }
            }
            if (number.Length > 0)
                tokens.Add(number.ToString());
            return tokens;
        }

        private List<string> InfixToPostfix(List<string> tokens)
        {
            var output = new List<string>();
            var opStack = new Stack<string>();

            foreach (var token in tokens)
            {
                if (double.TryParse(token, out _))
                {
                    output.Add(token);
                }
                else if (IsOperator(token))
                {
                    while (opStack.Count > 0 && IsOperator(opStack.Peek()) &&
                          GetPrecedence(opStack.Peek()) >= GetPrecedence(token))
                    {
                        output.Add(opStack.Pop());
                    }
                    opStack.Push(token);
                }
                else if (token == "(")
                {
                    opStack.Push(token);
                }
                else if (token == ")")
                {
                    while (opStack.Count > 0 && opStack.Peek() != "(")
                    {
                        output.Add(opStack.Pop());
                    }
                    if (opStack.Count > 0 && opStack.Peek() == "(")
                        opStack.Pop();
                }
            }

            while (opStack.Count > 0)
                output.Add(opStack.Pop());

            return output;
        }

        private double EvaluatePostfix(List<string> tokens)
        {
            var stack = new Stack<double>();

            foreach (var token in tokens)
            {
                if (double.TryParse(token, out double num))
                {
                    stack.Push(num);
                }
                else if (IsOperator(token))
                {
                    double b = stack.Pop();
                    double a = stack.Pop();
                    switch (token)
                    {
                        case "+":
                            stack.Push(a + b);
                            break;
                        case "-":
                            stack.Push(a - b);
                            break;
                        case "*":
                            stack.Push(a * b);
                            break;
                        case "/":
                            stack.Push(a / b);
                            break;
                    }
                }
            }
            return stack.Pop();
        }

        private bool IsOperator(string token)
        {
            return token == "+" || token == "-" || token == "*" || token == "/";
        }

        private int GetPrecedence(string op)
        {
            if (op == "+" || op == "-")
                return 1;
            if (op == "*" || op == "/")
                return 2;
            return 0;
        }


        public void AddToHistory(string expression, string result)
        {
            History.Add($"{expression} = {result}");
        }

        public void ClearHistory()
        {
            History.Clear();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
