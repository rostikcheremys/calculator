using System.Data;
using System.Windows.Controls;

namespace Program.Commands;

public class Сalculation(TextBox textBox) : ICommand
{
    public void Execute()
    {
        try
        {
            if (!string.IsNullOrEmpty(textBox.Text))
            {
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    object expression = new DataTable().Compute(textBox.Text.Replace('×', '*').Replace('÷', '/'), null);
                    
                    textBox.Text = expression.ToString() ?? string.Empty;
                }
            }
        }
        catch (Exception ex)
        {
            return;
        }
    }
}