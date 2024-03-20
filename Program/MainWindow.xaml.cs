using System.Windows;
using Program.Commands;
using System.Globalization;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace Program
{
    public partial class MainWindow : Window
    {
        
        private readonly string _operationPattern = @"[+\-/*].*[+\-/*]";
        private readonly char[] _operators = { '+', '-', '*', '/' };
        private static string _previousAction = "";
        
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
                        new Calculation(TextBox, _operators).Execute();
                        break;
                    default:
                        new Operation(TextBox, buttonText, _operationPattern, _operators).Execute();
                        
                        if (Regex.IsMatch(TextBox.Text, _operationPattern))
                        {
                            new Calculation(TextBox, _operators).Execute();
                            new Operation(TextBox, buttonText, _operationPattern, _operators).Execute();
                        }
                        break;
                }
            }
        }
        
        public static void PreviousAction(string expression)
        {
            _previousAction = expression;
        }
    }
}