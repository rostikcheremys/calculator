using System.Windows.Controls;

namespace Program.Commands;

public class MathOperations(TextBox textBox, char[] operators) : ICommand
{
    private readonly Dictionary<string, Func<double, double>?> _mathOperators = new()
    {
        { "√", Math.Sqrt },
        { "π", number => number * Math.PI },
        { "e", Math.Exp },
        { "n^2", number => Math.Pow(number, 2) },
        { "ln", Math.Log }
    };

    public void Execute()
    {
        string expression = textBox.Text;

        string[] mathOperators = { "√", "π", "e", "n^2", "ln" };

        foreach (string mathOperator in mathOperators)
        {
            if (expression.EndsWith(mathOperator))
            {
                MathOperation(mathOperator);
                break;
            }
        }
    }

    private void MathOperation(string mathOperator)
    {
        string expression = textBox.Text;

        if (expression.EndsWith(mathOperator) && expression.Length > 0)
        {
            expression = expression.Remove(expression.Length - mathOperator.Length);

            string[] parts = expression.Split(operators, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length > 0)
            {
                string lastNumberString = parts[^1].Trim();

                if (double.TryParse(lastNumberString, out double number))
                {
                    if (_mathOperators.TryGetValue(mathOperator, out Func<double, double>? operation))
                    {
                        operation!.Invoke(number);

                        double result = operation.Invoke(number);
                        
                        textBox.Text = expression.Substring(0, expression.Length - lastNumberString.Length) + result;
                    }
                }
            }
        }
    }
}