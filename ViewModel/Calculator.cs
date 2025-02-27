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
        }

        public void AppendDigit(string digit)
        {
            DisplayText += digit;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
