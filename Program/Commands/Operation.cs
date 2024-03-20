using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Program.Commands;

public class Operation(TextBox textBox, string? operation, string? operationPattern, char[] operators) : ICommand
{
    public void Execute()
    {
        string expression = textBox.Text;
        
        if (operation == ".")
        {
            if (expression.Length > 0 && char.IsDigit(expression[^1]))
            {
               
                if (!expression.Split(operators).Last().Contains('.'))
                {
                    textBox.Text += operation;
                }
            }
            
            return;
        }

        if (operation == "+" || operation == "-" || operation == "×" || operation == "÷")
        {
            if (expression.Length > 0)
            {
                char lastChar = expression[^1];
                
                if (lastChar == '.') return;
                
                if (lastChar == '+' || lastChar == '-' || lastChar == '×' || lastChar == '÷')
                {
                    textBox.Text = expression.Substring(0, expression.Length - 1);
                }
            }
        }

        textBox.Text += operation;
        
        if (!string.IsNullOrEmpty(operationPattern) && Regex.IsMatch(textBox.Text, operationPattern))
        {
            textBox.Text = textBox.Text.Substring(0, textBox.Text.Length - 1);
            
            new Calculation(textBox, operators).Execute();
            
            textBox.Text += operation;
        }
    }
}