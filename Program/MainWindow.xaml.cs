using System.Windows;
using Program.Commands;
using System.Globalization;
using System.Windows.Controls;

namespace Program
{
    public partial class MainWindow : Window
    {
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                string? buttonText = button.Content.ToString();

                
                if (Array.Exists(_numericButtons, element => element == buttonText))
                {
                    ICommand command = new AppendText(DisplayInput, buttonText);
                    ICommand command2 = new AppendText(DisplayOutput, buttonText);
                    command2.Execute();
                    command.Execute();
                }
                
                else if (Array.Exists(_operationButtons, element => element == buttonText))
                {
                    if (!string.IsNullOrEmpty(DisplayInput.Text))
                    {
                        ICommand command1 = new Operation(DisplayOutput, buttonText);
                        ICommand command2 = new AppendText(DisplayOutput, buttonText);
                        command2.Execute();
                        command1.Execute();
                        DisplayInput.Clear();
                    }
                }
                
                else
                {
                    switch (buttonText)
                    {
                        case ".":
                            ICommand command = new AppendPoint(DisplayInput);
                            command.Execute();
                            break;
                        case "C":
                            ICommand clearCommand = new ClearAll(DisplayInput, DisplayOutput);
                            clearCommand.Execute();
                            break;
                        case "CE":
                            ICommand clearEntryCommand = new ClearEntry(DisplayInput);
                            clearEntryCommand.Execute();
                            break;
                        case "\u232b":
                            ICommand removeLastCommand = new RemoveLast(DisplayInput);
                            removeLastCommand.Execute();
                            break;
                        case "=":
                            ICommand calculateCommand = new Сalculation(DisplayOutput, DisplayInput);
                            calculateCommand.Execute();
                            break;
                        default:
                            ICommand defaultCommand = new AppendText(DisplayInput, buttonText);
                            defaultCommand.Execute();
                            break;
                    }
                }
            }
        }
        
        private readonly string[] _numericButtons = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"};
        private readonly string[] _operationButtons = { "+", "-", "×", "÷" };


        public MainWindow()
        {
            InitializeComponent();
            InitializeEventHandlers();
            InitializeCustomCulture();
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

        private void InitializeEventHandlers()
        {
            foreach (var button in MainGrid.Children)
            {
                if (button is Button btn)
                {
                    btn.Click += Button_Click;
                }
            }
        }
    }
}
