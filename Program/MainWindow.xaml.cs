using System.Windows;
using Program.Commands;
using System.Globalization;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace Program
{
    public partial class MainWindow
    {
        private readonly string _operationPattern = @"(?<!\d\s*[-+*/])\d+\s*[+\-/*]\s*\d+\s*[+\-/*]";
        private readonly char[] _operators  = ['+', '-', '×', '÷'];
        private readonly List<string[]> _previousAction = new();
        
        public MainWindow()
        {
            InitializeComponent();
            InitializeEventHandlers();
            InitializeCustomCulture();
        }

        private void InitializeEventHandlers()
        {
            foreach (object button in MainGrid.Children)
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
        
        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                string? buttonText = button.Content.ToString();
                
                switch (buttonText)
                {
                    case "C":
                        new ClearAll(TextBox).Execute();
                        break;
                    case "CE":
                        new ClearEntry(TextBox, _previousAction).Execute();
                        break;
                    case "⌫":
                        new RemoveLast(TextBox).Execute();
                        break;
                    case "=":
                        new Calculation(TextBox, _previousAction).Execute();
                        break;
                    default:
                        new Operation(TextBox, buttonText, _operators, _operationPattern, _previousAction).Execute();
                        
                        if (Regex.IsMatch(TextBox.Text, _operationPattern))
                        {
                            new Calculation(TextBox, _previousAction).Execute();
                            new Operation(TextBox, buttonText, _operators ,_operationPattern, _previousAction).Execute();
                        }
                        break;
                }
            }
        }
    }
}