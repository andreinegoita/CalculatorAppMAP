using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;
using System.Windows;

namespace CalculatorProject
{
    public partial class MemoryWindow : Window
    {
        public List<double> MemoryStack { get; private set; }
        public double? SelectedValue { get; private set; }

        public MemoryWindow(List<double> memoryStack)
        {
            InitializeComponent();
            MemoryStack = memoryStack;
            memoryStack.Reverse(); 
            MemoryListBox.ItemsSource = memoryStack;
        }

        private void SelectValue_Click(object sender, RoutedEventArgs e)
        {
            if (MemoryListBox.SelectedItem is double selectedValue)
            {
                SelectedValue = selectedValue;
                this.DialogResult = true;
            }
        }
        private void MemoryAdd_Click(object sender, RoutedEventArgs e)
        {
            if (MemoryListBox.SelectedItem is double selectedValue)
            {

                if (Application.Current.MainWindow.DataContext is CalculatorViewModel viewModel)
                {
                    if (double.TryParse(viewModel.DisplayText, out double value))
                    {
                        int index = MemoryStack.IndexOf(selectedValue);
                        if (index >= 0)
                        {
                            MemoryStack[index] += value;
                            RefreshList();
                        }
                    }
                }
            }
        }

        private void MemorySubtract_Click(object sender, RoutedEventArgs e)
        {
            if (MemoryListBox.SelectedItem is double selectedValue)
            {
                if (Application.Current.MainWindow.DataContext is CalculatorViewModel viewModel)
                {
                    if (double.TryParse(viewModel.DisplayText, out double value))
                    {
                        int index = MemoryStack.IndexOf(selectedValue);
                        if (index >= 0)
                        {
                            MemoryStack[index] -= value;
                            RefreshList();
                        }
                    }
                }
            }
        }

        private void MemoryClear_Click(object sender, RoutedEventArgs e)
        {
            if (MemoryListBox.SelectedItem is double selectedValue)
            {
                MemoryStack.Remove(selectedValue);
                RefreshList();
            }
        }
        private void RefreshList()
        {
            MemoryListBox.ItemsSource = null;
            MemoryListBox.ItemsSource = MemoryStack;
        } 
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
