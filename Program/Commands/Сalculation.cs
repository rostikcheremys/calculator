using System.Data;
using System.Windows.Controls;

namespace Program.Commands;

public class Сalculation(TextBox inputTextBox, TextBox outputTextBox) : ICommand
{
    public void Execute()
    {
        try
        {
            object expression = new DataTable().Compute(inputTextBox.Text, null);
                    
            inputTextBox.Text += "=";
            outputTextBox.Text = expression.ToString();
                    
        }
        catch (Exception ex)
        {
            return;
        }
    }
}