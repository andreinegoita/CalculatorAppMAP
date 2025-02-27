using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CalculatorProject;

namespace CalculatorProject;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
/// 
public partial class MainWindow : Window
{
    
    public MainWindow()
    {
        InitializeComponent();
        ViewModel = new Calculator();
        this.DataContext = ViewModel;
    }

    internal Calculator ViewModel { get; private set; }

    private void DigitButton_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        if (button != null)
        {
            ViewModel.AppendDigit(button.Content.ToString());
        }
    }
}