using System.Windows;
using Program.Commands;
using System.Globalization;
using System.Windows.Controls;

namespace Program
{
    public partial class MainWindow : Window
    {
        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                string? buttonText = button.Content.ToString();
                
                if (Array.Exists(_numericButtons, element => element == buttonText))
                {
                    new AppendText(TextBox, buttonText).Execute();
                }
                else if (Array.Exists(_operationButtons, element => element == buttonText))
                {
                    new Operation(TextBox, buttonText).Execute();
                }
                
                else
                {
                    switch (buttonText)
                    {
                        case ".":
                            new AppendPoint(TextBox).Execute();
                            break;
                        case "C":
                            new ClearAll(TextBox).Execute();
                            break;
                        case "CE":
                            new ClearEntry(TextBox).Execute();
                            break;
                        case "⌫":
                            new RemoveLast(TextBox).Execute();
                            break;
                        case "=":
                            new Сalculation(TextBox).Execute();
                            break;
                        default:
                            new AppendText(TextBox, buttonText).Execute();
                            break;
                    }
                }
            }
        }
        
        private readonly string[]  _numericButtons = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        private readonly string[] _operationButtons = { "+", "-", "×", "÷" };
       
        public MainWindow()
        {
            InitializeComponent();
            InitializeEventHandlers();
            InitializeCustomCulture();
        }

        private void InitializeEventHandlers()
        {
            foreach (var button in MainGrid.Children)
            {
                if (button is Button btn)
                {
                    btn.Click += ButtonClick;
                }
            }
            
            MinWidth = 310;
            MinHeight = 480;
        }
        
        private void InitializeCustomCulture()
        {
            CultureInfo customCulture = new CultureInfo("en-US")
            {
                NumberFormat =
                {
                    NumberDecimalSeparator = "."
                }
            };
            
            Thread.CurrentThread.CurrentCulture = customCulture;
            Thread.CurrentThread.CurrentUICulture = customCulture;
        }
    }
}
