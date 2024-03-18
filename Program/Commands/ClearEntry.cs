using System.Windows.Controls;

namespace Program.Commands;

public class ClearEntry(TextBox textBox) : ICommand
{
    public void Execute()
    {
        textBox.Clear();
    }
}