using System.Windows.Controls;

namespace Program.Commands;

public class MathOperations(TextBox textBox, char[] operators) : ICommand
{
    public void Execute()
    {
        string expression = textBox.Text;
        
        if (expression.EndsWith("√"))
        {
            ProcessMathOperation("√");
        }

        if (expression.EndsWith("π"))
        {
            ProcessMathOperation("π");
        }

        if (expression.EndsWith("e"))
        {
            ProcessMathOperation("e");
        }
        
        if (expression.EndsWith("n^2"))
        {
            ProcessMathOperation("n^2");
        }

        if (expression.EndsWith("ln"))
        {
            ProcessMathOperation("ln");
        }
    }
    
    private void ProcessMathOperation(string mathOperator)
    {
        string expression = textBox.Text;

        if (expression.EndsWith(mathOperator) && expression.Length > 0)
        {
            expression = expression.Remove(expression.Length - mathOperator.Length);

            string[] parts = expression.Split(operators, StringSplitOptions.RemoveEmptyEntries);
            
            string lastNumberString = parts[^1].Trim();

            if (double.TryParse(lastNumberString, out double number))
            {
                double result = 0;

                switch (mathOperator)
                {
                    case "√":
                        result = Math.Sqrt(number);
                        break;
                    case "π":
                        result = number * Math.PI;
                        break;
                    case "e":
                        result = Math.Exp(number);
                        break;
                    case "n^2":
                        result = Math.Pow(number, 2);
                        break;
                    case "ln":
                        result = Math.Log(number);
                        break;
                }

                textBox.Text = expression.Substring(0, expression.Length - lastNumberString.Length) + result;
            }
        }
    }
}