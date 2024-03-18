using System.Windows.Controls;

namespace Program.Commands;

public class AppendText(TextBox textBox, string? textToAdd) : ICommand
{
    public void Execute()
    {
        textBox.Text += textToAdd;
    }
}