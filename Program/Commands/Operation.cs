using System.Windows.Controls;

namespace Program.Commands;

public class Operation(TextBox textBox, string? operation) : ICommand
{
    public void Execute()
    {
        if (textBox.Text.EndsWith(".") || string.IsNullOrEmpty(textBox.Text)) return;

        if (textBox.Text.EndsWith("+") || textBox.Text.EndsWith("-") || textBox.Text.EndsWith("×") ||
            textBox.Text.EndsWith("÷"))
        {
            textBox.Text = textBox.Text.Substring(0, textBox.Text.Length - 1);
            textBox.Text += operation;
        }
        else
        {
            textBox.Text += operation;
        }
    }
}