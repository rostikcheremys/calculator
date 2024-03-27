using System.Windows.Controls;

namespace Program.Commands;

public class MathOperations(TextBox textBox, char[] operators) : ICommand
{
    public void Execute()
    {
        if (textBox.Text.EndsWith("π"))
        {
            Maths("π");
        }

        if (textBox.Text.EndsWith("e"))
        {
            Maths("e");
        }
        
        if (textBox.Text.EndsWith("√"))
        {
            Maths("√");
        }
        
        if (textBox.Text.EndsWith("n^x"))
        {
            Maths("n^x");
        }
        
        if (textBox.Text.EndsWith("ln"))
        {
            Maths("ln");
        }
    }

    private void Maths(string mathOperator)
    {
        string expression = textBox.Text;
        
        if (expression.EndsWith(mathOperator))
        {
            expression = expression.Remove(expression.Length - 1);
            
            string[] parts = expression.Split(operators, StringSplitOptions.RemoveEmptyEntries);
            
            string lastNumberString = parts[^1].Trim();
            
            if (double.TryParse(lastNumberString, out double number))
            {
                double result = Math.Sqrt(number);
                
                textBox.Text = expression.Substring(0, expression.Length - lastNumberString.Length) + result.ToString();
            }
        }
    }
}