using System.Windows.Controls;

namespace Program.Commands;

public class AppendPoint(TextBox textBox) : ICommand
{
    public void Execute()
    {
        textBox.Text += ".";
    }
}