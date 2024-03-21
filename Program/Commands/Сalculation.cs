using System.Data;
using System.Windows.Controls;

namespace Program.Commands;

public class Calculation(TextBox textBox, List<string[]> previousAction) : ICommand
{
    public void Execute()
    {
        try
        {
            if (!string.IsNullOrEmpty(textBox.Text))
            {
                object result = new DataTable().Compute(textBox.Text.Replace('×', '*').Replace('÷', '/'), null);
               
                previousAction.Add(new[] { textBox.Text });
                
                textBox.Text = result.ToString() ?? string.Empty;
            }
        }
        catch (Exception)
        { 
            return;
        }
    }
}