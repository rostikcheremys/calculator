using System.Windows.Controls;

namespace Program.Commands;

public class ClearEntry(TextBox textBox ,List<string[]> previousAction) : ICommand
{
    public void Execute()
    {
        if (previousAction.Count > 0)
        {
            if (previousAction.Count > 0)
            {
                textBox.Text = previousAction[^1][0];
            }
            else
            {
                textBox.Clear();
            }
            
            previousAction.RemoveAt(previousAction.Count - 1);
        }
        else
        {
            textBox.Clear();
        }
    }
}