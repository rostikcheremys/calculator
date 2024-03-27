using System.Windows;
using Program.Commands;
using System.Globalization;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace Program
{
    public partial class MainWindow
    {
        private readonly string _operationPattern = @"(?<!\d\s*[-+×÷])\d+\s*[+\-÷×]\s*\d+\s*[+\-÷×]";
        private readonly char[] _operators  = ['+', '-', '×', '÷'];
        private readonly List<string[]> _previousAction = new();

        private void RemoveButtons()
        {
            MainGrid.Children.Remove(ButtonPі);
            MainGrid.Children.Remove(ButtonExp);
            MainGrid.Children.Remove(ButtonRoot);
            MainGrid.Children.Remove(ButtonPower);
            MainGrid.Children.Remove(ButtonLn);
            
            ChangeColumnCount(4);
        }

        private void AddButton(Button button, int column, int row)
        {
            Grid.SetColumn(button, column);
            Grid.SetRow(button, row);
            
            MainGrid.Children.Add(button);
        }

        private void AddButtons()
        {
            AddButton(ButtonPі, 4, 1);
            AddButton(ButtonExp, 4, 2);
            AddButton(ButtonRoot, 4, 3);
            AddButton(ButtonPower, 4, 4);
            AddButton(ButtonLn, 4, 5);

            ChangeColumnCount(5);
        }
        
        public MainWindow()
        {
            InitializeComponent();
            InitializeEventHandlers();
            InitializeCustomCulture();
            RemoveButtons();
        }
        
        private void ChangeColumnCount(int columnCount)
        {
            while (MainGrid.ColumnDefinitions.Count < columnCount)
            {
                MainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            while (MainGrid.ColumnDefinitions.Count > columnCount)
            {
                MainGrid.ColumnDefinitions.RemoveAt(MainGrid.ColumnDefinitions.Count - 1);
            }
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
            
            MinWidth = 330;
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
                        break; ;
                    case "☰":
                        ((Button)e.OriginalSource).Content = "<";
                        AddButtons();
                        break;
                    case "<":
                        ((Button)e.OriginalSource).Content = "☰";
                        RemoveButtons();
                        break;
                    default:
                        new Operation(TextBox, buttonText, _operators, _operationPattern, _previousAction).Execute();
                        //new MathOperations(TextBox, _previousAction).Execute();
                        
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