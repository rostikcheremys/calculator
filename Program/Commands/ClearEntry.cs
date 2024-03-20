using System.Windows.Controls;

namespace Program.Commands;

public class ClearEntry(TextBox textBox, string previousExpression) : ICommand
{
    public void Execute()
    {
        if (!string.IsNullOrEmpty(previousExpression))
        {
            textBox.Text = previousExpression;
            previousExpression = "";
        }
    }
}