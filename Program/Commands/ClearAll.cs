using System.Windows.Controls;

namespace Program.Commands;

public class ClearAll(TextBox textBox) : ICommand
{
    public void Execute()
    {
        textBox.Text = string.Empty;
    }
}