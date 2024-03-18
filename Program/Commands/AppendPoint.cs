using System.Windows.Controls;

namespace Program.Commands;

public class AppendPoint(TextBox textBox) : ICommand
{
    public void Execute()
    {
        if (textBox.Text.EndsWith("+") || textBox.Text.EndsWith("-") || textBox.Text.EndsWith("×") || textBox.Text.EndsWith("÷")) return;
        
        if (!string.IsNullOrEmpty(textBox.Text))
        {
            textBox.Text += ".";
        }
    }
}