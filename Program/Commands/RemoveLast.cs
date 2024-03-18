using System.Windows.Controls;

namespace Program.Commands;

public class RemoveLast(TextBox textBox) : ICommand
{
    public void Execute()
    {
        if (!string.IsNullOrEmpty(textBox.Text))
        {
            textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
        }
    }
}