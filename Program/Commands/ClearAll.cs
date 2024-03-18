using System.Windows.Controls;

namespace Program.Commands;

public class ClearAll(TextBox inputTextBox, TextBox outputTextBox) : ICommand
{
    public void Execute()
    {
        inputTextBox.Text = string.Empty;
        outputTextBox.Text = string.Empty;
    }
}