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
        public MemoryWindow(List<double> memoryStack)
        {
            InitializeComponent();
            memoryStack.Reverse(); 
            MemoryListBox.ItemsSource = memoryStack;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
